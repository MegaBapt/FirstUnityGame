using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{
    private Text interactUI;
    private bool isInRange;

    public Item item;
    public AudioClip soundToPlay;

    void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("interactUI").GetComponent<Text>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            TakeItem();
        }
    }

    void TakeItem()
    {
        inventaire.instance.content.Add(item);
        inventaire.instance.UpdateInventory();
        AudioManager.instance.PlayClipat(soundToPlay, transform.position);
        interactUI.enabled = false;
        Destroy(gameObject);
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
