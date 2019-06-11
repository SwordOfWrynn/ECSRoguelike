using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Networking;

public class TestingScript : MonoBehaviour
{
    public GameObject spritePrefab;
    Dictionary<string, Type> componentTypeDictionary = new Dictionary<string, Type>();

    List<SpriteSheet> spriteSheets = new List<SpriteSheet>();

    // Start is called before the first frame update
    void Start()
    {
        var componentDataQuery =
            from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
            from type in domainAssembly.GetLoadableTypes()
            where type != null && typeof(IComponentData).IsAssignableFrom(type) && !type.IsAbstract && type.IsPublic
            orderby type.Name
            select type;

        StringBuilder sb = new StringBuilder();
        foreach (var value in componentDataQuery)
        {
            sb.Append($"{value.Name}, ");
            componentTypeDictionary.Add(value.Name, value);
        }
        Debug.Log(sb);

        XmlLoader xmlLoader = new XmlLoader(Application.streamingAssetsPath + @"\ExampleMod\Units\TestUnit.xml");
        XmlUnit unit = (XmlUnit)xmlLoader.XmlObject;

        EntityManager EntityManager = World.Active.EntityManager;

        EntityArchetype playerArchtype = EntityManager.CreateArchetype(
            ComponentTypesFromUnit(unit)
            );

        Entity entity = EntityManager.CreateEntity(playerArchtype);
        GameObject gameObject = Instantiate(spritePrefab);

        int spriteSheetIndex = CheckSpriteSheetsForMatch(spriteSheets, unit.SpritePath);
        if (spriteSheetIndex == -1)
        {
            FileInfo spriteFile = new FileInfo(Application.streamingAssetsPath + "/" + unit.SpritePath);

            spriteSheets.Add(new SpriteSheet() { Name = unit.SpritePath, Sprites = GetSpriteSheetFromFile(spriteFile) });
            spriteSheetIndex = spriteSheets.Count - 1;
        }
        Sprite[] spritesArray = spriteSheets[spriteSheetIndex].Sprites;
        gameObject.GetComponent<SpriteRenderer>().sprite = spritesArray[unit.SpritePosition];

        //Sprite sprite = spriteSheets[spriteSheetIndex].Sprites[unit.SpritePosition];
        //gameObject.GetComponent<SpriteRenderer>().sprite = sprite;

        //List<SpriteSheet> sheets = spriteSheets;
        //gameObject.GetComponent<SpriteRenderer>().sprite = sheets[spriteSheetIndex].Sprites[unit.SpritePosition];

        //gameObject.GetComponent<SpriteRenderer>().sprite = spriteSheets[spriteSheetIndex].Sprites[unit.SpritePosition];

        int visualID = SpriteManager.RegisterVisualGameObjectAndReturnKey(gameObject);

        EntityManager.SetComponentData(entity, new SpriteToGameObject
        {
            VisualID = visualID
        });

        var staminaIndex = unit.Components.Select((value, index) => new { value, index = index + 1 })
                .Where(pair => pair.value.Name == "Stamina")
                .Select(pair => pair.index)
                .FirstOrDefault() - 1;

        Debug.Log($"The stamina to start is {unit.Components[staminaIndex].Values[0]}");

        EntityManager.SetComponentData(entity, new Stamina
        {
            Value = 5,
            StaminaCap = 20,
            StaminaPerTurn = 2
        });
    }

    // Update is called once per frame
    void Update()
    {
    }

   ComponentType[] ComponentTypesFromUnit(XmlUnit unit)
    {
        ComponentType[] componentTypes = new ComponentType[unit.Components.Length];
        List<int> invalidPositions = new List<int>();
        for (int i = 0; i < unit.Components.Length; i++)
        {
            if (componentTypeDictionary.ContainsKey(unit.Components[i].Name) == false) {
                Debug.LogError($"TestingScript -- ComponentTypesFromUnit: {unit.Components[i].Name} was not in the dictionary");
                invalidPositions.Add(i);
                continue;
                }
            componentTypes[i] = componentTypeDictionary[unit.Components[i].Name];
        }

        return componentTypes.Where(c => c != null).ToArray();
    }

    public Sprite[] GetSpriteSheetFromFile(FileInfo spriteFile)
    {
        byte[] imageData = File.ReadAllBytes(spriteFile.FullName);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageData);
        texture.filterMode = FilterMode.Point;
        int arraySize = (texture.width / 16) * (texture.height / 16);
        Sprite[] sprites = new Sprite[arraySize];
        int arrayPosition = 0;
        for (int x = 0; x < texture.width / 16; x++)
        {
            for (int y = 0; y < texture.height / 16; y++)
            {
                //Debug.Log("At position " + x + "," + y);
                sprites[arrayPosition] = Sprite.Create(texture, new Rect(x * 16, y * 16, 16, 16), new Vector2(0.5f, 0.5f), 16);
                arrayPosition++;
            }
        }
        return sprites;
    }

    /// <summary>
    /// Checks the give array for a name match, and returns the index of the match if found, and a -1 if no match was found
    /// </summary>
    /// <param name="spriteSheetList"></param>
    /// <param name="spriteSheetName"></param>
    /// <returns></returns>
    int CheckSpriteSheetsForMatch(List<SpriteSheet> spriteSheetList, string spriteSheetName)
    {
        return  spriteSheetList.Select((value, index) => new { value, index = index + 1 })
                .Where(pair => pair.value.Name == spriteSheetName)
                .Select(pair => pair.index)
                .FirstOrDefault() - 1;
    }

    //IEnumerator GetTextureAsync(FileInfo spriteFile)
    //{
    //    using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(@"file:///" + spriteFile.FullName.ToString()))
    //    {
    //        yield return request.SendWebRequest();

    //        if (request.isNetworkError || request.isHttpError)
    //        {
    //            Debug.Log(request.error);
    //        }
    //        else
    //        {
    //            // Get downloaded asset bundle
    //            var texture = DownloadHandlerTexture.GetContent(request);
    //        }

    //    }
    //}

    struct SpriteSheet {
        public string Name;
        public Sprite[] Sprites;
    }

}
