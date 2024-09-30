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

        //Connects actions to its functions
        _playerControl.Jump.performed += OnJump;
        _playerControl.Crouch.performed += OnCrouch;
        _playerControl.Crouch.canceled += OnCrouchCanceled;
        _playerControl.Dash.performed += OnDash;

        _playerControl.Escape.performed += OnEscape;
    }

    void FixedUpdate(){
        //Handle the player input and movement
        _playerMovement.HandleMovement(_playerControl.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate(){
        //Update the player's camera after the player movement
        _playerCamera.HandleCamera(_playerControl.Look.ReadValue<Vector2>());
        _playerCamera.HandleZTilt(_playerControl.Movement.ReadValue<Vector2>().x);
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

    //Handle crouch on crouch input pressed
    private void OnCrouch(InputAction.CallbackContext context){
        _playerMovement.HandleCrouch(true);
    }

    //Handle crouch on crouch input canceled
    private void OnCrouchCanceled(InputAction.CallbackContext context){
        _playerMovement.HandleCrouch(false);
    }

    //Handle dash on dash input pressed
    private void OnDash(InputAction.CallbackContext context){
        _playerMovement.HandleDash();
        _playerCamera.HandleFOV(100f);
    }

    private void OnEscape(InputAction.CallbackContext context){
        Application.Quit();
    }
}
