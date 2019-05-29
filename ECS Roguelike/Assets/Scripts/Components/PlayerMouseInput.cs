using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

/// <see cref="PlayerMouseInputSystem"/>

public struct PlayerMouseInput : IComponentData
{
    public bool LeftClickValue;
    public bool RightClickValue;
    public float3 MousePositionValue;
}
