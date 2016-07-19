using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TargetTurretsTest : MonoBehaviour
{
    public Transform target;
    public bool targetAir;
    public bool targetGround;

    public List<Transform> mobsInCollider;

    void Start()
    {
        mobsInCollider = new List<Transform>();
    }

    void Update()
    {
        if(target == null)
        {
            FindTarget();
        }
        else
        {
            if(target.GetComponent<Mobs>().mobIsDead == true)
            {
                mobsInCollider.Remove(target);
                target = null;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (targetGround)
        {
            if (other.gameObject.tag == "GroundMobs")
            {
                mobsInCollider.Add(other.transform);
            }
        }
        if (targetAir)
        {
            if (other.gameObject.tag == "AirMobs")
            {
                mobsInCollider.Add(other.transform);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (targetGround)
        {
            if (other.gameObject.tag == "GroundMobs")
            {
                if (other.transform == target)
                {
                    target = null;
                }
                mobsInCollider.Remove(other.transform);
            }
        }
        if (targetAir)
        {
            if (other.gameObject.tag == "AirMobs")
            {
                if (other.transform == target)
                {
                    target = null;
                }
                mobsInCollider.Remove(other.transform);
            }
        }
    }

    private void FindTarget()
    {
        foreach (Transform w in mobsInCollider)
        {
            if (w != null)
            {
                target = w;
            }
            else
            {
                mobsInCollider.Remove(w);
            }
        }
    }
    /*
public List<Collider> mobsInCollider;

void Start()
{
   mobsInCollider = new List<Collider>();
}

void Update()
{
   if (!target)
   {
       FindTarget();
   }
   else
   {
       if(target.gameObject.GetComponent<Mobs>().mobIsDead || target.gameObject.GetComponent<Collider>().enabled == false)
       {
           mobsInCollider.Remove(target.gameObject.GetComponent<Collider>());
           target = null;
       }
   }
}

void OnTriggerEnter(Collider other)
{
   if (targetGround)
   {
       if (other.gameObject.tag == "GroundMobs")
       {
           mobsInCollider.Add(other);
       }
   }
   if (targetAir)
   {
       if (other.gameObject.tag == "AirMobs")
       {
           mobsInCollider.Add(other);
       }
   }
}

void OnTriggerExit(Collider other)
{
   if (targetGround)
   {
       if (other.gameObject.tag == "GroundMobs")
       {
           if(other.transform == target)
           {
               target = null;
           }
           mobsInCollider.Remove(other);
       }
   }
   if (targetAir)
   {
       if (other.gameObject.tag == "AirMobs")
       {
           if (other.transform == target)
           {
               target = null;
           }
           mobsInCollider.Remove(other);
       }
   }
}

public void FindTarget()
{
   foreach(Collider w in mobsInCollider)
   {
       if (w != null && w.enabled == true && w.gameObject.GetComponent<Mobs>().mobIsDead == false)
       {
           target = w.gameObject.transform;
       }
       else
       {
           mobsInCollider.Remove(w);
       }
   }
}*/
}
