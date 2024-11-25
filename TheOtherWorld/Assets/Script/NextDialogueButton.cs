using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NextDialogueButton : MonoBehaviour
{
    private Button dialogueButton;
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;
    public Button sceneChangerButton;
    
    // Start is called before the first frame update
    void Start()
    {
        dialogueButton = GetComponent<Button>();
        dialogueButton.onClick.AddListener(OnButtonClicked);
    }
    //
    // // Update is called once per frame
    // void Update()
    // {
    //     
    // }

    private void OnButtonClicked()
    {
        speakerText.text = "세계의 신";
        dialogueText.text = "이 침공을 막기 위해 나는 주인공을 대리자로 선택한다.";
        Destroy(gameObject);
        sceneChangerButton.gameObject.SetActive(true);
    }
}
