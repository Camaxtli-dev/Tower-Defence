using UnityEngine;
using System.Collections;
using System;

public class PlayerBase : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "GroundMobs" || other.gameObject.tag == "AirMobs")
        {
            other.gameObject.GetComponent<Mobs>().myScore = 0;
            other.gameObject.GetComponent<Mobs>().myCashValue = 0;
            other.gameObject.GetComponent<Mobs>().Die();
            GameMaster.Instance.healthCount -= (int)other.GetComponent<Mobs>().OutgoingDamage(); // На тот случай если какой либо другой моб наносит больше дамага(босс к примеру)
            GameMaster.Instance.UpdateHUD();
        }
    }
}
