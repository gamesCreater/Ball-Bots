using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObj : MonoBehaviour
{
    //GameObject cube;
    Vector3 offset = new Vector3(2, 0, 0);
    bool positive = false;
    

    private void Update()
    {
        if (positive)
        {
            transform.position = transform.position + offset * Time.deltaTime;
            
        }
        if (!positive)
        {
            transform.position = transform.position - offset * Time.deltaTime;
            
        }        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))

        if (positive)
        {
            positive = false;
        }
        else
        {
            positive = true;
        }
    }











}
