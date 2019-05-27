using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    EntityManager m_EntityManager;
    public Mesh m_Mesh;
    public Material m_Material;

    // Start is called before the first frame update
    void Start()
    {
        m_EntityManager = World.Active.EntityManager;

        EntityArchetype archtype = m_EntityManager.CreateArchetype(
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(Translation));

        Entity entity = m_EntityManager.CreateEntity(archtype);

        m_EntityManager.SetSharedComponentData(entity, new RenderMesh
        {
            mesh = m_Mesh,
            material = m_Material
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
