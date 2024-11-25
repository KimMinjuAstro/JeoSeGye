using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHit : MonoBehaviour
{
    public float hp;
    public float maxHp;
    public int exp;

    private void Start()
    {
        maxHp = hp;
    }

    public void TakeDamage(float dmg)
    { 
        hp -= dmg;

        if (hp <= 0)
        {
            if (GetComponent<Monster>() != null)
            {
                SFXManager.instance.PlaySFX(4);
                MonsterSpawner.instance.SpawnerEenemy(this.gameObject.GetComponent<Monster>());
                DataManager.instance.AddExperience(exp);
                Destroy(gameObject);
            }

            if (GetComponent<PlayerController>() != null)
            { 
                // Á×À½
            }
        }
    }
}
