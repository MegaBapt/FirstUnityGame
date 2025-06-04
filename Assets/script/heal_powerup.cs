using UnityEngine;

public class heal_powerup : MonoBehaviour
{
    public int healthPoints;
    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayClipat(pickupSound, transform.position);
            vie_joueur.instance.HealPlayer(healthPoints);
            Destroy(gameObject);
        }
    }
}
