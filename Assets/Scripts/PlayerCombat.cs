using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;
    public Transform attackPoint;
    public Transform crouchAttackPoint;
    public Rigidbody2D rb;
    public Collider2D hitbox1;
    public Collider2D hitbox2;
    public PlayerMovement move;
    public CharacterController2D controller;
    public LayerMask enemyLayers;
    public LayerMask walls;    
    public float knockbackTaken;   
    public int maxHealth;
    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {       
        if (Input.GetButtonDown("Attack"))
        {
            if (move.crouch)
            {
                animator.SetTrigger("CrouchAttack");
            }
            else
            {
                if (controller.m_Grounded)
                {
                    move.enabled = false;
                    Invoke("StopAttack", 0.3f);
                    rb.velocity = new Vector2(0, 0);
                }
                animator.SetTrigger("Attack");
            }
        }
    }
    public void Attack()
    {       
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, 0.4f, enemyLayers);
        Collider2D[] hitWall = Physics2D.OverlapCircleAll(attackPoint.position, 0.4f, walls);       
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(40, 12500);
        }
        foreach (Collider2D bruh in hitWall)
        {
            if (controller.m_FacingRight)
            {
                rb.AddForce(new Vector2(-50, 0));
            }
            else
            {
                rb.AddForce(new Vector2(50, 0));
            }
        }
    }
    public void CrouchAttack()
    {
        Collider2D[] hitEnemies2 = Physics2D.OverlapCircleAll(crouchAttackPoint.position, 0.3f, enemyLayers);
        Collider2D[] hitWall2 = Physics2D.OverlapCircleAll(crouchAttackPoint.position, 0.3f, walls);
        foreach (Collider2D enemy in hitEnemies2)
        {
            enemy.GetComponent<Enemy>().TakeDamage(20, 6250);
        }
        foreach (Collider2D greyBrick in hitWall2)
        {
            if (controller.m_FacingRight)
            {
                rb.AddForce(new Vector2(-50, 0));
            }
            else
            {
                rb.AddForce(new Vector2(50, 0));
            }
        }
    }
    public void StopAttack()
    {
        move.enabled = true;
        rb.velocity = new Vector2(0, 0);
    }
    //void OnDrawGizmosSelected()
    //{
    //    if (attackPoint == null)
    //        return;
    //    Gizmos.DrawWireSphere(crouchAttackPoint.position, 0.3f);
    //}
    public void Hurt(int damageTaken)
    {
        animator.SetTrigger("Hurt");
        currentHealth -= damageTaken;
        if (controller.m_FacingRight)
        {
            rb.AddForce(new Vector2(-knockbackTaken, 0));
        }
        else
        {
            rb.AddForce(new Vector2(knockbackTaken, 0));
        }
    }   
}
