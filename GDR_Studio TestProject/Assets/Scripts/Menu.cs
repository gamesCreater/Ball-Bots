using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    int levelComplete;

    [SerializeField] Button ButLevel2;
    [SerializeField] Button ButLevel3;
    [SerializeField] Button ButLevel4;

    private void Start()
    {
        levelComplete = PlayerPrefs.GetInt("LevelComplete");

        ButLevel2.interactable = false;
        ButLevel3.interactable = false;
        ButLevel4.interactable = false;

        OpenLevelCheck();
    }

    //ѕроверка прохождени€ уровней и активаци€ соответственных кнопок
    public void OpenLevelCheck()
    {
        levelComplete = PlayerPrefs.GetInt("LevelComplete");

        switch (levelComplete)
        {            
            case 3:
                ButLevel2.interactable = true;
                break;

            case 4:
                ButLevel2.interactable = true;
                ButLevel3.interactable = true;
                break;
            case 5:
                ButLevel2.interactable = true;
                ButLevel3.interactable = true;
                ButLevel4.interactable = true;
                break;
        }
    }

    //—брос прохождени€
    public void ResetLvl()
    {
        ButLevel2.interactable = false;
        ButLevel3.interactable = false;
        ButLevel4.interactable = false;
        PlayerPrefs.DeleteAll();
    }
}
