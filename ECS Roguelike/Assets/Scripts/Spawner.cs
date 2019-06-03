using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject playerSpritePrefab;
    public GameObject enemySpritePrefab;
    public int enemiesToSpawn;

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
        GameObject gameObject = Instantiate(playerSpritePrefab);
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

        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemies()
    {
        EntityArchetype enemyArchtype = m_EntityManager.CreateArchetype(
            typeof(SpriteToGameObject),
            typeof(LocalToWorld),
            typeof(Translation),
            typeof(Rotation),
            typeof(PlayerMouseInput),
            typeof(MovementInput),
            typeof(Stamina)
            );

        Entity entity;
        GameObject spriteGO;
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            entity = m_EntityManager.CreateEntity(enemyArchtype);
            spriteGO = Instantiate(enemySpritePrefab);
            int visualID = SpriteManager.RegisterVisualGameObjectAndReturnKey(spriteGO);

            m_EntityManager.SetComponentData(entity, new Translation
            {
                Value = new float3(i, i+2, 0)
            });
            m_EntityManager.SetComponentData(entity, new SpriteToGameObject
            {
                VisualID = visualID
            });
            m_EntityManager.SetComponentData(entity, new Stamina
            {
                Value = 5,
                StaminaCap = 10,
                StaminaPerTurn = 1
            });
        }
    }

}
