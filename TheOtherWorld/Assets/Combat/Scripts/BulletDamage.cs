using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    private float dmg;

    public void Init(float dmg)
    {
        this.dmg = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<OnHit>(out OnHit hit))
        {
            hit.TakeDamage(dmg);
            hit.GetComponentInChildren<HpBar>().HpBarDamage();
            transform.GetComponent<FireBall>().Attack();

            if (transform.GetComponent<FireBall>() != null)
            {
                transform.position = hit.transform.position;    
                transform.GetComponent<FireBall>().Attack();
            }
        }
    }
}

