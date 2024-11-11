using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GunMovement : MonoBehaviour
{public float angleMultiplier = 1.5f; // Controls how much the gun rotates
    public float smoothing = 5f; // Smoothing factor for rotation

    private Transform cameraTransform;
    private Quaternion initialRotation;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        // Get the camera's vertical look angle (pitch)
        float pitch = cameraTransform.eulerAngles.x;

        // Adjust pitch for angles above 180 degrees
        if (pitch > 180f) pitch -= 360f;

        // Calculate the target rotation based on the pitch
        float gunAngle = pitch * angleMultiplier;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(-gunAngle, 0, 0);

        // Smoothly interpolate the gun's rotation
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * smoothing);
    }
}