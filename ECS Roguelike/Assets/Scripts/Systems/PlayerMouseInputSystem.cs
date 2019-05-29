using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

[UpdateBefore(typeof(MovementSystem))]
public class PlayerMouseInputSystem : JobComponentSystem
{
    [BurstCompile]
    struct PlayerMouseInputJob : IJobForEach<PlayerMouseInput>
    {

        public bool leftClick;
        public bool rightClick;
        public float3 mousePosition;

        public void Execute(ref PlayerMouseInput data)
        {
            data.LeftClickValue = leftClick;
            data.RightClickValue = rightClick;
            data.MousePositionValue = mousePosition;
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {

        var mousePos = Input.mousePosition;
        var ray = Camera.main.ScreenPointToRay(mousePos);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            mousePos = new float3(hit.point.x, 0, hit.point.z);
        }

        var job = new PlayerMouseInputJob
        {
            leftClick = Input.GetMouseButtonDown(0),
            rightClick = Input.GetMouseButtonDown(1),
            mousePosition = mousePos,
        };
        return job.Schedule(this, inputDeps);
    }

}
