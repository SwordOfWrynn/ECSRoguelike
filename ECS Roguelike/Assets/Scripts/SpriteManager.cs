using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteManager
{

    static Dictionary<int, GameObject> m_VisualGameObjectDictionary = new Dictionary<int, GameObject>();

    public static int RegisterVisualGameObjectAndReturnKey(GameObject gameObject)
    {
        int key= m_VisualGameObjectDictionary.Count - 1;

        m_VisualGameObjectDictionary.Add(key, gameObject);

        return key;
    }
    
    public static void UnRegisterVisualGameObjectAndReturnKey(int key)
    {
        if(m_VisualGameObjectDictionary.ContainsKey(key) == false)
        {
            Debug.LogError("SpriteManager -- UnRegisterVisualGameObjectAndReturnKey: Key did not exist");
            return;
        }
        m_VisualGameObjectDictionary.Remove(key);
    }

    public static GameObject GetVisualGameObject(int key)
    {
        return m_VisualGameObjectDictionary[key];
    }

}
