using UnityEngine;
using UnityEngine.UI;

public class mimic : MonoBehaviour
{
    private Text interactUI;
    private bool isInRange;

    public Animator animator;
    public int DammageToDeal;

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
        animator.SetTrigger("openMimic");
        vie_joueur.instance.TakeDamage(DammageToDeal);

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
