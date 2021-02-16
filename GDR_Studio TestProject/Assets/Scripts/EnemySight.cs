using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// область видимости АИ
public class EnemySight : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TextForTask;
    [SerializeField] TextMeshProUGUI TextWinOrLoose;
    [SerializeField] GameObject RestartBut;

    Transform player;

    bool findPlayerOnAwake = true;

    float angleSight = 30f;
    float distanceSight = 4f;

    Vector3 dir;
    float angle;

    CharacterController CC;

    private void Awake()
    {
        //пока игрока нет на сцене, объект его ищет

        if (findPlayerOnAwake)
        {
            StartCoroutine(FindPlayer());
        }
    }

    private void Update()
    {
        //пока игрока нет на сцене, объект его ищет
        if (!player) return;
        
        // два состояния. 1) Игрок в поле зрения и не в закрытом состоянии - проиграл
        //2) Игрок в поле зрения, но в закрытом состоянии движется - проиграл
        if ((InSight() && !CC._isClose) || (InSight() && CC._uncovered))
        {
            Loose();
        }
    }

    IEnumerator FindPlayer()
    {
        while (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            CC = player.GetComponent<CharacterController>();

            yield return new WaitForSeconds(0.5f);
        }
    }

    public bool CheckAngle()
    {
        dir = player.position - transform.position;
        dir.y = 0f;
        angle = Vector3.Angle(new Vector3(transform.forward.x, 0, transform.forward.z), dir);

        return angle < angleSight;
    }

    public bool CheckDistance()
    {
        return (player.position - transform.position).magnitude < distanceSight;
    }

    //В поле зрении попадает если угол между forward АИ и местоположением игрока не больше 30 на определенной дистанции

    public bool InSight()
    {
        return CheckAngle() && CheckDistance();
    }

    public void Loose()
    {
        TextForTask.gameObject.SetActive(false);
        TextWinOrLoose.gameObject.SetActive(true);
        TextWinOrLoose.text = "YOU Loose. Try Again.";
        RestartBut.gameObject.SetActive(true);

        Time.timeScale = 0f;
    }

}
