using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
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

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        //rb = GetComponent<Rigidbody>();
        navPath = new NavMeshPath();
        remainingPoints = new Queue<Vector3>();

        if (agent.CalculatePath(target.position, navPath))
        {
            Debug.Log("found path to target");
            foreach(Vector3 p in navPath.corners)
            {
                remainingPoints.Enqueue(p);
            }

            currentTargetPoint = remainingPoints.Dequeue();
        }
            
    }

    void Update()
    {
        new_forward = (currentTargetPoint - transform.position).normalized;
        new_forward.y = 0;
        guard.position = new_forward;

        //distToPointX = Mathf.Lerp(guard.position.x, currentTargetPoint.x, speed);
        //distToPointZ = Mathf.Lerp(guard.position.z, currentTargetPoint.z, speed);
        //myPoint = new Vector3(distToPointX, 0.05196404f, distToPointZ);
        //Debug.Log("guard position = " + guard.position.x + " and " + guard.position.z);
        //Debug.Log(currentTargetPoint.x + " and " + currentTargetPoint.z);

        if (new_forward.x < 0.4f && new_forward.z < 0.4f)
        {
            if(remainingPoints.Count > 0)
            {
                currentTargetPoint = remainingPoints.Dequeue();
            }
        }

        characterController.Move(new_forward * speed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        //rb.velocity = transform.forward * speed;
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

}
