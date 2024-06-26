using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private Vector2 playerDestination;
    private Animator playerAnimator;
    public float moveSpeed;
    private int isMoveHash;
    private int vectorXHash;
    private int vectorYHash;
    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponentInChildren<Animator>();
        isMoveHash = Animator.StringToHash("isMove");
        vectorXHash = Animator.StringToHash("vectorX");
        vectorYHash = Animator.StringToHash("vectorY");

    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        playerRigidBody.velocity = playerDestination * moveSpeed;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
       if(context.phase == InputActionPhase.Performed)
        {
            playerDestination = context.ReadValue<Vector2>();
            SetAnim();
        }
       else if(context.phase == InputActionPhase.Canceled)
        {
            playerDestination = Vector2.zero;
            SetAnim();
        }
    }
    private void SetAnim()
    {
        if(playerDestination == Vector2.zero)
        {
            playerAnimator.SetBool(isMoveHash, false);
            playerAnimator.SetInteger(vectorXHash, 0);
            playerAnimator.SetInteger(vectorYHash, 0);
            return;
        }
        playerAnimator.SetBool(isMoveHash, true);
        playerAnimator.SetInteger(vectorXHash, (int)(playerDestination.x * 10));
        playerAnimator.SetInteger(vectorYHash, (int)(playerDestination.y * 10));
    }
}
