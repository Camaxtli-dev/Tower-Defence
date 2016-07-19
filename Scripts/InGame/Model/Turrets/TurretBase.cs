using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretBase : PoolObject
{
    public int price;

    public GameObject upgradeTurrets;
    public int upgradeTurretsCost = 0;
    public string upgradeTurretsName = "";

    public GameObject projectile;
    public float reloadTime = 1;
    public float turnSpeed = 0.5f;
    public float firePauseTime = 0.25f;
    public GameObject effectOfShot;
    public Transform target;
    public Transform[] effectOfShotPosition;
    public Transform turretHead;
    public Transform aimTurretHead;
    public Transform aimTurretBase;
    public Transform turretBase;

    protected float nextFireTime;
    protected float aimError;
    protected int positionOfShot = 0;

    public bool isAttack;

    void Start()
    {
        PoolManager.Instance.CreatePool(effectOfShot, 1);
        PoolManager.Instance.CreatePool(projectile, 1);
    }

    public void Update()
    {
        if (target)
        {
            isAttack = true;
            LookTarget();
            if (Time.time >= nextFireTime)
            {
                fireProjectile();
            }
        }
        else
        {
            isAttack = false;
        }

        TargetMobs();
    }

    public virtual void LookTarget()
    {
        aimTurretBase.LookAt(target);
        aimTurretBase.eulerAngles = new Vector3(0, aimTurretBase.eulerAngles.y, 0);
        aimTurretHead.LookAt(target);

        turretBase.rotation = Quaternion.Lerp(turretBase.rotation, aimTurretBase.rotation, Time.deltaTime * turnSpeed);
        turretHead.rotation = Quaternion.Lerp(turretHead.rotation, aimTurretHead.rotation, Time.deltaTime * turnSpeed);
    }

    protected virtual void fireProjectile()
    {
        nextFireTime = Time.time + reloadTime;
        PoolManager.Instance.ReuseObject(projectile, effectOfShotPosition[positionOfShot].position, effectOfShotPosition[positionOfShot].rotation);
        PoolManager.Instance.ReuseObject(effectOfShot, effectOfShotPosition[positionOfShot].position, effectOfShotPosition[positionOfShot].rotation);
    }

    private void TargetMobs()
    {
        target = gameObject.GetComponentInChildren<TargetTurretsTest>().target;
    }
}
