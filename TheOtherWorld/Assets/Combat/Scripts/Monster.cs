using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private PlayerController Player;
    public float moveSpeed = 1.5f;
    public float attackSpeed = 1.0f;
    public float targetRange = 1.0f;

    float mapCount = 1;
    bool isAttack = false;
    bool isDead = false;
    private Animator anim;


    public GameObject weapon;
    //public List<Weapon> weapon = new List<Weapon>();

    private void Start()
    {
        TargetManager.instance.monsters.Add(this);
        Player = PlayerManager.instance.Player;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Player == null) return;
        if (isDead) return;

        bool isFacingLeft = transform.localScale.x < 0;

        // 플레이어가 왼쪽에 있을 때 방향 변경
        if (Player.transform.position.x > transform.position.x && !isFacingLeft)
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if (Player.transform.position.x <= transform.position.x && isFacingLeft)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }

        if (Vector3.Distance(transform.position, Player.transform.position) > 0.5f)
        {
            if (mapCount == 1)
            {
                if (Vector3.Distance(transform.position, Player.transform.position) < targetRange)
                {
                    //이동
                    transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Time.deltaTime * moveSpeed);
                }
            }
            else
            { 
                //이동
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Time.deltaTime * moveSpeed);
            }
        }
        else
        {
            if(!isAttack)
            {
                isAttack = true;
                weapon.SetActive(true);
                weapon.GetComponent<Collider2D>().enabled = true;
                //공격
                anim.SetTrigger("isAttack");
                Invoke("AttackSpeed", attackSpeed);
            }

        }
    }

    private void AttackSpeed()
    {
        weapon.SetActive(false);
        weapon.GetComponent<Collider2D>().enabled = false;
        isAttack = false;
    }
}
