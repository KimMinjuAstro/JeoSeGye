using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerController Player;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
}
