using UnityEngine;
using System.Collections;

public class Projectile : PoolObject {

    public float speed = 10;
    public float range = 10;
    public int damage;
    public float dist;

    public virtual void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        dist += Time.deltaTime * speed;

        if (dist >= range)
        {
            Explode();
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GroundMobs" || other.gameObject.tag == "AirMobs")
        {
            Explode();
            if (other.GetComponent<Mobs>().mobIsDead == false)
            {
                other.GetComponent<Mobs>().IncomingDamage(damage);
            }
        }
    }

    public virtual void Explode()
    {
        Destroy();
        dist = 0;
    }
}
