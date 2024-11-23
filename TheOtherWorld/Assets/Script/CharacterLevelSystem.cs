using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
public class CharacterData
{
    public string CharacterId;  // 캐릭터 식별번호. 단순 열거
    public int CharacterType;   // 캐릭터 이름. 0만 있음
    public int CharacterLevel;  // 캐릭터 레벨
    public int CharacterMaxExp; // 캐릭터 레벨업시 필요경험치
    public int CharacterHp;     // 캐릭터 체력
    public int CharacterDamage; // 캐릭터 공격력
    public int CharacterSpeed;  // 캐릭터 이동속도
}

public class CharacterLevelSystem : MonoBehaviour
{
    private static CharacterLevelSystem instance;
    public static CharacterLevelSystem Instance
    {
        get
        {
            if (instance == null)
            {
                // Scene에서 찾아보기
                instance = FindObjectOfType<CharacterLevelSystem>();
                
                // Scene에 없다면 새로 생성
                if (instance == null)
                {
                    GameObject go = new GameObject("CharacterLevelSystem");
                    instance = go.AddComponent<CharacterLevelSystem>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);    // 중복된 인스턴스 제거
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);  // 씬 전환시에도 유지
        
        LoadCharacterData();
    }
    
    
    private List<CharacterData> characterDataList;
    private CharacterData currentCharacter;
    private int currentExp = 0;

    private void LoadCharacterData()
    {
        // Resources 폴더에서 JSON 파일 로드
        TextAsset jsonFile = Resources.Load<TextAsset>("CharacterDataTableJSON");
        if (jsonFile != null)
        {
            string jsonString = jsonFile.text;
            characterDataList = JsonUtility.FromJson<List<CharacterData>>(
                "{\"Items\":" + jsonString + "}"
            ).ToList();
        }
        else
        {
            Debug.LogError("리소스 폴더에 JSON 파일 없음");
        }
    }

    public void SetCharacter(string characterId)
    {
        currentCharacter = characterDataList.Find(x => x.CharacterId == characterId);
        if (currentCharacter == null)
        {
            Debug.LogError($"{characterId} ID를 가진 캐릭터 없음");
        }
    }

    public void AddExperience(int exp)
    {
        if (currentCharacter == null)
        {
            Debug.LogError("선택된 캐릭터 없음");
            return;
        }

        currentExp += exp;
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        if (currentExp >= currentCharacter.CharacterMaxExp)
        {
            // 다음 레벨 데이터 찾기
            CharacterData nextLevelData = characterDataList.Find(
                x => x.CharacterLevel == currentCharacter.CharacterLevel + 1
            );

            if (nextLevelData != null)
            {
                // 레벨업 처리
                int remainingExp = currentExp - currentCharacter.CharacterMaxExp;
                currentCharacter = nextLevelData;
                currentExp = remainingExp;
                
                OnLevelUp();
                
                // 남은 경험치로 추가 레벨업 가능한지 확인
                if (currentExp >= currentCharacter.CharacterMaxExp)
                {
                    CheckLevelUp();
                }
            }
        }
    }

    // 레벨업 시 필요한 추가 처리 구현
    private void OnLevelUp()
    {
        Debug.Log($"레벨 업! 현재 레벨: {currentCharacter.CharacterLevel}");
        Debug.Log($"현재 HP : {currentCharacter.CharacterHp}");
        Debug.Log($"현재 최대 HP: {currentCharacter.CharacterMaxExp}");
        Debug.Log($"현재 공격력: {currentCharacter.CharacterDamage}");
    }

    // 현재 캐릭터 정보 조회 메서드들
    public int GetCurrentLevel() => currentCharacter?.CharacterLevel ?? 0;
    public int GetCurrentExp() => currentExp;
    public int GetMaxExp() => currentCharacter?.CharacterMaxExp ?? 0;
    public int GetCurrentHp() => currentCharacter?.CharacterHp ?? 0;
    public float GetExpPercentage() => currentCharacter == null ? 0 : (float)currentExp / currentCharacter.CharacterMaxExp;
}