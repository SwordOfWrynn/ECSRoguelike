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
    const float MIN_TIME_BETWEEN_INPUT = 0.5f;
    float timer;

    protected override void OnUpdate()
    {
        timer += Time.deltaTime;

        if ((Input.GetAxisRaw("Horizontal") != 0 || (int)Input.GetAxisRaw("Vertical") != 0) && timer >= MIN_TIME_BETWEEN_INPUT)
        {
            GameManager.run = true;
            Entities.WithAll<LocalPlayerTag>().ForEach((ref MovementInput playerMovementInput) =>
            {
                float horizontalValue = (int)Input.GetAxisRaw("Horizontal");
                float verticalValue = (int)Input.GetAxisRaw("Vertical");

                if (horizontalValue != 0 && verticalValue != 0)
                    verticalValue = 0;

                playerMovementInput.Value = new float2(horizontalValue, verticalValue);
            });
            timer = 0;
        }
        else
            GameManager.run = false;
    }

}