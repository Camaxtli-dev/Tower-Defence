using UnityEngine;

public class CreateOrUpgrade : MonoBehaviour {

    public void Create(GameObject gameObj, Vector3 objectPostition)
    {
        PoolManager.Instance.CreatePool(gameObj, 1);
        PoolManager.Instance.ReuseObject(gameObj, objectPostition, Quaternion.identity);
    }

    public void Upgrade(GameObject oldObject, GameObject newObject)
    {
        Vector3 spawnPos = oldObject.transform.position;
        PoolManager.Instance.CreatePool(newObject, 1);
        PoolManager.Instance.ReuseObject(newObject, spawnPos, Quaternion.identity);
        oldObject.SetActive(false);
    }
}
