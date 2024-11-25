using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangerButton : MonoBehaviour
{
    public Button sceneChangerButton;
    
    // Start is called before the first frame update
    void Start()
    {
        sceneChangerButton = GetComponent<Button>();
        sceneChangerButton.onClick.AddListener(OnButtonClicked);
    }
    //
    // // Update is called once per frame
    // void Update()
    // {
    //     
    // }

    private void OnButtonClicked()
    {
        SceneManager.LoadScene("MergeScene");
    }
}
