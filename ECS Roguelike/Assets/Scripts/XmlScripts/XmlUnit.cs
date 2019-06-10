using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class XmlUnit : XmlObject
{
    string m_Name;
    public string Name { get => m_Name; }
    XmlUnitComponent[] m_Components;
    public XmlUnitComponent[] Components { get => m_Components; }
    string m_BaseSpritePath;
    public string SpritePath { get => m_BaseSpritePath; }

    public override XmlObjectType XmlObjectType => XmlObjectType.XmlUnit;

    public XmlUnit(string unitName, XmlUnitComponent[] xmlUnitComponentArray, string spritePath)
    {
        m_Name = unitName;
        m_Components = xmlUnitComponentArray;
        m_BaseSpritePath = spritePath;

        //Debug.Log("The components for this unit");
        //foreach(var component in m_Components)
        //{
        //    Debug.Log(component.ToString());
        //}
        Debug.Log(ToString());
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append($"XmlUnit: {m_Name}, Art Path: {m_BaseSpritePath}");
        if (m_Components.Length <= 0)
            return sb.ToString();
        foreach(var value in m_Components)
        {
            sb.Append($", Component: {value.ToString()}");
        }
        return sb.ToString();
    }

}

public class XmlUnitComponent
{
    public string Name;
    public List<string> Values;

    public override string ToString()
    {
        if (Values != null && Values.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name );
            foreach (var value in Values)
            {
                sb.Append($",  {value}");
            }
            return sb.ToString();
        }
        else return Name;
    }
}