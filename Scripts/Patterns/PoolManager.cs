using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    private static PoolManager _instance;
    private Dictionary<int, List<ObjectInstance>> poolDictionary = new Dictionary<int, List<ObjectInstance>>();
    private Dictionary<int, GameObject> poolHolders = new Dictionary<int, GameObject>();

    public static PoolManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PoolManager>();
            }
            return _instance;
        }
    }

    public void CreatePool(GameObject prefab, int poolSize)
    {
        int poolKey = prefab.GetInstanceID();
        if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey, new List<ObjectInstance>());

            GameObject poolHolder = new GameObject(prefab.name + " pool");
            poolHolder.transform.parent = transform;

            poolHolders.Add(poolKey, poolHolder);

            for (int i = 0; i < poolSize; i++)
            {
                ObjectInstance newObject = new ObjectInstance(Instantiate(prefab) as GameObject);
                poolDictionary[poolKey].Add(newObject);
                newObject.SetParent(poolHolder.transform);
            }
        }
    }

    public ObjectInstance CreatePool(GameObject prefab)
    {

        int poolKey = prefab.GetInstanceID();

        ObjectInstance newObject = new ObjectInstance(Instantiate(prefab) as GameObject);
        poolDictionary[poolKey].Add(newObject);

        newObject.SetParent(poolHolders[poolKey].transform);

        return newObject;
    }

    public void ReuseObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        int poolKey = prefab.GetInstanceID();
        if (poolDictionary.ContainsKey(poolKey))
        {
            foreach (ObjectInstance ob in poolDictionary[poolKey])
            {
                if (ob.gameObject.activeSelf == false)
                {
                    ObjectInstance objectToReuse = ob;
                    objectToReuse.Reuse(position, rotation);
                    return;
                }
            }
            CreatePool(prefab).Reuse(position, rotation);
        }
    }

    public GameObject ReturnReuseObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        int poolKey = prefab.GetInstanceID();
        if (poolDictionary.ContainsKey(poolKey))
        {
            foreach (ObjectInstance ob in poolDictionary[poolKey])
            {
                if (ob.gameObject.activeSelf == false)
                {
                    ObjectInstance objectToReuse = ob;
                    return objectToReuse.ReturnReuse( position, rotation);
                }
            }
            return CreatePool(prefab).ReturnReuse(position, rotation);
        }
        return null;
    }

    public class ObjectInstance
    {
        public GameObject gameObject;
        Transform transform;

        bool hasPoolObjectComponent;
        PoolObject poolObjectScript;

        public ObjectInstance(GameObject objectInstance)
        {
            gameObject = objectInstance;
            transform = gameObject.transform;
            gameObject.SetActive(false);

            if (gameObject.GetComponent<PoolObject>())
            {
                hasPoolObjectComponent = true;
                poolObjectScript = gameObject.GetComponent<PoolObject>();
            }
        }

        public void Reuse(Vector3 position, Quaternion rotation)
        {
            gameObject.SetActive(true);
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;

            if (hasPoolObjectComponent)
            {
                poolObjectScript.OnObjectReuse();
            }
        }

        public GameObject ReturnReuse(Vector3 position, Quaternion rotation)
        {
            gameObject.SetActive(true);
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;

            if (hasPoolObjectComponent)
            {
                poolObjectScript.OnObjectReuse();
            }
            return gameObject;
        }

        public void SetParent(Transform parent)
        {
            transform.parent = parent;
        }
    }
}
