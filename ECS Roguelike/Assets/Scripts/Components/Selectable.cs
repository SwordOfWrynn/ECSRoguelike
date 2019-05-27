using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct Selectable : IComponentData
{
    //public string name;
    public bool isSelected;
}
