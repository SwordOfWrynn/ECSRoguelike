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
            GameObject spriteGO = SpriteManager.GetVisualGameObject(spriteToGameObject.VisualID);
            if (float.IsNaN(translation.Value.x))
            {
                Debug.LogError("SpriteVisualSystem -- OnUpdate: translation.Value is NaN!");
                return;
            }
            spriteGO.transform.position = translation.Value;
            spriteGO.transform.rotation = rotation.Value;
        });
        Entities.WithNone<Rotation>().ForEach((ref Translation translation, ref SpriteToGameObject spriteToGameObject) => {
            SpriteManager.GetVisualGameObject(spriteToGameObject.VisualID).transform.position = translation.Value;
        });
    }
}