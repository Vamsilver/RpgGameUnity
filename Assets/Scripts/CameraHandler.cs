using System;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform TargetTransform;
    public Transform CameraTransform;
    public Transform CameraPivotTransform;
    public Transform MyTransform;

    public float LookSpeed = 0.1f;
    public float FollowSpeed = 0.1f;
    public float PivotSpeed = 0.1f;
    public float MinimumPivot = -35f;
    public float MaximumPivot = 35f;

    public static CameraHandler Singleton;
    
    
    private Vector3 _cameraTransformPositions;
    private LayerMask _ignoreLayers;

    private float _defaultPosition;
    private float _lookAngle;
    private float _pivotAngle;

    private void Awake()
    {
        Singleton = this;
        MyTransform = transform;
        _defaultPosition = CameraTransform.localPosition.z;
        _ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
    }

    public void FollowTarget(float delta)
    {
        Vector3 targetPosition = Vector3.Lerp(MyTransform.position, TargetTransform.position, delta / FollowSpeed);
        MyTransform.position = targetPosition;
    }

    public void HandleCameraRotation(float delta, float mouseInputX, float mouseInputY)
    {
        _lookAngle += (mouseInputX * LookSpeed) / delta;
        _pivotAngle -= (mouseInputY * PivotSpeed) / delta;
        _pivotAngle = Mathf.Clamp(_pivotAngle, MinimumPivot, MaximumPivot);

        Vector3 rotation = Vector3.zero;
        rotation.y = _lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        MyTransform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = _pivotAngle;
        
        targetRotation = Quaternion.Euler(rotation);
        CameraPivotTransform.localRotation = targetRotation;

    }
}
