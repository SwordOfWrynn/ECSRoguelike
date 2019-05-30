using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;

public class MovementSystem : JobComponentSystem
{
    const int STAMINA_PER_MOVE = 1;

    [BurstCompile]
    struct MovementSystemJob : IJobForEach<Translation, MovementInput, Stamina>
    {

        public void Execute(ref Translation translation, ref MovementInput movementInput, ref Stamina stamina)
        {
            if (round(stamina.Value) > STAMINA_PER_MOVE)
            {
                translation.Value += new float3(movementInput.HorizontalValue, movementInput.VerticalValue, 0);
                movementInput.HorizontalValue = movementInput.VerticalValue = 0;
                stamina.Value -= STAMINA_PER_MOVE;
            }
        }
    }
    
    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new MovementSystemJob();
        
        // Now that the job is set up, schedule it to be run. 
        return job.Schedule(this, inputDependencies);
    }
}