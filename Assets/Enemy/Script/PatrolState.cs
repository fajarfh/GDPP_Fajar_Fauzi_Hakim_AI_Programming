using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{

    private bool _isMoving;
    private Vector3 _destination;

    public void EnterState(Enemy enemy)
    {
        Debug.Log("Start Patrol");
        _isMoving = false;
        
        if (enemy == null) return;
        enemy?.animatorBall.SetTrigger("PatrolState");
        //enemy.animatorModel.SetTrigger("PatrolState");
    }

    public void UpdateState(Enemy enemy)
    {
        //Debug.Log("Patrolling");

        if (Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) < enemy.ChaseDistance)
        {
            enemy.SwitchState(enemy.chaseState);
        }


        if (!_isMoving)
        {
            Debug.Log("New Destination");
            _isMoving = true;
            int index = Random.Range(0, enemy.waypoints.Count);
            _destination = enemy.waypoints[index].position;
            enemy.navMeshAgent.destination = _destination;
        }
        else
        {
            if (Vector3.Distance(_destination, enemy.transform.position) <= 0.1)
            {
                Debug.Log("Arrive at Destination");
                _isMoving = false;
            }
        }
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Stop Patrol");
    }
}
