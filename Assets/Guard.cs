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
    Vector3 currentTargetPoint;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
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
        var new_forward = (Vector3.zero - transform.position).normalized;
        new_forward.y = 0;
        transform.position = new_forward;

        float distToPoint = Vector3.Distance(transform.position, currentTargetPoint);
        Debug.Log("transform position = " + transform.position.x + " and " + transform.position.z);
        Debug.Log(distToPoint);
        Debug.Log(currentTargetPoint.x + " and " + currentTargetPoint.z);

        if (distToPoint < 1)
        {
            currentTargetPoint = remainingPoints.Dequeue();
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
