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
            Destroy(gameObject);
        }
    }
}
