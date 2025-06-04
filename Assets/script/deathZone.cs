using System.Collections;
using UnityEngine;

public class deathZone : MonoBehaviour
{
    public int degat_collision;
    private Animator fadeSystem;
    public SpriteRenderer graphics;

    private void Awake()
    {
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(ReplacePlayer(collision));
            vie_joueur playerHealth = collision.transform.GetComponent<vie_joueur>();
            playerHealth.TakeDamage2(degat_collision);
        }
    }

        public IEnumerator ReplacePlayer(Collider2D collision)
    {
        graphics.color = new Color(1f, 1f, 1f, 0f);
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        graphics.color = new Color(1f, 1f, 1f, 1f);
        collision.transform.position = CurrentSceneManager.instance.respawnPoint;
    }
}