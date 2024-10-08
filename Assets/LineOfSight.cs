using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LineOfSight : MonoBehaviour
{
    
    public bool canBeOnSight = false;
    [SerializeField] Guard guard;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            canBeOnSight = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            canBeOnSight = false;
            guard.investigating = false;
        }
    }
}
