using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

///<see cref="MovementSystem"/>

///<see cref="PlayerKeyboardInputSystem"/> //Modifies these values on the player

[Serializable]
public struct MovementInput : IComponentData
{
    //public int HorizontalValue;
    //public int VerticalValue;
    public float2 Value;
}
