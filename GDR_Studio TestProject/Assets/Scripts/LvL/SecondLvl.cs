using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SecondLvl : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TextWinOrLoose;
    [SerializeField] TextMeshProUGUI TextForTask;
    [SerializeField] TextMeshProUGUI CrystalCounter;
    [SerializeField] Button NextLvlBut;
    [SerializeField] CharacterController CC;
    [SerializeField] GameObject Crystal;

    bool FirstTaskRun = false;
    bool SecondTaskRun = false;



    private void Start()
    {
        TextWinOrLoose.text = " Level 2 ";
        StartCoroutine(FirstTask());
        CounterCrystal.counterPoints = 0;
    }

    private void Update()
    {
        CrystalCounter.text = CounterCrystal.counterPoints.ToString();

        if (CounterCrystal.counterPoints == 1 && !FirstTaskRun)
        {
            StartCoroutine(SecondTask());
            FirstTaskRun = true;
        }

        if (CC.SecondLvlBool && SecondTaskRun)
        {
            TextForTask.text = "Ok. Now just collect 3 crystal and we go to the next level";
        }
        
        if (CounterCrystal.counterPoints == 2)
        {
            Crystal.SetActive(true);
        }

        if (CounterCrystal.counterPoints == 3)
        {
            StartCoroutine(Win());
        }

    }

    IEnumerator FirstTask()
    {
        yield return new WaitForSeconds(2f);
        TextWinOrLoose.gameObject.SetActive(false);
        TextForTask.gameObject.SetActive(true);
        TextForTask.text = "Now you need to collect 3 crystals for going to the next level";

        yield return new WaitForSeconds(5f);
        TextForTask.text = "Go and collect 1st crystal. Get fast boost inside the ball - press 'Space' ";
    }

    IEnumerator SecondTask()
    {
        TextForTask.text = "May be you know, if you bump into wall when you in ball, you are open and transform to the bot";

        yield return new WaitForSeconds(5f);

        TextForTask.text = "Try to bump into wall";

        SecondTaskRun = true;
    }

    IEnumerator Win()
    {
        TextForTask.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        TextWinOrLoose.gameObject.SetActive(true);
        TextWinOrLoose.text = "YOU WIN !!!";
        NextLvlBut.gameObject.SetActive(true);

        Time.timeScale = 0f;
    }
}
