using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _acceleration;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _maxAirSpeed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private Transform _playerBasis;
    [SerializeField] private float _friction;

    [Header("Floor Check")]
    [Tooltip("Used for raycast checking floor collision")]
    [SerializeField] private float _playerHeight;

    //Used to check if player is touching the floor
    private bool _isOnFloor;
    //Vector of the player's input
    private Vector3 _inputDir;
    //Rigid body component of the player
    private Rigidbody _rigidBody;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        //Freeze the rotation of the player
        _rigidBody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if it is on floor
        _isOnFloor = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.02f);

        //Apply friction if it is on floor
        if(_isOnFloor){
            _rigidBody.drag = _friction;
        }
        else{
            _rigidBody.drag = 0;
        }
    }

    //Handle movement input
    public void HandleMovement(Vector2 input){
        //Get the movement input
        _inputDir = _playerBasis.forward * input.normalized.y + _playerBasis.right * input.normalized.x;

        //Apply the velocity to the player body
        _rigidBody.velocity = Vector3.Lerp(_rigidBody.velocity, HandleVelocity(), _acceleration * Time.deltaTime);
    }

    //Handle jumping
    public void HandleJump(){
        //Jump only when it the player is on a floor
        if(_isOnFloor){
            _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, 0f, _rigidBody.velocity.z);

            _rigidBody.velocity += Vector3.up * _jumpHeight;
        }
    }

    //Handle the velocity of the player and able to bunnyhop
    private Vector3 HandleVelocity(){
        //Scalar projection or Dot product of the current velocity to the current input direction
        float projectedVelocity = Vector3.Dot(_rigidBody.velocity, _inputDir);

        //The new velocity being added to the current velocity
        float addedVelocity;

        //Limit the added speed
        if(_isOnFloor)
            addedVelocity = Mathf.Clamp(_maxSpeed - projectedVelocity, 0, _maxSpeed);
        else
            addedVelocity = Mathf.Clamp( _maxAirSpeed - projectedVelocity, 0, _maxAirSpeed);

        //Return the new velocity
        return _rigidBody.velocity + (addedVelocity * _inputDir);
    }
}
