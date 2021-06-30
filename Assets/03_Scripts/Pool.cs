using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]

public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool;
    public bool shouldExpand;
}

public class Pool : MonoBehaviour
{
    public static Pool Instance;
    public List<ObjectPoolItem> itemsToPool;
    public List<GameObject> pooledObjects;

    void Awake()
    {
        Instance = this;

        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
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
        foreach (ObjectPoolItem item in itemsToPool)
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

    GameObject CreateNewObject(ObjectPoolItem item)
    {
        GameObject obj = (GameObject)Instantiate(item.objectToPool);
        obj.name = obj.name.Replace("(Clone)", "");
        obj.SetActive(false);
        pooledObjects.Add(obj);

        return obj;
    }

}
