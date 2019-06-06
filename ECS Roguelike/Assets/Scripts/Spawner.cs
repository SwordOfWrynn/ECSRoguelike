using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject playerSpritePrefab;
    public GameObject enemySpritePrefab;
    public GameObject floorSpritePrefab;
    public GameObject wallSpritePrefab;
    public int enemiesToSpawn;

    EntityManager m_EntityManager;
    public UnityEngine.Mesh m_Mesh;
    public UnityEngine.Material m_Material;

    // Start is called before the first frame update
    void Start()
    {
        m_EntityManager = World.Active.EntityManager;

        //EntityArchetype playerArchtype = m_EntityManager.CreateArchetype(
        //    typeof(SpriteToGameObject),
        //    typeof(LocalToWorld),
        //    typeof(Translation),
        //    typeof(Rotation),
        //    typeof(PlayerMouseInput),
        //    typeof(MovementInput),
        //    typeof(LocalPlayerTag),
        //    typeof(Stamina)
        //    );

        //Entity entity = m_EntityManager.CreateEntity(playerArchtype);
        //GameObject gameObject = Instantiate(playerSpritePrefab);
        //int visualID = SpriteManager.RegisterVisualGameObjectAndReturnKey(gameObject);

        //m_EntityManager.SetComponentData(entity, new SpriteToGameObject
        //{
        //    VisualID = visualID
        //});
        //m_EntityManager.SetComponentData(entity, new Stamina
        //{
        //    Value = 5,
        //    StaminaCap = 20,
        //    StaminaPerTurn = 2
        //}) ;
        //m_EntityManager.SetComponentData(entity, new PhysicsCollider
        //{
        //    Value = Unity.Physics.BoxCollider.Create(float3.zero, quaternion.identity, new float3(1, 1, 1), 0.5f)
        //});

        SpawnEnemies();
        CreateRoom();
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
                Value = new float3(i, i+1, 0)
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
            //m_EntityManager.SetComponentData(entity, new PhysicsCollider
            //{
            //    Value = Unity.Physics.BoxCollider.Create(new float3(i, i + 1, 0), quaternion.identity, new float3(1, 1, 1), 0.5f)
            //});

        }
    }

    void CreateRoom()
    {
        EntityArchetype roomSpritesArchtype = m_EntityManager.CreateArchetype(
            typeof(SpriteToGameObject),
            typeof(LocalToWorld),
            typeof(Translation)
            );

        Entity entity;
        GameObject spriteGO;
        for (int x = -5; x < 5; x++)
        {
            for (int y = -5; y < 5; y++)
            {
                entity = m_EntityManager.CreateEntity(roomSpritesArchtype);

                if ((x == -5 || x == 4) || (y == -5 || y == 4))
                    spriteGO = Instantiate(wallSpritePrefab);
                else
                    spriteGO = Instantiate(floorSpritePrefab);

                int visualID = SpriteManager.RegisterVisualGameObjectAndReturnKey(spriteGO);

                m_EntityManager.SetComponentData(entity, new Translation
                {
                    Value = new float3(x, y, 0)
                });
                m_EntityManager.SetComponentData(entity, new SpriteToGameObject
                {
                    VisualID = visualID
                });
            }
        }
    }
}
