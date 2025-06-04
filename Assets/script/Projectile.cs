using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 direction;    // Direction du projectile
    private float speed;          // Vitesse du projectile
    private int damage;           // Dégâts infligés

    // Initialiser les paramètres du projectile
    public void Initialize(Vector3 direction, float speed, int damage)
    {
        this.direction = direction;
        this.speed = speed;
        this.damage = damage;
    }

    void Update()
    {
        // Déplacer le projectile
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Vérifier si collision avec un mur
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject); // Détruire le projectile
            return;
        }

        // Vérifier si collision avec le joueur
        if (collision.CompareTag("Player"))
        {
            // Appliquer des dégâts au joueur
            vie_joueur vieJoueur = collision.GetComponent<vie_joueur>();
            if (vieJoueur != null)
            {
                vieJoueur.TakeDamage(damage);
            }
            Destroy(gameObject); // Détruire le projectile
        }
    }
}