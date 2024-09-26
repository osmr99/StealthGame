using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] Transform guard;
    //Rigidbody rb;
    CharacterController characterController;
    [SerializeField] float speed;
    NavMeshPath navPath;
    Queue<Vector3> remainingPoints;
    public Vector3 currentTargetPoint;
    //public float distToPointX;
    //public float distToPointZ;
    //public Vector3 myPoint;
    public Vector3 new_forward;
    public bool foundYou = false;
    public bool investigating = false;
    public bool spot = false;
    public int timer = 200; // 50 = 1 second
    Vector3 heardSomethingHere;
    Vector3[] patrolLocationArrays = new Vector3[5];

    // Start is called before the first frame update
    void Start()
    {
        patrolLocationArrays[0] = new Vector3(10, 0, 11);
        patrolLocationArrays[1] = new Vector3(9, 0, -7);
        patrolLocationArrays[2] = new Vector3(-9, 0, -10);
        patrolLocationArrays[3] = new Vector3(-9, 0, 10);
        patrolLocationArrays[4] = new Vector3(0, 0, 0);
        characterController = GetComponent<CharacterController>();
        //rb = GetComponent<Rigidbody>();
        navPath = new NavMeshPath();

        int randonNumber = UnityEngine.Random.Range(0, 3);
        switch(randonNumber)
        {
            case 0:
                calculatePath(patrolLocationArrays[0]);
                break;
            case 1:
                calculatePath(patrolLocationArrays[1]);
                break;
            case 2:
                calculatePath(patrolLocationArrays[3]);
                break;
        }
        
            
    }

    void Update()
    {
        if(!foundYou && !investigating)
        {
            speed = 2;
            new_forward = (currentTargetPoint - transform.position).normalized;
            new_forward.y = 0;
            guard.position = new_forward;

            if ((new_forward.x < 0.1f && new_forward.z < 0.1f))
            {
                if (remainingPoints.Count > 0)
                {
                    currentTargetPoint = remainingPoints.Dequeue();
                }
                else
                {
                    int randonNumber = UnityEngine.Random.Range(0, 5);
                    switch (randonNumber)
                    {
                        case 0:
                            calculatePath(patrolLocationArrays[0]);
                            break;
                        case 1:
                            calculatePath(patrolLocationArrays[1]);
                            break;
                        case 2:
                            calculatePath(patrolLocationArrays[2]);
                            break;
                        case 3:
                            calculatePath(patrolLocationArrays[3]);
                            break;
                        case 4:
                            calculatePath(patrolLocationArrays[4]);
                            break;
                    }
                }
            }

            characterController.Move(new_forward * speed * Time.deltaTime);
        }
        else if (foundYou)
        {
            speed = 0;
            agent.SetDestination(target.position);
            timer = 300;
        }

        if (timer == 0)
        {
            investigating = false;
            timer = 300;
        }
            

    }
    private void FixedUpdate() // Updates 50 times a frame
    {
        //rb.velocity = transform.forward * speed;
        if (investigating && !foundYou && timer == 300)
        {
            speed = 0;
            spot = true;
            InvestigateHere();
            timer--;
        }
        else if (timer != 300 && timer != 0)
        {
            agent.SetDestination(heardSomethingHere);
            timer--;
        }
            
    }

    private void OnDrawGizmos()
    {
        if (navPath == null)
            return;

        Gizmos.color = Color.red;
        foreach(Vector3 node in navPath.corners)
        {
            Gizmos.DrawWireSphere(node, 0.5f);
        }
    }

    void InvestigateHere()
    {
        if (spot)
        {
            spot = false;
            heardSomethingHere = target.position;
        } 
    }

    void calculatePath(Vector3 here)
    {
        remainingPoints = new Queue<Vector3>();

        if (agent.CalculatePath(here, navPath))
        {
            foreach (Vector3 p in navPath.corners)
            {
                remainingPoints.Enqueue(p);
            }

            currentTargetPoint = remainingPoints.Dequeue();
        }
    }
}
