using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private BaseState _currentState;
    public PatrolState patrolState = new PatrolState();
    public ChaseState chaseState = new ChaseState();
    public RetreatState retreatState = new RetreatState();

    [SerializeField]
    public List<Transform> waypoints = new List<Transform>();

    [HideInInspector]
    public NavMeshAgent navMeshAgent;


    private void Awake()
    {
        _currentState = patrolState;
        _currentState.EnterState(this);

        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _currentState?.UpdateState(this);
    }

}
