using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator anim;
    PlayerController pc;
    public GameObject baseAttack;
    //public 
    
    public float baseAttackSpeed = 1f;
    private float baseAttackTimer = 0f;
    private bool isAttack;

    private void Start()
    {
        anim = GetComponent<Animator>();
        pc = GetComponent<PlayerController>();
    }

    public void AttackStart()
    { 
        isAttack = true;
        baseAttack.gameObject.SetActive(true);
    }

    public void AttackEnd()
    { 
        isAttack = false;
        baseAttack.gameObject.SetActive(false);
    }

    private void Update()
    {
        Animation_Controller();
        Filp();
    }

    private void Animation_Controller()
    {
        baseAttackTimer += Time.deltaTime;

        if (baseAttackTimer >= baseAttackSpeed)
        {
            baseAttackTimer = 0f;
            SFXManager.instance.PlaySFX(0);
            anim.SetTrigger("isAttack");
        }

        if (!isAttack)
        { 
            if (pc.dir == Vector3.zero)
            {
                AnimationChange("isIDLE");
            }
            else
            {
                AnimationChange("isMOVE");
            }        
        }

    }

    private void Filp()
    {
        if (pc.dir.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (pc.dir.x > 0)
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
