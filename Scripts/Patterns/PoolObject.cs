using UnityEngine;
using System.Collections;

public class PoolObject : MonoBehaviour {
    
    public virtual void OnObjectReuse() // Вызывается при использовании объекта
    {
    }
    
    public void Destroy()
    {
        gameObject.SetActive(false);
    }
}
