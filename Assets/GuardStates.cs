using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public enum TurretStates
{
    PATROL,
    INVESTIGATE,
    PURSUE
}

public class GuardStates : MonoBehaviour
{

    public TurretStates state = TurretStates.PATROL;
    [SerializeField] Transform target;
    [SerializeField] LineOfSight lineOfSight;
    [SerializeField] SoundRadius soundRadius;
    [SerializeField] Player player;
    [SerializeField] Guard guard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lineOfSight.canBeOnSight)
        {
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            Vector3 forwardDirection = transform.forward;

            float dot = Vector3.Dot(forwardDirection, directionToTarget);

            if (dot > 0.5f)
            {
                state = TurretStates.PURSUE;
                guard.investigating = false;
            }
            else
            {
                state = TurretStates.PATROL;
            }
        }
        else if(!lineOfSight.canBeOnSight && !guard.investigating)
        {
            guard.foundYou = false;
            state = TurretStates.PATROL;
        }

        if(soundRadius.canBeHeard && !player.isSneaking && (player.movement.x != 0 || player.movement.y != 0))
        {
            state = TurretStates.INVESTIGATE;
        }
        switch (state)
        {
            case TurretStates.PATROL:
                UpdatePatrol();
                break;
            case TurretStates.PURSUE:
                UpdatePursue();
                break;
            case TurretStates.INVESTIGATE:
                UpdateInvestigate();
                break;
        }
    }

    void UpdatePatrol()
    {
        guard.agent.speed = 0;
        guard.foundYou = false;
    }

    void UpdatePursue()
    {
        guard.agent.speed = 2.5f;
        guard.foundYou = true;
        guard.investigating = false;
    }

    void UpdateInvestigate()
    {
        guard.agent.speed = 2.5f;
        guard.investigating = true;
    }
}
