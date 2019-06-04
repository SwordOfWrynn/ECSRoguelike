using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;

public class DebugSystem : ComponentSystem
{
    protected unsafe override void OnUpdate()
    {
        Entities.ForEach((ref PhysicsCollider collider) =>
        {
            Color color;
            switch(UnityEngine.Random.Range(0,5))
            {
                case 0:
                    color = Color.red;
                    break;
                case 1:
                    color = Color.blue;
                    break;
                case 2:
                    color = Color.black;
                    break;
                case 3:
                    color = Color.green;
                    break;
                case 4:
                    color = Color.yellow;
                    break;
                default:
                    color = Color.cyan;
                    break;
            }


            if (collider.Value.GetUnsafePtr() != null)
            {
                Debug.DrawLine(collider.Value.Value.CalculateAabb().Min, collider.Value.Value.CalculateAabb().Max, color);
            }
        });
    }
}
