using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _randomCooldownRange;

    private EnemyGun _gun;
    private Transform _player;
    private EnemyNavAI _navAI;
    private Rigidbody _rigidBody;
    private float _currentAttackCooldown;
    private bool _canFireGun = false;
    private bool _playerInView = false;
    private Ray _rayInView;
    private RaycastHit _hitRayCast;
    private bool _isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        _gun = GetComponentInChildren<EnemyGun>();
        _rigidBody = GetComponent<Rigidbody>();
        _navAI = GetComponent<EnemyNavAI>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        //Sets the current attack cooldown plus a random amount of time within the range for variability
        _currentAttackCooldown = _attackCooldown + Random.Range(-_randomCooldownRange, _randomCooldownRange);
    }

    // Update is called once per frame
    void Update()
    {
        if(_isAlive){
            HandleAttack();
        }
    }

    //Handle enemy attacking behavior
    private void HandleAttack(){
        //Count down the attack cooldown
        _currentAttackCooldown -= Time.deltaTime;

        //Ray to check if the projectile has a path to the player
        _rayInView = new Ray(_gun.GetSpawnPoint().position, (_player.position - _gun.GetSpawnPoint().position).normalized);

        //Cast the ray and check if the hit object is the player
        Physics.Raycast(_rayInView, out _hitRayCast);
        _playerInView = _hitRayCast.transform.tag == "Player";

        //The boolean to check if the enemy is ready to fire the gun
        _canFireGun = _currentAttackCooldown <= 0 && _playerInView;

        //Fire the gun
        if(_canFireGun){
            _gun.FireGun();
            //Set the cooldown back plus the randomized range
            _currentAttackCooldown = _attackCooldown + Random.Range(-_randomCooldownRange, _randomCooldownRange);
        }
    }

    //Handles death
    public void HandleDeath(){
        _rigidBody.drag = 0.5f;
        _isAlive = false;
        _navAI.enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        _rigidBody.mass = 1;
        _rigidBody.AddForce(-transform.forward * 5f + transform.up * 1f + transform.right * Random.Range(-2, 2), ForceMode.Impulse);
        
    }
}
