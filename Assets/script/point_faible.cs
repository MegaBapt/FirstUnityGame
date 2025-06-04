using UnityEngine;

public class point_faible : MonoBehaviour
{
    public GameObject objectToDestroy;
    public AudioClip killSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayClipat(killSound, transform.position);
            inventaire.instance.AddCoins(1);
            CurrentSceneManager.instance.coinsPickedUpInThisSceneCount++;
            Destroy(objectToDestroy);
        }
    }
}