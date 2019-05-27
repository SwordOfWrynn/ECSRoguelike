using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerInputComponent : IComponentData
{
    public bool leftClick;
    public bool rightClick;
    public float3 mousePosition;
}
