using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;

public class GameTurnSystem : JobComponentSystem
{
    // The job is also tagged with the BurstCompile attribute, which means
    // that the Burst compiler will optimize it for the best performance.
    [BurstCompile]
    struct GameTurnSystemJob : IJobForEach<GameTurn>
    {
        // Add fields here that your job needs to do its work.
        // For example,
        //    public float deltaTime;
        
        
        
        public void Execute(ref GameTurn gameTurn)
        {
            gameTurn.IsTurnValue = true;
            gameTurn.HasPlayedValue = false;
            
        }
    }
    
    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new GameTurnSystemJob();
        
        // Assign values to the fields on your job here, so that it has
        // everything it needs to do its work when it runs later.
        // For example,
        //     job.deltaTime = UnityEngine.Time.deltaTime;
        
        
        
        // Now that the job is set up, schedule it to be run. 
        return job.Schedule(this, inputDependencies);
    }
}