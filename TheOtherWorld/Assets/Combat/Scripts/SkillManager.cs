using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    protected Monster[] monsters { get { return TargetManager.instance.monsters.ToArray(); } }
    public PlayerController player;

    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;

    public bool windStorm;         // ���� ����
    public bool moon;              // ����
    public bool darknessExplosion; // ��� ����
    public bool fireArrow;         // �� ȭ��
    public bool iceArrow;          // �� ȭ��  
    public bool fireBall;          // ȭ�̾ ??

    [Header("��ũ")]
    public GameObject toromePrefabs;
    public Sprite toromeImage;
    public float toromeDuration = 1.0f;
    public float toromeTimer = 0.0f;
    public bool isTorome = false;
    public float toromeDmg = 10f;
    public bool toromeImagebool = false;

    [Header("����")]
    public GameObject windStormPrefabs;
    public Sprite windStormImage;
    public float windStormDuration = 1.0f;
    public float windStormTimer = 0.0f;
    public bool isWindStorm = false;
    public float windStormDmg = 10f;
    public bool windStormImagebool = false;

    [Header("����")]
    public GameObject moonPrefabs;
    public Sprite moonImage;
    public float moonDuration = 1.0f;
    public float moonTimer = 0.0f;
    public bool isMoon = false;
    public float mooonDmg = 10f;
    public bool moonImagebool = false;

    [Header("���̽�")]
    public GameObject icePrefabs;
    public Sprite iceImage;
    public float iceDuration = 1.0f;
    public float iceTimer = 0.0f;
    public bool isIce = false;
    public float iceDmg = 10f;
    public bool iceImagebool = false;


    [Header("���̾�")]
    public GameObject firePrefabs;
    public Sprite fireImage;
    public float fireDuration = 1.0f;
    public float fireTimer = 0.0f;
    public bool isFire = false;
    public float fireDmg = 10f;
    public bool fireImagebool = false;

    [Header("���̾�2")]
    public GameObject fire2Prefabs;
    public Sprite fire2Image;
    public float fire2Duration = 1.0f;
    public float fire2Timer = 0.0f;
    public bool isFire2 = false;
    public float fire2Dmg = 10f;
    public bool fire2Imagebool = false;

    public void SpriteIcon(Sprite sprite)
    {
        if (image1.enabled == false)
        {
            image1.sprite = sprite;
            image1.enabled = true;
        }
        else if (!image2.enabled)
        {
            image2.sprite = sprite;
            image2.enabled = true;
        }
        else if (!image3.enabled)
        {
            image3.sprite = sprite;
            image3.enabled = true;
        }
        else if(!image4.enabled)
        {
            image4.sprite = sprite;
            image4.enabled = true;
        }
    }


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
        if (!toromeImagebool)
        {
            toromeImagebool = true;
            SpriteIcon(toromeImage);
        }

        toromeTimer += Time.deltaTime;

        if (toromeTimer >= toromeDuration)
        {
            if (isTorome) return;

            isTorome = true;
            StartCoroutine(ToromeEnd());

            if (monsters.Length <= 0)
            {
                //Debug.Log("��ó�� ���� ����");
                return;
            }

            Monster monster = FindClosestTarget();

            Vector2 direction = (monster.transform.position - player.transform.position).normalized;
            var go = Instantiate(toromePrefabs, player.transform.position, Quaternion.identity);

            // �߻�ü ȸ�� ���� (�� ��������)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // ���� ���
            go.transform.rotation = Quaternion.Euler(0, 0, angle);
            go.GetComponent<BulletDamage>().Init(fireDmg);

            toromeTimer = 0;

        }
    }
    private void SkillMoon()
    {
        if (!moonImagebool)
        {
            moonImagebool = true;
            SpriteIcon(moonImage);
        }
        moonTimer += Time.deltaTime;

        if (moonTimer >= moonDuration)
        {
            if (isMoon) return;

            isMoon = true;
            StartCoroutine(MoonEnd());

            if (monsters.Length <= 0)
            {
                //Debug.Log("��ó�� ���� ����");
                return;
            }

            Monster monster = FindClosestTarget();
            Vector2 direction = Vector2.zero;
            if (monster != null)
                direction = (monster.transform.position - player.transform.position).normalized;
            
            var go = Instantiate(moonPrefabs, player.transform.position, Quaternion.identity);

            // �߻�ü ȸ�� ���� (�� ��������)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // ���� ���
            go.transform.rotation = Quaternion.Euler(0, 0, angle);
            go.GetComponent<BulletDamage>().Init(mooonDmg);

            toromeTimer = 0;

        }
    }

    private void Skill_Fire()
    {
        if (!fireImagebool)
        { 
            fireImagebool = true;
            SpriteIcon(fireImage);
            
        }


        fireTimer += Time.deltaTime;

        if (fireTimer >= fireDuration)
        {
            if (isFire) return;

            isFire = true;
            StartCoroutine(FireEnd());

            if (monsters.Length <= 0)
            {
                //Debug.Log("��ó�� ���� ����");
                return;
            }

            Monster monster = FindClosestTarget();

            Vector2 direction = (monster.transform.position - player.transform.position).normalized;

            var go = Instantiate(firePrefabs, player.transform.position, Quaternion.identity);

            // �߻�ü ȸ�� ���� (�� ��������)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // ���� ���
            go.transform.rotation = Quaternion.Euler(0, 0, angle);
            go.GetComponent<BulletDamage>().Init(fireDmg);

            fireTimer = 0;

        }
    }

    private void Skill_Fire2()
    {
        if (!fire2Imagebool)
        {
            fire2Imagebool = true;
            SpriteIcon(fire2Image);

        }

        fire2Timer += Time.deltaTime;

        if (fire2Timer >= fire2Duration)
        {
            if (isFire2) return;

            isFire2 = true;
            StartCoroutine(IceFire2());

            if (monsters.Length <= 0)
            {
                //Debug.Log("��ó�� ���� ����");
                return;
            }

            fire2Timer = 0;
            Monster monster = FindClosestTarget();

            if (monster == null) return;

            Vector2 direction = (monster.transform.position - player.transform.position).normalized;

            var go = Instantiate(fire2Prefabs, player.transform.position, Quaternion.identity);

            // �߻�ü ȸ�� ���� (�� ��������)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // ���� ���
            go.transform.rotation = Quaternion.Euler(0, 0, angle);
            go.GetComponent<BulletDamage>().Init(fire2Dmg);


        }
    }

    private IEnumerator ToromeEnd()
    {
        SpriteIcon(toromeImage);
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
        if (!iceImagebool)
        { 
            iceImagebool = true;
            SpriteIcon(iceImage);
        
        }
        iceTimer += Time.deltaTime;

        if (iceTimer >= iceDuration)
        {
            if (isIce) return;

            isIce = true;
            StartCoroutine(IceEnd());

            if (monsters.Length <= 0)
            {
                //Debug.Log("��ó�� ���� ����");
                return;
            }

            Monster monster = FindClosestTarget();

            Vector2 direction = (monster.transform.position - player.transform.position).normalized;

            var go = Instantiate(icePrefabs, player.transform.position, Quaternion.identity);

            // �߻�ü ȸ�� ���� (�� ��������)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // ���� ���
            go.transform.rotation = Quaternion.Euler(0, 0, angle);
            go.GetComponent<BulletDamage>().Init(iceDmg);

            iceTimer = 0;

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
            //Debug.Log($"����: {target.name}, �Ÿ�: {distance}");
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
        if (!windStormImagebool)
        {
            windStormImagebool = true;
            SpriteIcon(windStormImage);
        }
        windStormTimer += Time.deltaTime;

        if (windStormTimer >= windStormDuration)
        {
            if (isWindStorm) return;

            isWindStorm = true;
            StartCoroutine(WindStormEnd());


            if (monsters.Length <= 0)
            {
                //Debug.Log("��ó�� ���� ����");
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
