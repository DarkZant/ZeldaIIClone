using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public Transform player;
    public Transform enemy;
    public Collider2D playerCollider;
    public Collider2D enemyCollider;
    public LayerMask playerL;
    public Transform hitbox;
    public int maxHealth = 100;
    public float moveSpeed = 0.01f;   
    public int damageDealt;
    public float hitboxRange;
    bool m_FacingRight = false;
    int currentHealth;
    public float dist;

    void Start()
    {      
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage, float knockback)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if (player.position.x <= enemy.position.x)
        {
            rb.AddForce(new Vector2(knockback, 0));
        }
        else
        {
            rb.AddForce(new Vector2(-knockback, 0));
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void DealDamage()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCapsuleAll(hitbox.position, new Vector2(0.75f, 1.25f), new CapsuleDirection2D(), 0f);           
        foreach (Collider2D Player in hitPlayer)
        {
            Debug.Log("We got hit");
            //player.GetComponent<PlayerCombat>().Hurt(damageDealt);
        }
    }
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        dist = Mathf.Abs(player.position.x - enemy.position.x);
        if (dist < 5)
        {
            float speed = m_FacingRight ? moveSpeed : -moveSpeed;
            transform.position += new Vector3(speed, 0, 0);
        }   
        if (player.position.x > enemy.position.x && !m_FacingRight)
        {
            Flip();        
        }
        else if (player.position.x <= enemy.position.x && m_FacingRight)
        {
            Flip();
        }
    }
    void Die()
    {
        animator.SetBool("IsDead", true);
        Destroy(GetComponent<Rigidbody2D>());
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        enabled = false;
    }                 
    private void Flip()
    {        
        m_FacingRight = !m_FacingRight;       
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void OnDrawGizmosSelected()
    {
        if (hitbox == null)
            return;
        Gizmos.DrawWireSphere(hitbox.position, 1f);
    }
}
