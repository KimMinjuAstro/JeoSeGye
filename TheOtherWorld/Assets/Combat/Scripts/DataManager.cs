using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;   
        }
    }

    CharacterLevelSystem stat;

    public int level = 0;
    public int currentExp = 0;
    public int maxExp = 0;
    public int currentHp = 0;
    public float expPercentage = 0;




    private void Start()
    {
        stat = CharacterLevelSystem.Instance;

        stat.SetCharacter("C01");

        level = stat.GetCurrentLevel();
        currentExp = stat.GetCurrentExp();
        maxExp = stat.GetMaxExp();

        currentHp = stat.GetCurrentHp();
        expPercentage = stat.GetExpPercentage();

        /*
        stat.AddExperience(1000);
        level = stat.GetCurrentLevel();
        currentExp = stat.GetCurrentExp();
        maxExp = stat.GetMaxExp();
        currentHp = stat.GetCurrentHp();
        expPercentage = stat.GetExpPercentage();
        */
    }

    public void AddExperience(int exp)
    {
        stat.AddExperience(exp);
        level = stat.GetCurrentLevel();
        currentExp = stat.GetCurrentExp();
        maxExp = stat.GetMaxExp();
        currentHp = stat.GetCurrentHp();
        expPercentage = stat.GetExpPercentage();
    }
}
