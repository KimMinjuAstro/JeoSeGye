using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 1.0f;
    private Animator anim;
    private bool isAttack = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        Destroy(gameObject, 10.0f);
    }

    void Update()
    {
        if(!isAttack)
            transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void Attack()
    {
        isAttack = true;
        anim.SetTrigger("isAttack");
        Destroy(gameObject, .5f);
    }
}
