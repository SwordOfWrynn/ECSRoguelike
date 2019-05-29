using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct GameTurn : IComponentData
{
    public bool IsTurnValue;
    public bool HasPlayedValue;
}
