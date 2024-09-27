using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerLocomotion : MonoBehaviour
{
    private Transform _cameraObject;
    private InputHandler _inputHandler;
    private Vector3 _moveDirection;

    [HideInInspector] public Transform MyTransform;
    [HideInInspector] public AnimatorHandler animatorHandlerObject;
    
    public new Rigidbody Rigidbody;
    public GameObject NormalCamera;

    [Header("Stats")]
    [SerializeField] private float _movementSpeed = 5f;

    [SerializeField] private float _rotationSpeed = 10f;
    
    
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        _inputHandler = GetComponent<InputHandler>();
        animatorHandlerObject = GetComponentInChildren<AnimatorHandler>();
        _cameraObject = Camera.main!.transform;
        MyTransform = transform;
        
        animatorHandlerObject.Initialize();
    }

    public void Update()
    {
        float delta = Time.deltaTime;
        
        _inputHandler.TickInput(delta);

        _moveDirection = _cameraObject.forward * _inputHandler.Vertical;
        _moveDirection += _cameraObject.right * _inputHandler.Horizontal;
        _moveDirection.Normalize();

        float speed = _movementSpeed;
        _moveDirection *= speed;

        Vector3 projectedVelocity = Vector3.ProjectOnPlane(_moveDirection, _normalVector);
        Rigidbody.velocity = projectedVelocity;
        
        animatorHandlerObject.UpdateAnimatorValues(_inputHandler.MoveAmount, 0);

        if (animatorHandlerObject.CanRotateObject)
        {
            HandleRotation(delta);
        }
    }

    #region Movement

    private Vector3 _normalVector;
    private Vector3 _targetPosition;

    private void HandleRotation(float delta)
    {
        float moveOverride = _inputHandler.MoveAmount;

        Vector3 targetDirection = _cameraObject.forward * _inputHandler.Vertical;
        targetDirection += _cameraObject.right * _inputHandler.Horizontal;
        
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = MyTransform.forward;

        float rs = _rotationSpeed;

        Quaternion tr = Quaternion.LookRotation(targetDirection);
        Quaternion targetRotation = Quaternion.Slerp(MyTransform.rotation, tr, rs * delta);

        MyTransform.rotation = targetRotation;
    }



    #endregion

}
