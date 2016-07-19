using UnityEngine;
using System.Collections;
using System;

public class Mobs : CObject
{
    private int idWaypoint;
    public Transform mainAim;
    public Transform main;
    public int myCashValue;
    public int myScore;
    public bool isRun = true;

    public Transform target;
    private Transform _myTransform;
    private static readonly Vector3 forward = Vector3.forward;
    private Quaternion mainAimRotation;

    public bool mobIsDead = false;

    void Start()
    {
        _myTransform = GetComponent<Transform>();
    }

    void Update()
    {
        findWaypoints();
        mainAim.LookAt(target);
        Move();
        SetRotation();
        main.rotation = Quaternion.Lerp(main.rotation, mainAimRotation, Time.deltaTime * (_moveSpeed + 500));
    }

    public override void OnObjectReuse()
    {
        base.OnObjectReuse();
        idWaypoint = 0;
        myScore = _hp / 10;
        mobIsDead = false;
        target = null;
    }

    public void SetRotation()
    {
        mainAimRotation.x = 0;
        mainAimRotation.y = mainAim.rotation.y;
        mainAimRotation.z = 0;
        mainAimRotation.w = mainAim.rotation.w;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Waypoint")
        {
            if (other.gameObject.name == idWaypoint.ToString())
            {
                idWaypoint++;
                findWaypoints();
            }
        }
    }

    public void Move()
    {
        _myTransform.Translate(forward * (moveSpeed * Time.deltaTime));
    }

    public void findWaypoints()
    {
        if (idWaypoint >= GameMaster.Instance.waypoints.Count)
        {
            idWaypoint = 0;
        }
        foreach (Transform w in GameMaster.Instance.waypoints)
        {
            if (w.name == idWaypoint.ToString())
            {
                target = w;
            }
        }
    }

    public override void Die()
    {
        mobIsDead = true;
        base.Die();
        GameMaster.Instance.cashCount += myCashValue;
        GameMaster.Instance.scoreCount += myScore;
        GameMaster.Instance.enemyCount--;
        GameMaster.Instance.UpdateHUD();
    }
}
