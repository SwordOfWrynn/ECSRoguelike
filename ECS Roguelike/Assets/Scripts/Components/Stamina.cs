using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct Stamina : IComponentData
{
    public float Value;
    public int StaminaCap;
    public float StaminaPerTurn;
}
