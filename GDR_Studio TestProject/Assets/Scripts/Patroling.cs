using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroling : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] GameObject[] movePoints;
    int counter = 0;

    private void Update()
    {

        //в массив кладем точки на которые должен идти АИ. Меняем его transform.position в направлении следующей точки. 
        //По достижении ищем следующую точку.

        if (movePoints.Length > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoints[counter].transform.position, speed * Time.deltaTime);
            
            transform.LookAt(movePoints[counter].transform.position);

            if ((Vector3.Distance(transform.position, movePoints[counter].transform.position)) < 0.2f)
            {
                CounterPoints();
            }
        }        
    }

    private int CounterPoints()
    {
        counter++;
        if (counter == movePoints.Length)
        {
            counter = 0;
        }
        return counter;
    }



}
