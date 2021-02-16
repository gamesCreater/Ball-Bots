using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThirdLvl : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TextWinOrLoose;
    [SerializeField] TextMeshProUGUI TextForTask;
    [SerializeField] TextMeshProUGUI CrystalCounter;
    [SerializeField] GameObject NextLvlBut;

    bool SecondTaskCheck = true;

    private void Awake()
    {
        Time.timeScale = 1f;
    }
    void Start()
    {
        TextWinOrLoose.text = " Level 3 ";
        StartCoroutine(FirstTask());
        CounterCrystal.counterPoints = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CrystalCounter.text = CounterCrystal.counterPoints.ToString();

        if (CounterCrystal.counterPoints == 2 && SecondTaskCheck)
        {
            StartCoroutine(SecondTask());
            SecondTaskCheck = false;
        }

        if (CounterCrystal.counterPoints == 3)
        {
            StartCoroutine(Win());
        }
    }

    //Блок первого задания для персонажа
    IEnumerator FirstTask()
    {
        yield return new WaitForSeconds(2f); 
        TextWinOrLoose.gameObject.SetActive(false);
        TextForTask.gameObject.SetActive(true);
        TextForTask.text = "Now you need to collect 3 crystals for going to the next level";
        yield return new WaitForSeconds(5f);
        TextForTask.text = "But be careful, you must hide from enemy bots";
        yield return new WaitForSeconds(5f);
        TextForTask.gameObject.SetActive(false);
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

    IEnumerator SecondTask()
    {
        TextForTask.gameObject.SetActive(true);
        TextForTask.text = "You need to know about one trick";

        yield return new WaitForSeconds(5f);

        TextForTask.text = "when you in the ball and without moving you are hidden";

        yield return new WaitForSeconds(5f);

        TextForTask.text = "USE IT";
    }
}
