using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class XmlUnit : XmlObject
{
    string m_Name;
    XmlUnitComponent[] m_Components;
    string m_BaseSpritePath;

    public override XmlObjectType XmlObjectType => XmlObjectType.XmlUnit;

    public XmlUnit(string unitName, XmlUnitComponent[] xmlUnitComponentArray)
    {
        m_Components = xmlUnitComponentArray;

        Debug.Log("The components for this unit");
        foreach(var component in m_Components)
        {
            Debug.Log(component.ToString());
        }

    }
}

public class XmlUnitComponent
{
    public string Name;
    public List<float> Values;

    public override string ToString()
    {
        if (Values != null && Values.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name + ", ");
            foreach (var value in Values)
            {
                sb.Append(value.ToString() + ", ");
            }
            return sb.ToString();
        }
        else return Name;
    }
}