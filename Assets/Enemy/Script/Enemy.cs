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

    [SerializeField]
    public float ChaseDistance;

    [SerializeField]
    public Player Player;

    //[HideInInspector]
    public Animator animatorBall;

    private AudioSource _audioSource;

    //public Animator animatorModel;




    private void Awake()
    {
        _currentState = patrolState;
        _currentState.EnterState(this);

        _audioSource = GetComponent<AudioSource>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        animatorBall = GetComponent<Animator>();
        
    }
    private void Start()
    {
        if (Player != null)
        {
            Player.OnPowerUpStart += StartRetreating;
            Player.OnPowerUpStop += StopRetreating;
        }
    }

    private void Update()
    {
        _currentState?.UpdateState(this);
    }

    public void SwitchState(BaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }

    private void StartRetreating()
    {
        SwitchState(retreatState);
    }

    private void StopRetreating()
    {
        SwitchState(patrolState);
    }

    public void Dead()
    {
        _audioSource?.Play();
        //Destroy(gameObject);
        int index = Random.Range(0, waypoints.Count);
        Vector3 _destination = waypoints[index].position;
        transform.position = _destination;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_currentState != retreatState)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Player>().Dead();
            }
        }
    }


}
