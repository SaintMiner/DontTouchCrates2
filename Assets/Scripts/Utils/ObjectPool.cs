using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool sharedInstance;
    public List<MonoBehaviour> pooledObjects;

    [SerializeField] private GameObject objectToPool;
    [SerializeField] private int amountToPool;
    [SerializeField] private bool shouldExpand = true;

    private void Awake()
    {
        sharedInstance = this;
    }

    public void FillPool<T>() where T : MonoBehaviour
    {
        pooledObjects = new List<MonoBehaviour>();
        GameObject tmp;

        for (int i = 0; i < amountToPool; i++)
        {

            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp.GetComponent<T>());
        }            
    }

    public T GetPooledObject<T>() where T : MonoBehaviour
    {

        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy)
            {
                Debug.Log(pooledObjects[i]);
                return (T)pooledObjects[i];
            }
        }

        if (shouldExpand)
        {
            GameObject pooledObject = (GameObject)Instantiate(objectToPool.gameObject);
            pooledObject.SetActive(false);
            T behavior = pooledObject.GetComponent<T>();
            pooledObjects.Add(behavior);
            return behavior;
        }

        return null;
    }
}
