// using System.Collections;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using TMPro;
// using UnityEngine;
// using UnityEngine.Serialization;
// using UnityEngine.UI;
//
//
// // 대화 데이터를 저장할 구조체
// [System.Serializable]
// public class Dialogue
// {
//     public string Speaker;
//     public string DialogueString;
// }
//
// [System.Serializable]
// public class CutsceneData
// {
//     public string Scene;
//     public string Description;
//     public Dialogue[] Dialogues;
// }
//
// // 컷씬 매니저 스크립트
// public class CutsceneManager : MonoBehaviour
// {
//     [SerializeField] private TextMeshProUGUI speakerText;  // 화자 텍스트
//     [SerializeField] private TextMeshProUGUI dialogueText; // 대사 텍스트
//     [SerializeField] private GameObject dialoguePanel;     // 대화창 패널
//     [SerializeField] private float typingSpeed = 0.05f;    // 타이핑 효과 속도
//     
//     private CutsceneData[] cutscenes;
//     private int currentSceneIndex = 0;
//     private int currentDialogueIndex = 0;
//     private bool isTyping = false;
//     private Coroutine typingCoroutine;
//
//     private void Start()
//     {
//         LoadCutsceneData();
//         dialoguePanel.SetActive(false);
//         StartCutscene();
//     }
//
//     private void LoadCutsceneData()
//     {
//         TextAsset jsonFile = Resources.Load<TextAsset>("CutSceneDialogueJSON");
//         cutscenes = JsonUtility.FromJson<CutsceneData[]>(jsonFile.text);
//     }
//
//     public void StartCutscene(int sceneIndex)
//     {
//         currentSceneIndex = sceneIndex;
//         currentDialogueIndex = 0;
//         dialoguePanel.SetActive(true);
//         DisplayNextDialogue();
//     }
//
//     public void DisplayNextDialogue()
//     {
//         if (isTyping)
//         {
//             // 타이핑 중이면 즉시 전체 텍스트 표시
//             if (typingCoroutine != null)
//                 StopCoroutine(typingCoroutine);
//             dialogueText.text = cutscenes[currentSceneIndex].Dialogues[currentDialogueIndex].DialogueString;
//             isTyping = false;
//             return;
//         }
//
//         if (currentDialogueIndex >= cutscenes[currentSceneIndex].Dialogues.Length)
//         {
//             EndCutscene();
//             return;
//         }
//
//         Dialogue currentDialogue = cutscenes[currentSceneIndex].Dialogues[currentDialogueIndex];
//         speakerText.text = currentDialogue.Speaker;
//         typingCoroutine = StartCoroutine(TypeDialogue(currentDialogue.DialogueString));
//         currentDialogueIndex++;
//     }
//
//     private IEnumerator TypeDialogue(string dialogue)
//     {
//         isTyping = true;
//         dialogueText.text = "";
//         
//         foreach (char letter in dialogue.ToCharArray())
//         {
//             dialogueText.text += letter;
//             yield return new WaitForSeconds(typingSpeed);
//         }
//         
//         isTyping = false;
//     }
//
//     private void EndCutscene()
//     {
//         dialoguePanel.SetActive(false);
//         // 여기에 컷씬 종료 후 실행할 로직 추가
//     }
//
//     // 씬 전환이나 특정 이벤트 발생 시 호출할 메서드들
//     public void OnSceneTransition()
//     {
//         // 씬 전환 효과나 페이드 효과 등을 추가
//     }
//
//     public void OnSpecialEvent()
//     {
//         // 특수 이벤트(예: 카메라 효과, 파티클 등) 실행
//     }
// }