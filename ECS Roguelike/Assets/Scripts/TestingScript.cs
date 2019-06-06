using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    public GameObject playerSpritePrefab;
    Dictionary<string, Type> componentTypeDictionary = new Dictionary<string, Type>();
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

        XmlLoader xmlLoader = new XmlLoader(Application.streamingAssetsPath + @"\TestUnit.xml");
        XmlUnit unit = (XmlUnit)xmlLoader.XmlObject;

        EntityManager EntityManager = World.Active.EntityManager;

        EntityArchetype playerArchtype = EntityManager.CreateArchetype(
            ComponentTypesFromUnit(unit)
            );

        Entity entity = EntityManager.CreateEntity(playerArchtype);
        GameObject gameObject = Instantiate(playerSpritePrefab);
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

}
