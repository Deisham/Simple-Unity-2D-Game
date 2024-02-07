using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float knockbackForce = 5f;
    
    public Image healthBar;
    public UIManager uiManager;
    public Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            TakeDamage(20);
        }

        if (IsDead())
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        healthBar.fillAmount = healthPercentage;
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            Debug.LogWarning("Damage value should be non-negative.");
            return;
        }

        currentHealth -= damage;
        UpdateHealthBar();
        animator.SetTrigger("takedmg");

        if (IsDead())
        {
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TrapSpikes"))
        {
            TakeDamage(10);
            rb.velocity = new Vector2(rb.velocity.x, knockbackForce);
        }
    }

    bool IsDead()
    {
        return currentHealth <= 0;
    }

    void Die()
    {
        Debug.Log("Player is dead!");

        uiManager.ShowDeathPanel();
        gameObject.SetActive(false);
    }

    public void AddHealth(int amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning("Amount value should be non-negative.");
            return;
        }

        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        UpdateHealthBar();
    }
}
