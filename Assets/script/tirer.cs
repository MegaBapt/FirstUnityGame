using UnityEngine;
using System.Collections;

public class tirer : MonoBehaviour
{
    public GameObject projectilePrefab;    // Le prefab du projectile
    public float projectileSpeed = 5f;     // Vitesse du projectile
    public int damageAmount = 10;         // Quantité de dégâts infligés au joueur
    public float fireRate = 1f;

    private GameObject currentProjectile;  // Référence au projectile actif
    private Transform player;             // Référence à la position du joueur
    private Vector3 directionToPlayer;    // Direction initiale vers le joueur
    private float nextFireTime;  

    void Start()
    {
        // Trouver le joueur au démarrage
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("Player not found! Assurez-vous que le joueur a le tag 'Player'.");
        }
        nextFireTime = Time.time; // Initialiser pour permettre un tir immédiat
    }

    void Update()
    {
        // Lancer le projectile si aucun n'est actif
        if (currentProjectile == null && Time.time >= nextFireTime)
        {
            LaunchProjectile();
        }

        // Gérer le projectile s'il existe
        if (currentProjectile != null)
        {
            HandleProjectile();
        }
    }

    void LaunchProjectile()
    {
        if (player == null) return;

        // Instancier le projectile à la position de l'ennemi
        currentProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Calculer la direction vers le joueur
        directionToPlayer = (player.position - transform.position).normalized;

        // Passer les données nécessaires au projectile
        Projectile projectileScript = currentProjectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.Initialize(directionToPlayer, projectileSpeed, damageAmount);
        }

        // Mettre à jour le temps du prochain tir
        nextFireTime = Time.time + fireRate;
    }

    void HandleProjectile()
    {
        // Vérifier si hors écran
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(currentProjectile.transform.position);
        if (viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1)
        {
            StartCoroutine(TeleportAndDestroy());
        }
    }

    IEnumerator TeleportAndDestroy()
    {
        // Téléporter le projectile à la position de l'ennemi
        currentProjectile.transform.position = transform.position;
        yield return new WaitForSeconds(0f);
        Destroy(currentProjectile);
        currentProjectile = null; // Réinitialiser pour permettre un nouveau tir
    }
}