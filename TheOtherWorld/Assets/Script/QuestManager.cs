using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public class Quest
    {
        public int QuestIndex;  // 단순 열거
        public int QuestType;   // 0은 일반몹, 1은 중간몹, 2는 천사몹
        public int QuestExp;    // 퀘스트 성공 경험치
        public int QuestGiftNumber; // 퀘스트 성공시 Gift 선택수

    }
}
