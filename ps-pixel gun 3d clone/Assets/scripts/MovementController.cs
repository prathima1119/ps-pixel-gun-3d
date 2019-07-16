﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    [SerializeField]
    float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;
    [SerializeField]
    GameObject fpsCamera;
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float CameraUpAndDownRotation = 0f;
    private float CurrentCameraUpAndDownRotation = 0f;
    private Rigidbody rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        //caluclate movement velocity,ty as a 3d vector
        float _xMovement = Input.GetAxis("Horizontal");
        float _zMovement = Input.GetAxis("Vertical");
        Vector3 _movementHorizontal = transform.right * _xMovement;
        Vector3 _movementVertical = transform.forward * _zMovement;
        //final movement velocity
        Vector3 _movementVelocity = (_movementHorizontal + _movementVertical).normalized * speed;
        //final movement
        Move(_movementVelocity);
        //caluclate rotation as a 3d vector for turning around
        float _vRotation = Input.GetAxis("Mouse X");
        Vector3 _rotationVector = new Vector3(0, _vRotation, 0)*lookSensitivity;
        //apply rotation
        Rotate(_rotationVector);
        //caluclate look up and down camera rotation
        float _cameraUpDownRotation = Input.GetAxis("Mouse Y")*lookSensitivity;
        //apply rotation
        RotateCamera(_cameraUpDownRotation);

    }
    //runs per physics iteration
    private void FixedUpdate()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        }
        rb.MoveRotation(rb.rotation*Quaternion.Euler(rotation));
        if(fpsCamera!=null)
        {
            CurrentCameraUpAndDownRotation -= CameraUpAndDownRotation;
            CurrentCameraUpAndDownRotation = Mathf.Clamp(CurrentCameraUpAndDownRotation, -85, 85);
            fpsCamera.transform.localEulerAngles = new Vector3(CurrentCameraUpAndDownRotation,0,0);
        }
    }
    void Move(Vector3 movementVelocity)
    {
        velocity = movementVelocity;
    }
    void Rotate(Vector3 rotationVector)
    {
        rotation = rotationVector;
    }
    void RotateCamera(float cameraUpAndDownRotation)
    {
        cameraUpAndDownRotation = cameraUpAndDownRotation;
    }
}
