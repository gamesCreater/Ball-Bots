using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// то же что и EnemySight, только без проигрыша
public class EnemySightLVL1 : MonoBehaviour
{
    Transform player;

    bool findPlayerOnAwake = true;

    float angleSight = 30f;
    float distanceSight = 4f;

    Vector3 dir;
    float angle;

    CharacterController CC;

    [SerializeField]
    TextMeshProUGUI WarningText;

    bool oneTime = true;

    private void Awake()
    {
        if (findPlayerOnAwake)
        {
            StartCoroutine(FindPlayer());
        }
    }

    private void Update()
    {
        if (!player) return;
        
        if (InSight() && !CC._isClose)
        {
            WarningText.gameObject.SetActive(true);
            WarningText.text = " I see you!!! ";
            if (oneTime)
            {
                FirstLvl.firstCheck = true;
                oneTime = false;
            }
            
        }
        else
        {
            WarningText.gameObject.SetActive(false);
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

    public bool InSight()
    {
        return CheckAngle() && CheckDistance();
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;        
    //    Vector3 tmp = new Vector3(0,0,distanceSight);
    //    Gizmos.DrawRay(transform.position, tmp);
    //}

}
