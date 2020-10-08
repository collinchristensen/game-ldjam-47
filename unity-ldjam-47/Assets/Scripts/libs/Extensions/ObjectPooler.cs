using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ObjectPoolerItem
{
    public GameObject prefabToPool;
    public int amountToPool;
    public bool shouldExpand;
}


// pools all objects, make sure every item is a prefab
public class ObjectPooler : MonoBehaviour
{

    public int maximumPooledObjects = 1000;

    public List<GameObject> pooledObjects;

    public List<ObjectPoolerItem> itemsToPool;


    public static ObjectPooler SharedInstance;

    public static ObjectPooler Instance
    {
        get
        {
            if (SharedInstance == null)
            {
                SharedInstance = GameObject.FindObjectOfType<ObjectPooler>();
            }

            return SharedInstance;
        }
    }

    private void Awake()
    {
        // singleton check
        if (SharedInstance != null && SharedInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        SharedInstance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolerItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.prefabToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObjectByName(string name)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].name.Contains(name))
            {
                Debug.Log("\npooledObjectsname: " + pooledObjects[i].name + "\ngo.name: " + name + "\n\n\n");
                return pooledObjects[i];
            }
        }
        foreach (ObjectPoolerItem item in itemsToPool)
        {
            if (item.prefabToPool.name.Contains(name) && item.shouldExpand)
            {
                if (pooledObjects.Count < maximumPooledObjects)
                {
                    GameObject obj = (GameObject)Instantiate(item.prefabToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }

    /*
    public GameObject GetPooledObjectByObject(GameObject go) {

        string name = go.name;

        GameObject temp = GetPooledObjectByName(name);

        if (temp != null) {
            return temp;
        }
        else {
            return AddNewPooledPrefab(go);
        }
    }

    public GameObject AddNewPooledPrefab(GameObject prefab) {

        ObjectPoolerItem temp = new ObjectPoolerItem {
            prefabToPool = prefab,
            amountToPool = 1,
            shouldExpand = true
        };
        itemsToPool.Add(temp);

        GameObject obj = Instantiate(temp.prefabToPool);
        obj.Hide();
        pooledObjects.Add(obj);
        return obj;
    }
    */

}