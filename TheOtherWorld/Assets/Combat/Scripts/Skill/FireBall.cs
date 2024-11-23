using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 1.0f;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        //Destroy(gameObject, 5.0f);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

    }

    public void Attack()
    {
        anim.SetTrigger("isAttack");
        Destroy(gameObject, .5f);
    }
}
