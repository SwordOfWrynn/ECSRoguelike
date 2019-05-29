using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using static Unity.Mathematics.math;

public class PlayerKeyboardInputSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        if ((Input.GetAxisRaw("Horizontal") != 0 || (int)Input.GetAxisRaw("Vertical") != 0))
        {
            Entities.WithAll<LocalPlayerTag>().ForEach((ref MovementInput playerMovementInput, ref GameTurn gameTurn) =>
            {
                if (gameTurn.IsTurnValue && gameTurn.HasPlayedValue == false)
                {
                    playerMovementInput.HorizontalValue = (int)Input.GetAxisRaw("Horizontal");
                    playerMovementInput.VerticalValue = (int)Input.GetAxisRaw("Vertical");
                    gameTurn.HasPlayedValue = true;
                }
            });
        }
    }
}