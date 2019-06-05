using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum XmlObjectType : byte
{
    XmlUnit,
    XmlItem,
    XmlRoom
}

public abstract class XmlObject
{
    public abstract XmlObjectType XmlObjectType { get; }
}
