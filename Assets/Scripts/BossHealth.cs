using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour
{
    [Header("References")]
    public GameManager gameManager;

    [Header("Health Settings")]
    private int maxHealth = 200;
    private int damagePerHit = 1;

    private int currentHealth;
    private bool isAlive = true;
    private SpriteRenderer sprite_;

    private float redGlowTimer = 0f;
    private Color originalColor;
    private Animator an;

    void Start()
    {
        currentHealth = maxHealth;
        sprite_ = GetComponent<SpriteRenderer>();
        originalColor = sprite_.color;
        an = GetComponent<Animator>();
    }

    public void TakeDamage(int damageAmount)
    {
        if (!isAlive) return;

        // Start or extend red glow
        redGlowTimer = 0.25f;

        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"👹 Boss took {damageAmount} damage. HP: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
            Die();
    }

    void Update()
    {
        // Handle red glow over time
        if (redGlowTimer > 0f)
        {
            redGlowTimer -= Time.deltaTime;
            sprite_.color = Color.red;
        }
        else
        {
            sprite_.color = originalColor;
        }
    }

    private void Die()
    {
        if (!isAlive) return;
        isAlive = false;
        Debug.Log("💥 Boss died!");
        gameManager.bossDead = true;
        gameManager.boss_script.enabled = false;
        an.SetTrigger("Death");
        
    }

    public float GetHealthPercent()
    {
        return (float)currentHealth / maxHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isAlive) return;

        if (other.CompareTag("bullet"))
        {
            TakeDamage(damagePerHit);
            Debug.Log("hit by bullet");
        }
    }
}
