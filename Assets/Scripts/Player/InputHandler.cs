using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    //The input mappings of the input system
    private PlayerInput _playerInput;
    //The inputs while controlling the player
    private PlayerInput.PlayerControlActions _playerControl;

    //The player camera script component
    private PlayerCamera _playerCamera;
    //The player movement script component
    private PlayerMovement _playerMovement;

    // Start is called before the first frame update
    void Awake()
    {
        //Player input from mapping
        _playerInput = new PlayerInput();
        //The player on foot action controls
        _playerControl = _playerInput.PlayerControl;

        _playerCamera = GetComponent<PlayerCamera>();
        _playerMovement = GetComponent<PlayerMovement>();

        //Connects jump performed action to onJump method
        _playerControl.Jump.performed += OnJump;
    }

    void FixedUpdate(){
        //Handle the player input and movement
        _playerMovement.HandleMovement(_playerControl.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate(){
        //Update the player's camera after the player movement
        _playerCamera.HandleCamera(_playerControl.Look.ReadValue<Vector2>());
    }

    //Enable the player control
    private void OnEnable(){
        _playerControl.Enable();
    }

    //Disable the player control
    private void OnDisable(){
        _playerControl.Disable();
    }

    //Handle jump on jump input pressed
    private void OnJump(InputAction.CallbackContext context){
        _playerMovement.HandleJump();
    }
}
