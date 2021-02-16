using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstLvl : MonoBehaviour
{
    [SerializeField] GameObject MainPanel;
    [SerializeField] GameObject PlayerPref;
    [SerializeField] GameObject PlaceResp;
    [SerializeField] TextMeshProUGUI TextForTask;
    [SerializeField] TextMeshProUGUI TextCrystalCount;
    [SerializeField] TextMeshProUGUI TextWinOrLoose;    
    [SerializeField] GameObject CheckPlace;
    [SerializeField] GameObject CheckPlace2;
    [SerializeField] GameObject CheckPlace3;
    [SerializeField] GameObject EnemyPref;
    [SerializeField] GameObject EnemyObj;
    

    bool firstOpen = false;
    bool oneTime = false;

    
    public static bool firstCheck = false;

    private void Update()
    {
        TextCrystalCount.text = CounterCrystal.counterPoints.ToString();

        if (Input.GetButtonDown("Open/Close") && !firstOpen )
        {
            firstOpen = true;
            TextForTask.gameObject.SetActive(false);
            StartCoroutine(SecondTask());
        }

        if(CounterCrystal.counterPoints == 1)
        {
            ThirdTask();
        }

        if (CounterCrystal.counterPoints == 2 && !oneTime)
        {
            oneTime = true;
            
            StartCoroutine(FourthTask());
        }

        if (firstCheck)
        {
            StartCoroutine(LastTask());
            firstCheck = false;
        }

        if (CounterCrystal.counterPoints == 3)
        {
            StartCoroutine(Win());
        }
    }

    public void Next()
    {
        MainPanel.SetActive(false);
        Instantiate(PlayerPref, PlaceResp.transform.position, Quaternion.identity);
        StartCoroutine(FirstTask());
    }

    IEnumerator FirstTask()
    {
        yield return new WaitForSeconds(2f);

        TextForTask.gameObject.SetActive(true);
    }

    IEnumerator SecondTask()
    {
        yield return new WaitForSeconds(1.5f);
        TextForTask.text = "Press 'W', 'A', 'D' for movement";
        TextForTask.gameObject.SetActive(true);
        CheckPlace.SetActive(true);        
    }

    IEnumerator FourthTask()
    {
        yield return new WaitForSeconds(0.5f);
        TextForTask.text = "Ok. Now we try to play SERIOUS";

        yield return new WaitForSeconds(5f);
        TextForTask.text = "this is an enemy bot. Try to hide from its gaze";

        EnemyObj.SetActive(true);

        yield return new WaitForSeconds(5f);
        TextForTask.text = "But at first check its gaze. Come to the bot";

    }

    public void ThirdTask()
    {
        CheckPlace2.SetActive(true);
        TextForTask.text = "Very slow? Please press 'Space' for boost";
    }

    IEnumerator LastTask()
    {
        TextForTask.text = "Ok. Try few times more and when you will be ready, take last crystal and go next";

        yield return new WaitForSeconds(2.5f);

        CheckPlace3.SetActive(true);

    }

    IEnumerator Win()
    {
        TextForTask.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        TextWinOrLoose.gameObject.SetActive(true);
        TextWinOrLoose.text = "YOU WIN !!!";
        Time.timeScale = 0f;
    }



    


}
