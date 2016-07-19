using UnityEngine;
using System.Collections;

public class EffectOfShot : PoolObject {

    public float liveTime; // 0.2f

    public override void OnObjectReuse()
    {
        Invoke("Destroy", liveTime);
    }
}
