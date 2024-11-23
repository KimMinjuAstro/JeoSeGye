using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private PlayerController Player;
    public float moveSpeed = 1.5f;
    public float attackSpeed = 1.0f;
    bool isAttack = false;
    private Animator anim;

    private void Start()
    {
        Player = PlayerManager.instance.Player;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Player == null) return;

        if (Vector3.Distance(transform.position, Player.transform.position) > 0.5f)
        {
            //이동
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Time.deltaTime * moveSpeed);
        }
        else
        {
            if(!isAttack)
            {
                isAttack = true;
                //공격
                anim.SetTrigger("isAttack");
                Invoke("AttackSpeed", attackSpeed);
            }

        }


    }

    private void AttackSpeed()
    {
        isAttack = false;
    }
}
