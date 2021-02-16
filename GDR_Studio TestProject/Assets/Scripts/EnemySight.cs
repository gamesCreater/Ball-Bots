using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// ������� ��������� ��
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
        //���� ������ ��� �� �����, ������ ��� ����

        if (findPlayerOnAwake)
        {
            StartCoroutine(FindPlayer());
        }
    }

    private void Update()
    {
        //���� ������ ��� �� �����, ������ ��� ����
        if (!player) return;
        
        // ��� ���������. 1) ����� � ���� ������ � �� � �������� ��������� - ��������
        //2) ����� � ���� ������, �� � �������� ��������� �������� - ��������
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

    //� ���� ������ �������� ���� ���� ����� forward �� � ��������������� ������ �� ������ 30 �� ������������ ���������

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
