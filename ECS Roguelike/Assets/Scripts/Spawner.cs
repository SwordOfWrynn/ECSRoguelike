using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject spritePrefab;

    EntityManager m_EntityManager;
    public Mesh m_Mesh;
    public Material m_Material;

    // Start is called before the first frame update
    void Start()
    {
        m_EntityManager = World.Active.EntityManager;

        EntityArchetype playerArchtype = m_EntityManager.CreateArchetype(
            typeof(SpriteToGameObject),
            typeof(LocalToWorld),
            typeof(Translation),
            typeof(Rotation),
            typeof(PlayerMouseInput),
            typeof(MovementInput),
            typeof(LocalPlayerTag),
            typeof(Stamina)
            );

        Entity entity = m_EntityManager.CreateEntity(playerArchtype);
        GameObject gameObject = Instantiate(spritePrefab);
        int visualID = SpriteManager.RegisterVisualGameObjectAndReturnKey(gameObject);

        m_EntityManager.SetComponentData(entity, new SpriteToGameObject
        {
            VisualID = visualID
        });
        m_EntityManager.SetComponentData(entity, new Stamina
        {
            Value = 5,
            StaminaCap = 20,
            StaminaPerTurn = 2
        }) ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
