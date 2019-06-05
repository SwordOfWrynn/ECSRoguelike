using System.Collections;
using System.Collections.Generic;
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
                Debug.LogError("XmlLoader: Unregonized xml");
                break;
        }
    }

    void LoadUnitXml()
    {
        Debug.Log("XmlLoader -- LoadUnitXml");

        List<XmlUnitComponent> unitComponents = new List<XmlUnitComponent>();

        foreach(var element in XmlFile.Elements())
        {
            if (element.Name.ToString() == "Components")
                foreach (var childElement in element.Elements())
                {
                    XmlUnitComponent component = new XmlUnitComponent();
                    component.Name = childElement.Name.ToString();
                    if (childElement.HasElements)
                    {
                        component.Values = new List<float>();
                        foreach (var componentElement in childElement.Elements())
                        {
                            component.Values.Add(float.Parse(componentElement.Value));
                        }
                    }
                    unitComponents.Add(component);
                }

        }

        m_XmlObject = new XmlUnit(unitComponents.ToArray());
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
