using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _dashCooldown;
    [SerializeField] private float _maxStamina;
    [SerializeField] private float _staminaRegenSpeed;
    [SerializeField] private float _staminaRegenTimer;
    [SerializeField] private float _dashStaminaCost;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _maxAirSpeed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private Transform _playerBasis;
    [SerializeField] private float _friction;

    [Header("Crouching")]
    [SerializeField] private float _crouchSpeed;
    [SerializeField] private float _crouchInitialYScaling;
    [SerializeField] private float _crouchFriction;
    [SerializeField] private float _crouchYScaling;

    [Header("Floor Check")]
    [Tooltip("Used for raycast checking floor collision")]
    [SerializeField] private float _playerHeight;

    //Used to check if player is touching the floor
    private bool _isOnFloor;
    //Used to check if player is crouching
    private bool _isCrouching;
    //Used to check if player is dashing
    private bool _isDashing;
    //Current dash cooldown timer
    private float _currentDashCooldown;
    //Current stamina stamina regen timer
    private float _currentStaminaRegenTimer;
    //Current stamina
    private float _stamina;
    //Used to check if the player can regen stamina
    private bool _canRegenStamina = true;
    //Vector of the player's input
    private Vector3 _inputDir;
    //Rigid body component of the player
    private Rigidbody _rigidBody;
    //Reference to the player camera script
    private PlayerCamera _playerCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _playerCamera = GetComponent<PlayerCamera>();

        //Freeze the rotation of the player
        _rigidBody.freezeRotation = true;
        
        _currentDashCooldown = 0;
        _stamina = _maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if it is on floor
        _isOnFloor = Physics.SphereCast(transform.position, 0.25f, Vector3.down, out RaycastHit hit, _playerHeight * transform.localScale.y * 0.5f - 0.25f + 0.02f);

        if(_currentDashCooldown > 0)
            _currentDashCooldown -= Time.deltaTime;

        if(_currentStaminaRegenTimer > 0)
            _currentStaminaRegenTimer -= Time.deltaTime;

        if(_stamina < _maxStamina && _currentStaminaRegenTimer <= 0){
            _stamina += _staminaRegenSpeed * Time.deltaTime;
            if(_stamina > _maxStamina)
                _stamina = _maxStamina;
        }
        
        if(_isOnFloor){
            if(_inputDir == Vector3.zero){
                if(_rigidBody.velocity.magnitude < 1){
                    _rigidBody.velocity = Vector3.zero;
                }
            }

            //Apply friction if it is on floor
            if(!_isDashing){
                if(!_isCrouching)
                    _rigidBody.drag = _friction;
                else
                    _rigidBody.drag = _crouchFriction;
            }
        }
        else{
            _rigidBody.drag = 0;
        }
    }

    //Handle movement input in InputHandler
    public void HandleMovement(Vector2 input){
        //Get the movement input
        _inputDir = _playerBasis.forward * input.normalized.y + _playerBasis.right * input.normalized.x;

        //Apply the velocity to the player body
        _rigidBody.velocity = Vector3.Lerp(_rigidBody.velocity, HandleVelocity(), _acceleration * Time.deltaTime);
    }

    //Handle jumping in InputHandler
    public void HandleJump(){
        //Jump only when it the player is on a floor
        if(_isOnFloor){
            _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, 0f, _rigidBody.velocity.z);
            
            _rigidBody.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);
        }
    }

    //Handle crouching in InputHandler
    public void HandleCrouch(bool isCrouching){
        _isCrouching = isCrouching;

        if(isCrouching){
            transform.localScale = new Vector3(transform.localScale.x, _crouchYScaling, transform.localScale.z);

            if(_isOnFloor){
                _rigidBody.AddForce(Vector3.down * 10f, ForceMode.Impulse);
            }
        }
        else{
            transform.localScale = new Vector3(transform.localScale.x, _crouchInitialYScaling, transform.localScale.z);
        }
    }

    //Handle dashing in Input Handler
    public void HandleDash(){
        StartCoroutine(DashCoroutine());
        _currentDashCooldown = _dashCooldown;
    }

    //Handle the velocity of the player and able to bunnyhop
    private Vector3 HandleVelocity(){
        //Do nothing with the velocity if dashing
        if(_isDashing)
            return _rigidBody.velocity;

        //Scalar projection or Dot product of the current velocity to the current input direction
        float projectedVelocity = Vector3.Dot(_rigidBody.velocity, _inputDir);

        //The new velocity being added to the current velocity
        float addedVelocity;

        //Calculate the added velocity and limit it
        if(_isOnFloor){
            if(!_isCrouching)
                addedVelocity = Mathf.Clamp(_maxSpeed - projectedVelocity, 0, _maxSpeed);
            else
                addedVelocity = Mathf.Clamp(_crouchSpeed - projectedVelocity, 0, _crouchSpeed);
        }
        else
            addedVelocity = Mathf.Clamp( _maxAirSpeed - projectedVelocity, 0, _maxAirSpeed);

        //Return the new velocity
        return _rigidBody.velocity + (addedVelocity * _inputDir);
    }

    //Coroutine for dashing
    private IEnumerator DashCoroutine(){
        _stamina -= 25;
        _isDashing = true;
        _currentStaminaRegenTimer = _staminaRegenTimer;
        _rigidBody.useGravity = false;
        _rigidBody.drag = 0;
        if(_inputDir != Vector3.zero)
            _rigidBody.AddForce(_inputDir * 15f, ForceMode.Impulse);
        else
            _rigidBody.AddForce(transform.forward * 15f, ForceMode.Impulse);

        yield return new WaitForSeconds(0.25f);
        _isDashing = false;
        _rigidBody.useGravity = true;
    }

    public float GetStamina(){
        return _stamina;
    }

    public float GetMaxStamina(){
        return _maxStamina;
    }

    public bool CanDash(){
        return _currentDashCooldown <= 0 && _stamina >= _dashStaminaCost;
    }
}
