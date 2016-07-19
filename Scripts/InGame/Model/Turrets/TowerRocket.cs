using UnityEngine;
using System.Collections;

public class TowerRocket : TurretBase {

    protected override void fireProjectile()
    {
        nextFireTime = Time.time + reloadTime;
        positionOfShot = Random.Range(0, effectOfShotPosition.Length);
        GameObject obj = PoolManager.Instance.ReturnReuseObject(projectile, effectOfShotPosition[positionOfShot].position, effectOfShotPosition[positionOfShot].rotation);
        obj.GetComponent<RocketProjectile>().target = target;
        PoolManager.Instance.ReuseObject(effectOfShot, effectOfShotPosition[positionOfShot].position, effectOfShotPosition[positionOfShot].rotation);
    }
}