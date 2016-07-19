using UnityEngine;
using System.Collections;

public class AnimationControllerTurrets : MonoBehaviour {

    public Animator anim;

    void Start()
    {
        if (gameObject.GetComponent<TurretBase>().isAttack)
        {
            anim.SetBool("isAttack", true);
        }
        else
        {
            anim.SetBool("isAttack", false);
        }
    }

    void Update()
    {
        if (gameObject.GetComponent<TurretBase>().isAttack)
        {
            anim.SetBool("isAttack", true);
        }
        else
        {
            anim.SetBool("isAttack", false);
        }
    }
}
