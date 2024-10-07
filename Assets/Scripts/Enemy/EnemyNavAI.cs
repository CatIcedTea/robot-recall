using UnityEngine;
using UnityEngine.AI;

public class EnemyNavAI : MonoBehaviour
{
    private Transform _player;
    private NavMeshAgent _navAgent;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _navAgent = GetComponent<NavMeshAgent>();

        _navAgent.updatePosition = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Set the destination
        _navAgent.destination = _player.position;

        //Makes the enemy rotate to the player when it is within the stopping distance
        if( _navAgent.remainingDistance <= _navAgent.stoppingDistance){
            transform.LookAt(_player.position);
        }
    }
}
