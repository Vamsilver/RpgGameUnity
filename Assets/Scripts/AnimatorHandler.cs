using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    public Animator AnimatorObject;
    public bool CanRotateObject;
    
    private readonly int _vertical = Animator.StringToHash("Vertical");
    private readonly int _horizontal = Animator.StringToHash("Horizontal");

    public void Initialize()
    {
        AnimatorObject = GetComponent<Animator>();
        
    }

    public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement) 
    {
        #region Vertical

        float vert = 0;
        
        if (verticalMovement is > 0 and < 0.55f)
        {
            vert = 0.5f;
        }
        else if (verticalMovement > 0.55f)
        {
            vert = 1;
        }
        else if (verticalMovement is < 0 and > -0.55f)
        {
            vert = -0.5f;
        }
        else if (verticalMovement < -0.55f)
        {
            vert = -1f;
        }
        else
        {
            vert = 0;
        }

        #endregion

        #region Horizontal

        float horiz = 0;

        if (horizontalMovement is > 0 and < 0.55f)
        {
            horiz = 0.5f;
        }
        else if (horizontalMovement > 0.55f)
        {
            horiz = 1;
        }
        else if (horizontalMovement is < 0 and > -0.55f)
        {
            horiz = -0.5f;
        }
        else if (horizontalMovement < -0.55f)
        {
            horiz = -1f;
        }
        else
        {
            horiz = 0;
        }

        #endregion
        
        AnimatorObject.SetFloat(_vertical, vert, 0.1f, Time.deltaTime);
        AnimatorObject.SetFloat(_horizontal, horiz, 0.1f, Time.deltaTime);
    }

    public void CanRotate()
    {
        CanRotateObject = true;
    }

    public void StopRotation()
    {
        CanRotateObject = false;
    }
}
