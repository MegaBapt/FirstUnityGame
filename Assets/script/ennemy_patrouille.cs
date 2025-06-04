using UnityEngine;

public class ennemy_patrouille : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;
    public int degat_collision;

    public SpriteRenderer graphique;
    private Transform target;
    private int destPoint = 0;

    void Start()
    {
        target = waypoints[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        graphique.flipX = IsFlip(dir);

        //si l'ennemi est quasiment arriver a sa destination
        if(Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            vie_joueur playerHealth = collision.transform.GetComponent<vie_joueur>();
            playerHealth.TakeDamage(degat_collision);
        }
    }

    private bool IsFlip(Vector3 dir)
    {
        
        return 0 > dir.x;
    }
    
}
