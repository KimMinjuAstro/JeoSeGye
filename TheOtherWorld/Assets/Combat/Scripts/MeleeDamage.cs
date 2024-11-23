using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : Weapon 
{
    public float dmg = 0f;
    private Collider2D cd;

    private void Awake()
    {
        cd = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        cd.enabled = true;
    }

    private void OnDisable()
    {
        cd.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<OnHit>(out OnHit hit))
        { 
            hit.TakeDamage(dmg);
            hit.GetComponentInChildren<HpBar>().HpBarDamage();
        }
    }
}
