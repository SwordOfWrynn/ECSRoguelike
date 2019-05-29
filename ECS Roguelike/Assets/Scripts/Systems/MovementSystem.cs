using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;

public class MovementSystem : JobComponentSystem
{
    [BurstCompile]
    struct MovementSystemJob : IJobForEach<Translation, MovementInput>
    {
        
        public void Execute(ref Translation translation, ref MovementInput movementInput)
        {
            translation.Value += new float3(movementInput.HorizontalValue, movementInput.VerticalValue, 0);
            movementInput.HorizontalValue = movementInput.VerticalValue = 0;
        }
    }
    
    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new MovementSystemJob();
        
        // Now that the job is set up, schedule it to be run. 
        return job.Schedule(this, inputDependencies);
    }
}