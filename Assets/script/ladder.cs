using UnityEngine;
using UnityEngine.UI;

public class ladder : MonoBehaviour
{
    private bool isInRange;
    private mouvement_joueur playerMovement;
    public BoxCollider2D topcollider;
    private Text interactUI;

    void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<mouvement_joueur>();
        interactUI = GameObject.FindGameObjectWithTag("interactUI").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange && playerMovement.isClimbing && Input.GetKeyDown(KeyCode.E))
        {
            playerMovement.isClimbing = false;
            topcollider.isTrigger = false;
            return;
        }
        if(isInRange && Input.GetKeyDown(KeyCode.E))
        {
            playerMovement.isClimbing = true;
            topcollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactUI.enabled = true;
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactUI.enabled = false;
            isInRange = false;
            playerMovement.isClimbing = false;
            topcollider.isTrigger = false;
        }
    }
}
