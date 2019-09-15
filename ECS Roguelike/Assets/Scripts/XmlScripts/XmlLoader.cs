using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;

public class XmlLoader
{
    private XElement m_XmlFile;
    public XElement XmlFile { get => m_XmlFile; }

    private XmlObject m_XmlObject;
    public XmlObject XmlObject { get => m_XmlObject; }

    // Start is called before the first frame update
    public XmlLoader(string path)
    {
        m_XmlFile = XElement.Load(path);
        Debug.Log(m_XmlFile.Name);
        switch (m_XmlFile.Name.ToString())
        {
            case "Unit":
                LoadUnitXml();
                break;
            case "Room":
                LoadRoomXml();
                break;
            default:
                Debug.LogError("XmlLoader: Unregonized xml type");
                break;
        }
    }

    void LoadUnitXml()
    {
        Debug.Log("XmlLoader -- LoadUnitXml");

        //Values to pass to the unit when it's created
        string unitName = string.Empty;
        List<XmlUnitComponent> unitComponents = new List<XmlUnitComponent>();
        string spritePath = string.Empty;
        int spritePosition = 0;

        foreach (var element in m_XmlFile.Elements())
        {
            if (element.Name == "Art")
            {
                spritePath = element.Element("BaseSpritePath").Value;
                spritePosition = int.Parse(element.Element("SpritePosition").Value);
            }
            else if (element.Name == "Name")
                unitName = element.Value;

        }
        //Create a LINQ query to find the Components element. then get its children ordered by name
        var componentQuery =
            from element in m_XmlFile.Elements()
            where element.Name == "Components"
            from component in element.Elements()
            orderby component.Name.ToString()
            select component;

        //Iterate over the query for the results
        foreach(var component in componentQuery)
        {
            XmlUnitComponent unitComponent = new XmlUnitComponent();
            unitComponent.Name = component.Name.ToString();
            if (component.HasElements)
            {
                unitComponent.Values = new List<string>();
                foreach (var componentelement in component.Elements())
                {
                    unitComponent.Values.Add(componentelement.Value);
                }
            }
            unitComponents.Add(unitComponent);
        }

        //Create the unit
        m_XmlObject = new XmlUnit(unitName, unitComponents.ToArray(), spritePath, spritePosition);
    }

    void LoadItemXml()
    {
        Debug.Log("XmlLoader -- LoadItemXml");
        throw new System.NotImplementedException();
    }

    void LoadRoomXml()
    {
        Debug.Log("XmlLoader -- LoadRoomXml");
        throw new System.NotImplementedException();
    }
}
