using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private PlayerController Player;

    private void Start()
    {
        Player = PlayerManager.instance.Player;
    }


}
