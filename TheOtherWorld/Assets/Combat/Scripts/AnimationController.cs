using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator anim;
    PlayerController pc;

    private void Start()
    {
        anim = GetComponent<Animator>();
        pc = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (pc.dir == Vector3.zero)
        {
            AnimationChange("isIDLE");
        }
        else
        {
            AnimationChange("isMOVE");
        }

        if (pc.dir.x < 0)
        {
            transform.localScale = new Vector3(-1, 1,1);
        }
        else if(pc.dir.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void AnimationChange(string temp)
    {
        anim.SetBool("isMOVE", false);
        anim.SetBool("isIDLE", false);

        if (temp == "isAttack" || temp == "isSkill")
        {
            anim.SetTrigger(temp);
            return;
        }

        anim.SetBool(temp, true);
    }

}
