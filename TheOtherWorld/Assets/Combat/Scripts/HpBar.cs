using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public OnHit hit;
    private float hp;
    private float maxhp;
    private Image fillHpbar;

    private void Start()
    {
        hit = GetComponentInParent<OnHit>();
        hp = hit.hp;
        maxhp = hit.maxHp;

        fillHpbar = transform.GetChild(1).GetComponent<Image>();
        fillHpbar.fillAmount = hit.hp / hit.maxHp;
    }

    public void HpBarDamage()
    {
        fillHpbar.fillAmount = hit.hp / hit.maxHp;
    }
}
