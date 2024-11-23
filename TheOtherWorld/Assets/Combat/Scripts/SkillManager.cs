using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    protected Monster[] monsters { get { return TargetManager.instance.monsters.ToArray(); } }
    public PlayerController player;

    public bool windStorm;         // 윈드 스톰
    public bool moon;              // 위성
    public bool darknessExplosion; // 어둠 폭팔
    public bool fireArrow;         // 불 화살
    public bool iceArrow;          // 얼름 화살  
    public bool fireBall;          // 화이어볼 ??

    [Header("윈드")]
    public GameObject windStormPrefabs;
    public float windStormDuration = 1.0f;
    public float windStormTimer = 0.0f;
    public bool isWindStorm = false;
    public float windStormDmg = 10f;

    [Header("위성")]
    public GameObject moonPrefabs;
    public float moonDuration = 1.0f;
    public float moonTimer = 0.0f;
    public bool isMoon = false;
    public float mooonDmg = 10f;

    [Header("아이스")]
    public GameObject icePrefabs;
    public float iceDuration = 1.0f;
    public float iceTimer = 0.0f;
    public bool isIce = false;
    public float iceDmg = 10f;

    private void Start()
    {
        player = PlayerManager.instance.Player;
    }

    private void Update()
    {
        if (windStorm)
        {
            SkillWinStorm();
        }
        if (moon)
        {
            SkillMoon();
        }
        if (iceArrow)
        {
            Skill_IceArrow();
        }
    }

    private void Skill_IceArrow()
    {
        iceTimer += Time.deltaTime;

        if (iceTimer >= iceDuration)
        {
            if (isIce) return;

            isIce = true;
            StartCoroutine(IceEnd());

            if (monsters.Length <= 0)
            {
                Debug.Log("근처에 적이 없음");
                return;
            }

            Monster monster = FindClosestTarget();

            Vector2 direction = (monster.transform.position - player.transform.position).normalized;

            var go = Instantiate(icePrefabs, player.transform.position, Quaternion.identity);

            // 발사체 회전 설정 (적 방향으로)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 각도 계산
            go.transform.rotation = Quaternion.Euler(0, 0, angle);
            go.GetComponent<BulletDamage>().Init(iceDmg);

            iceTimer = 0;

        }
    }

    private void SkillMoon()
    {
        moonTimer += Time.deltaTime;

        if (moonTimer >= moonDuration)
        {
            if (isMoon) return;

            isMoon = true;

            if (monsters.Length <= 0)
            {
                Debug.Log("근처에 적이 없음");
                return;
            }

            for (int i = 0; i < monsters.Length; i++)
            {
                var go = Instantiate(windStormPrefabs, monsters[i].transform.position, Quaternion.identity);
                Destroy(go, 1f);
                monsters[i].GetComponent<OnHit>().TakeDamage(mooonDmg);
                monsters[i].GetComponentInChildren<HpBar>().HpBarDamage();
            }

            windStormTimer = 0;

            StartCoroutine(MoonEnd());
        }
    }

    private Monster FindClosestTarget()
    {
        float minDistance = Mathf.Infinity;
        Monster closest = null;

        foreach (Monster target in monsters)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = target;
            }
        }

        Debug.Log("??" + minDistance);
        return closest;
    }

    private void SkillWinStorm()
    {
        windStormTimer += Time.deltaTime;

        if (windStormTimer >= windStormDuration)
        {
            if (isWindStorm) return;

            isWindStorm = true;

            if (monsters.Length <= 0)
            {
                Debug.Log("근처에 적이 없음");
                return;
            }

            for (int i = 0; i < monsters.Length; i++)
            {
                var go = Instantiate(windStormPrefabs, monsters[i].transform.position, Quaternion.identity);
                Destroy(go, 1f);
                monsters[i].GetComponent<OnHit>().TakeDamage(windStormDmg);
                monsters[i].GetComponentInChildren<HpBar>().HpBarDamage();
            }

            windStormTimer = 0;

            StartCoroutine(WindStormEnd());
        }
    }

    private IEnumerator IceEnd()
    {
        yield return new WaitForSeconds(1.0f);
        isIce = false;
    }

    private IEnumerator MoonEnd()
    {
        yield return new WaitForSeconds(1.0f);
        isMoon = false;
    }

    private IEnumerator WindStormEnd()
    {
        yield return new WaitForSeconds(1.0f);
        isWindStorm = false;
    }
}
