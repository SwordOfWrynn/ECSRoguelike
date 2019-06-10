using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

///<see cref="StaminiaSystem"/>

public struct Stamina : IComponentData
{
    public float Value;
    public int StaminaCap;
    public float StaminaPerTurn;
}
