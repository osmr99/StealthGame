using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TurretStates
{
    IDLE,
    SHOOT
}

public class LineOfSight : MonoBehaviour
{

    [SerializeField] Transform target;
    TurretStates state = TurretStates.IDLE;
    public bool canBeOnSight = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canBeOnSight)
        {
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            Vector3 forwardDirection = transform.forward;

            float dot = Vector3.Dot(forwardDirection, directionToTarget);

            if (dot > 0.5f)
            {
                state = TurretStates.SHOOT;
            }
            else
            {
                state = TurretStates.IDLE;
            }

            switch (state)
            {
                case TurretStates.IDLE:
                    UpdateIdle();
                    break;
                case TurretStates.SHOOT:
                    UpdateShoot();
                    break;
            }
        }
    }



    //private void OnCollisionEnter(Collision collision)
    //{
        //Debug.Log("something entered");
        //if (collision.gameObject.name == "Player")
        //{
            //Debug.Log("enter");
            //canBeOnSight = true;
        //}
    //}

    //private void OnCollisionExit(Collision collision)
    //{
        //Debug.Log("something exited");
        //if (collision.gameObject.name == "Player")
        //{
            //Debug.Log("exit");
            //canBeOnSight = false;
        //}
    //}

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
        }
    }

    void UpdateIdle()
    {

    }

    void UpdateShoot()
    {

    }
}
