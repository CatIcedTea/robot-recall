using System.Collections;
using System.Runtime.InteropServices;
using DG.Tweening;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Transform _cameraHolder;


    [Header("Camera Position and Basis")]
    [Tooltip("This is the basis/orientation and position of the player camera")]
    [SerializeField] private Transform _playerBasis;

    [Tooltip("Mouse Sensitivity")]
    [Header("Sensitivity")]
    [SerializeField] private float _sensitivityX;
    [SerializeField] private float _sensitivityY;

    private PlayerMovement playerMovement;
    
    //The X rotation input
    private float _xRotation;
    //The Y rotation input
    private float _yRotation;

    // Start is called before the first frame update
    void Start()
    {
        //Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //This wil update the camera rotation in the InputHandler
    public void HandleCamera(Vector2 cameraInput){
        //Rotation of the input
        _yRotation += cameraInput.x * Time.deltaTime * _sensitivityX;
        _xRotation -= cameraInput.y * Time.deltaTime * _sensitivityY;

        //Restrict the up and down rotation to 90 degrees up and down
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        //Apply the rotation to the camera
        _cameraHolder.transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0);

        //Apply the rotation to the player
        _playerBasis.parent.transform.localRotation = Quaternion.Euler(0, _yRotation, 0);

        //Moves the camera to the player camera position
        _cameraHolder.transform.position = _playerBasis.transform.position;
    }

    //Handle the Z rotation with the given argument
    public void HandleZTilt(float rotation){
        _cameraHolder.GetChild(0).transform.DOLocalRotate(new Vector3(0, 0, -rotation * 2f), 0.5f);
    }

    //Handle the camera FOV with the given argument
    public void HandleFOV(float fov){
        StartCoroutine(HandleFOVCoroutine(fov));
    }

    //Coroutine to handle FOV
    private IEnumerator HandleFOVCoroutine(float fov){
        _cameraHolder.GetChild(0).GetComponent<Camera>().DOFieldOfView(fov, 0.25f);
        yield return new WaitForSeconds(0.2f);
        _cameraHolder.GetChild(0).GetComponent<Camera>().DOFieldOfView(90f, 1f);
    }
}
