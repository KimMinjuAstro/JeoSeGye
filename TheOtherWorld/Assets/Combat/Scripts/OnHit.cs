using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHit : MonoBehaviour
{
    public float hp;
    public float maxHp;

    private void Start()
    {
        maxHp = hp;
    }

    public void TakeDamage(float dmg)
    { 
        hp -= dmg;

        if (hp <= 0)
        { 
            Destroy(gameObject);
        }
    }
}
