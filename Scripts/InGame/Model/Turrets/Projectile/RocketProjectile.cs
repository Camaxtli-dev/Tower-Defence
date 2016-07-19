using UnityEngine;
using System.Collections;

public class RocketProjectile : Projectile {
    public Transform target;

    public override void Update()
    {
        base.Update();

        if (!target.gameObject.GetComponent<Mobs>().mobIsDead)
        {
            transform.LookAt(target.position);
        }
        else
        {
            target = null;
            Explode();
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AirMobs" && other.transform == target)
        {
            Explode();
            if (other.GetComponent<Mobs>().mobIsDead == false)
            {
                other.GetComponent<Mobs>().IncomingDamage(damage);
            }
            target = null;
        }
    }
}
