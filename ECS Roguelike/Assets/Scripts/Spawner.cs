using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    EntityManager m_EntityManager;
    public Sprite m_Sprite;
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

        Rect spriteRect = m_Sprite.textureRect;
        spriteRect.x /= m_Sprite.texture.width;
        spriteRect.width /= m_Sprite.texture.width;
        spriteRect.y /= m_Sprite.texture.height;
        spriteRect.height /= m_Sprite.texture.height;
        // Log.Info("spriteRect {0} num UVs {1}", spriteRect, m_mesh.uv.Length);
        // m_mesh.uv[0] = new Vector2(spriteRect.x + 0.0f,             spriteRect.y + spriteRect.height);
        // m_mesh.uv[1] = new Vector2(spriteRect.x + spriteRect.width, spriteRect.y + spriteRect.height);
        // m_mesh.uv[2] = new Vector2(spriteRect.x + 0.0f,             spriteRect.y + 0.0f);
        // m_mesh.uv[3] = new Vector2(spriteRect.x + spriteRect.width, spriteRect.y + 0.0f);
        Vector2[] uvs = m_Mesh.uv;
        m_Mesh.uv[0] = new Vector2(0.0f, 1.0f);
        m_Mesh.uv[1] = new Vector2(1.0f, 1.0f);
        m_Mesh.uv[2] = new Vector2(0.0f, 0.0f);
        m_Mesh.uv[3] = new Vector2(1.0f, 0.0f);
        m_Mesh.uv = uvs;

        m_Material.mainTexture = m_Sprite.texture;

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
