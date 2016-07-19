using UnityEngine;
using System.Collections;

public class FieldToBuild : MonoBehaviour {

    public GameObject myStructure;

    void Update()
    {
        if (myStructure != null)
        {
            if (myStructure.activeSelf == false)
            {
                myStructure = null;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "StructureTurrets")
        {
            myStructure = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject == myStructure)
        {
            myStructure = null;
        }
    }
}
