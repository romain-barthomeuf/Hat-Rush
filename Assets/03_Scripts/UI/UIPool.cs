using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]

public class UIObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool;
    public bool shouldExpand;
}

public class UIPool : MonoBehaviour
{
    public static UIPool Instance;
    public List<UIObjectPoolItem> itemsToPool;
    public List<GameObject> pooledObjects;

    Transform ownTransform;

    void Awake()
    {
        Instance = this;
        ownTransform = transform;

        pooledObjects = new List<GameObject>();
        foreach (UIObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                CreateNewObject(item);
            }
        }
    }

    // Use this for initialization
    void Start()
    {
    }

    public GameObject GetPooledObject(string objectToPoolName)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].name == objectToPoolName)
            {
                return pooledObjects[i];
            }
        }
        foreach (UIObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.name == objectToPoolName)
            {
                if (item.shouldExpand)
                {
                    GameObject newObject = CreateNewObject(item);
                    return newObject;
                }
            }
        }
        return null;
    }

    GameObject CreateNewObject(UIObjectPoolItem item)
    {
        GameObject obj = (GameObject)Instantiate(item.objectToPool, ownTransform);
        obj.name = obj.name.Replace("(Clone)", "");
        obj.SetActive(false);
        pooledObjects.Add(obj);

        return obj;
    }

}
