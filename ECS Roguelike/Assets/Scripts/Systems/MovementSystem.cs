using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class MovementSystem : JobComponentSystem
{
    const int STAMINA_PER_MOVE = 1;

    [BurstCompile]
    struct MovementSystemJob : IJobForEach<Translation, MovementInput, Stamina>
    {
        public bool run;
        public void Execute(ref Translation translation, ref MovementInput movementInput, ref Stamina stamina)
        {
            if ((math.round(stamina.Value) > STAMINA_PER_MOVE) &&
                (movementInput.Value.x != 0 || movementInput.Value.y != 0)
                && run)
            {

                translation.Value += new float3(movementInput.Value, 0);
                //movementInput.HorizontalValue = movementInput.VerticalValue = 0;
                movementInput.Value = float2.zero;
                stamina.Value -= STAMINA_PER_MOVE;
            }
        }
    }
    
    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new MovementSystemJob()
        {
            run = GameManager.run
        };
        
        // Now that the job is set up, schedule it to be run. 
        return job.Schedule(this, inputDependencies);
    }
}