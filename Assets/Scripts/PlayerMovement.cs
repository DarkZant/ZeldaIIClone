using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public PlayerCombat combat;
    public Animator animator;
    float horizontalMove = 0f;
    bool jump = false;
    public bool crouch = false;
    public float runSpeed = 40f;
    void Update()
    {
        if (transform.position.y < -50)
        {
            transform.position = new Vector3(-4, 2, 0);
        }
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump") && controller.m_Grounded)
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;            
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            animator.enabled = false;
            animator.enabled = true;
        }       
    }
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }
    public void CheckForNextInput()
    {
        if (Input.GetButton("Jump") && controller.m_Grounded || Input.GetButton("Crouch") || Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 || Input.GetButton("Attack"))
        {
            animator.Rebind();
            if (Input.GetButton("Jump") && controller.m_Grounded)
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }
            if (Input.GetButton("Crouch"))
            {
                crouch = true;
            }
            if (Input.GetButtonDown("Attack"))
            {
                animator.SetTrigger("Attack");
            }
        }
        else       
            return;
    }    
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
