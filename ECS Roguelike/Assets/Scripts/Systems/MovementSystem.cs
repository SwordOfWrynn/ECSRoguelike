using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class MovementSystem : ComponentSystem
{
    const int STAMINA_PER_MOVE = 1;

    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation, ref MovementInput movementInput, ref Stamina stamina) => {
            if ((math.round(stamina.Value) > STAMINA_PER_MOVE) &&
                (movementInput.Value.x != 0 || movementInput.Value.y != 0)
                && GameManager.run)
            {

                translation.Value += new float3(movementInput.Value, 0);
                //movementInput.HorizontalValue = movementInput.VerticalValue = 0;
                movementInput.Value = float2.zero;
                stamina.Value -= STAMINA_PER_MOVE;
            }
        });
    }
}