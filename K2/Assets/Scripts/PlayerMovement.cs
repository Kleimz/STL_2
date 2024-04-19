using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{   
    // Start is called before the first frame update
    [SerializeField]
    private float playerSpeed = 2.0f;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    public bool jumped = false;
    public bool groundedPlayer;

    Animator animator;

    Vector2 moveVector = new Vector2(0, 0);

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
        
        if (moveVector == Vector2.zero)
        {
            animator.SetFloat("Movement", -1);
        }
        else
        {
            animator.SetFloat("Movement", 1);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger("Jump");
            jumped = !jumped;
        }
        
    }

    void FixedUpdate()
    {
        if (jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        jumped = false;
        if(!groundedPlayer && controller.isGrounded)
        {
            animator.SetTrigger("Landing");
        }
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(moveVector.x, 0, moveVector.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        
    }
}
