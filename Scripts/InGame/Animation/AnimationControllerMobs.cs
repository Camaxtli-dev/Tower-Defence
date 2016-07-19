using UnityEngine;
using System.Collections;

public class AnimationControllerMobs : MonoBehaviour {

    public Animator anim;

    void Start()
    {
        if (gameObject.GetComponent<Mobs>().isRun)
        {
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }
    }

    void Update()
    {
        if (gameObject.GetComponent<Mobs>().isRun)
        {
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }
    }
}
