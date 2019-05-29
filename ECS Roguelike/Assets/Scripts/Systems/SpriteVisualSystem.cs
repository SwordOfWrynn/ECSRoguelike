using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class SpriteVisualSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation, ref Rotation rotation, ref SpriteToGameObject spriteToGameObject) =>
        {
            SpriteManager.GetVisualGameObject(spriteToGameObject.VisualID).transform.position = translation.Value;
        });
    }
}