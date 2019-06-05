using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        XmlLoader xmlLoader = new XmlLoader(Application.streamingAssetsPath + @"\TestUnit.xml");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
