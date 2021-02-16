using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ����� �������������� ����������� �������
public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    int sceneIndex;
    int levelComplete;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelComplete = PlayerPrefs.GetInt("LevelComplete");
    }


    public void IsEndGame()
    {
        //�������� ������ ����� � � ���������� levelComplete ���������� ������ ����������� (������)

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelComplete = PlayerPrefs.GetInt("LevelComplete");

        if (sceneIndex == 6)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            //���� ������ (������) ����������� ������ ������� �����, �� ������ ��������� �� ������� � ��������� ���� �����

            if (levelComplete < sceneIndex)
            {
                PlayerPrefs.SetInt("LevelComplete", sceneIndex);
                
                SceneManager.LoadScene(sceneIndex + 1);                
            }
        }
    }

    

    
}
