using UnityEngine;
using System.Collections;

public class vie_joueur : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float invicibility_time = 3f;
    public float InvicibilityFlashDelay = 0.2f;

    public bool isinvicible = false;

    public barre_de_vie healthbar;
    public SpriteRenderer graphics;

    public AudioClip hitSound;

    public static vie_joueur instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(60);
        }
    }

    public void HealPlayer(int amount)
    {
        if((currentHealth + amount) > maxHealth)
        {
            currentHealth = maxHealth;
        }else
        {
            currentHealth += amount;
        }
        healthbar.SetHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if(!isinvicible && !mouvement_joueur.instance.isInvincible)
        {
            AudioManager.instance.PlayClipat(hitSound, transform.position);
            currentHealth -= damage;
            healthbar.SetHealth(currentHealth);
 
            if(currentHealth < 1)
            {
                Die();
                return;
            }

            isinvicible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    public void TakeDamage2(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
        AudioManager.instance.PlayClipat(hitSound, transform.position);
        if(currentHealth < 1)
        {
            Die();
            return;
        }
    }

    public void Die()
    {
        mouvement_joueur.instance.enabled = false;
        mouvement_joueur.instance.animator.SetTrigger("Die");
        mouvement_joueur.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        mouvement_joueur.instance.rb.velocity = Vector3.zero;
        mouvement_joueur.instance.player_Collider.enabled = false;
        GameOverManager.instance.OnPlayerDeath();
    }

    public void Respawn()
    {
        mouvement_joueur.instance.enabled = true;
        mouvement_joueur.instance.animator.SetTrigger("respawn");
        mouvement_joueur.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        mouvement_joueur.instance.player_Collider.enabled = true;
        currentHealth = maxHealth;
        healthbar.SetHealth(currentHealth);
    }

    public IEnumerator InvincibilityFlash()
    {
        while (isinvicible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(InvicibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(InvicibilityFlashDelay);
        }
    }
    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(invicibility_time);
        isinvicible = false;
    }
}