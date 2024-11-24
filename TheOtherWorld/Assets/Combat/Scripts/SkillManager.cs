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

    [Header("다크")]
    public GameObject toromePrefabs;
    public float toromeDuration = 1.0f;
    public float toromeTimer = 0.0f;
    public bool isTorome = false;
    public float toromeDmg = 10f;

    [Header("파이어")]
    public GameObject firePrefabs;
    public float fireDuration = 1.0f;
    public float fireTimer = 0.0f;
    public bool isFire = false;
    public float fireDmg = 10f;

    [Header("파이어2")]
    public GameObject fire2Prefabs;
    public float fire2Duration = 1.0f;
    public float fire2Timer = 0.0f;
    public bool isFire2 = false;
    public float fire2Dmg = 10f;


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

        if (fireBall)
        {
            Skill_Fire2();
        }

        if (fireArrow)
        {
            Skill_Fire();
        }

        if (darknessExplosion)
        {
            Skill_DarknessExplosion();
        }
    }

    private void Skill_DarknessExplosion()
    {
        toromeTimer += Time.deltaTime;

        if (toromeTimer >= toromeDuration)
        {
            if (isTorome) return;

            isTorome = true;
            StartCoroutine(ToromeEnd());

            if (monsters.Length <= 0)
            {
                //Debug.Log("근처에 적이 없음");
                return;
            }

            Monster monster = FindClosestTarget();

            Vector2 direction = (monster.transform.position - player.transform.position).normalized;
            var go = Instantiate(toromePrefabs, player.transform.position, Quaternion.identity);

            // 발사체 회전 설정 (적 방향으로)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 각도 계산
            go.transform.rotation = Quaternion.Euler(0, 0, angle);
            go.GetComponent<BulletDamage>().Init(fireDmg);

            toromeTimer = 0;

        }
    }

    private void Skill_Fire()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireDuration)
        {
            if (isFire) return;

            isFire = true;
            StartCoroutine(FireEnd());

            if (monsters.Length <= 0)
            {
                //Debug.Log("근처에 적이 없음");
                return;
            }

            Monster monster = FindClosestTarget();

            Vector2 direction = (monster.transform.position - player.transform.position).normalized;

            var go = Instantiate(firePrefabs, player.transform.position, Quaternion.identity);

            // 발사체 회전 설정 (적 방향으로)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 각도 계산
            go.transform.rotation = Quaternion.Euler(0, 0, angle);
            go.GetComponent<BulletDamage>().Init(fireDmg);

            fireTimer = 0;

        }
    }

    private void Skill_Fire2()
    {
        fire2Timer += Time.deltaTime;

        if (fire2Timer >= fire2Duration)
        {
            if (isFire2) return;

            isFire2 = true;
            StartCoroutine(IceFire2());

            if (monsters.Length <= 0)
            {
                //Debug.Log("근처에 적이 없음");
                return;
            }

            Monster monster = FindClosestTarget();

            Vector2 direction = (monster.transform.position - player.transform.position).normalized;

            var go = Instantiate(fire2Prefabs, player.transform.position, Quaternion.identity);

            // 발사체 회전 설정 (적 방향으로)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 각도 계산
            go.transform.rotation = Quaternion.Euler(0, 0, angle);
            go.GetComponent<BulletDamage>().Init(fire2Dmg);

            fire2Timer = 0;

        }
    }

    private IEnumerator ToromeEnd()
    {
        yield return new WaitForSeconds(.3f);
        isTorome = false;
    }


    private IEnumerator FireEnd()
    {
        yield return new WaitForSeconds(.3f);
        isFire = false;
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
                //Debug.Log("근처에 적이 없음");
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
                //Debug.Log("근처에 적이 없음");
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
            if (target == null) break;

            float distance = Vector2.Distance(player.transform.position, target.transform.position);
            //Debug.Log($"몬스터: {target.name}, 거리: {distance}");
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = target;
            }
        }

        return closest;
    }

    private void SkillWinStorm()
    {
        windStormTimer += Time.deltaTime;

        if (windStormTimer >= windStormDuration)
        {
            if (isWindStorm) return;

            isWindStorm = true;
            StartCoroutine(WindStormEnd());


            if (monsters.Length <= 0)
            {
                //Debug.Log("근처에 적이 없음");
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

        }
    }

    private IEnumerator IceFire2()
    {
        yield return new WaitForSeconds(.3f);
        isFire2 = false;
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
        yield return new WaitForSeconds(0.2f);
        isWindStorm = false;
    }
}
