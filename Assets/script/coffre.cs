using UnityEngine;
using UnityEngine.UI;

public class coffre : MonoBehaviour
{
    private Text interactUI;
    private bool isInRange;

    public Animator animator;
    public int coinsToAdd;
    public int lifeToAdd;
    public AudioClip soundToPlay;

    void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("interactUI").GetComponent<Text>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        animator.SetTrigger("OpenChest");
        inventaire.instance.AddCoins(coinsToAdd);
        vie_joueur.instance.HealPlayer(lifeToAdd);
        AudioManager.instance.PlayClipat(soundToPlay, transform.position);
        GetComponent<BoxCollider2D>().enabled = false;
        interactUI.enabled = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            interactUI.enabled = true;
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            {
                interactUI.enabled = false;
                isInRange = false;
            }
    }
}
