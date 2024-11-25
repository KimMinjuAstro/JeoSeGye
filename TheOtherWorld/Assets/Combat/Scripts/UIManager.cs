using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public QuestPopUpHandler questPopUp;
    public GameObject ClearButton;

    private void Awake()
    {
        Instance = this;
    }


    public void Show()
    {
        questPopUp.Show();
        ClearButton.gameObject.SetActive(true);
    }
}


