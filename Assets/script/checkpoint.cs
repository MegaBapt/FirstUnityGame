using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CurrentSceneManager.instance.respawnPoint = transform.position;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
