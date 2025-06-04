using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCoin : MonoBehaviour
{
    public AudioClip sound;
    public int ValeurPiece;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayClipat(sound, transform.position);
            inventaire.instance.AddCoins(ValeurPiece);
            CurrentSceneManager.instance.coinsPickedUpInThisSceneCount += ValeurPiece;
            Destroy(gameObject);
        }
    }
}
