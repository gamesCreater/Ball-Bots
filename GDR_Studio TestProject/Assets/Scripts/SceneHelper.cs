using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Вспомогательный универсальный класс в котором много кнопок
public class SceneHelper : MonoBehaviour
{
    int sceneIndex;   

    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void NextLvl()
    {
        Time.timeScale = 1f;
        
        LevelController.instance.IsEndGame();              
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void Restart()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(sceneIndex);        
    }

    public void Back()
    {
        SceneManager.LoadScene(sceneIndex - 1);
    }

    public void ChooseLevel()
    {
        SceneManager.LoadScene(1);
    }   
    public void Lvl1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Lvl2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void Lvl3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void Lvl4()
    {
        SceneManager.LoadScene("Level4");
    }
}
