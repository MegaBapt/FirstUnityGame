using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;
    private bool isInRange;
    private Text interactUI;

    void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("interactUI").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange && Input.GetKeyDown(KeyCode.E))
        {
            TriggerDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isInRange = true;
            interactUI.enabled = true;
        }
    }

    private void OnTriggerExit2D (Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isInRange = false;
            interactUI.enabled = false;
            DialogueManager.instance.EndDialogue();
        }
    }

    void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(dialogue);
    }
}

