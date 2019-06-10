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
    public GameObject playerSpritePrefab;
    Dictionary<string, Type> componentTypeDictionary = new Dictionary<string, Type>();

    public Sprite[] spritesArray;
    public int test = 0;
    GameObject player;
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
        foreach(var value in componentDataQuery)
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
        GameObject gameObject = Instantiate(playerSpritePrefab);

        FileInfo spriteFile = new FileInfo(Application.streamingAssetsPath + "/" + unit.SpritePath);

        //StartCoroutine(GetTextureAsync(spriteFile));

        byte[] imageData = File.ReadAllBytes(spriteFile.FullName);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageData);
        texture.filterMode = FilterMode.Point;
        int arraySize = (texture.width / 16) * (texture.height / 16);
        Sprite[] sprites = new Sprite[arraySize];
        int arrayPosition = 0;
        for (int x = 0; x < texture.width/16; x++)
        {
            for (int y = 0; y < texture.height/16; y++)
            {
                Debug.Log("At position " + x + "," + y);
                sprites[arrayPosition] = Sprite.Create(texture, new Rect(x * 16, y * 16, 16, 16), new Vector2(0.5f, 0.5f), 16);
                //sprites[arrayPosition].
                arrayPosition++;
            }
        }
        spritesArray = sprites;

        //Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[unit.SpritePosition];

        int visualID = SpriteManager.RegisterVisualGameObjectAndReturnKey(gameObject);

        EntityManager.SetComponentData(entity, new SpriteToGameObject
        {
            VisualID = visualID
        });
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

        return componentTypes.Where(c => c!= null).ToArray();
    }

    IEnumerator GetTextureAsync(FileInfo spriteFile)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(@"file:///" + spriteFile.FullName.ToString()))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                // Get downloaded asset bundle
                var texture = DownloadHandlerTexture.GetContent(request);
            }

        }
    }

}
