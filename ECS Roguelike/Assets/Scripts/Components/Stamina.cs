using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct Stamina : IComponentData
{
    public int Value;
    public int StaminaCap;
    public int StaminaPerTurn;
}
