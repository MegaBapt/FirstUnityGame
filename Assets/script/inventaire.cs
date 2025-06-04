using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class inventaire : MonoBehaviour
{
    public int coinsCount;
    public Text coinsCountText;

    public List<Item> content = new List<Item>();
    public int contentCurrentIndex = 0;
    public Image ItemImageUI;
    public Text itemNameUI;
    public Sprite emptyItemImage;
    public PlayerEffects playerEffects;

    public static inventaire instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("fait gaffe il y as trop d'instance dans l'inventaire");
            return;
        }

        instance = this;
    }

    private void Start()
    {
        // Initialisation de playerEffects
        if (playerEffects == null)
        {
            Debug.LogError("PlayerEffects n'est pas attaché au même GameObject que Inventaire !");
        }
        UpdateInventory();
    }

    public void ComsumeItem()
    {
        if (content.Count == 0)
        {
            return;
        }

        if (vie_joueur.instance == null)
        {
            vie_joueur vj = FindObjectOfType<vie_joueur>();
            if (vj != null)
            {
                Debug.LogWarning("Instance non trouvée automatiquement, référence manuelle utilisée.");
                vie_joueur.instance = vj;
            }
            else
            {
                Debug.LogError("Aucun objet contenant vie_joueur trouvé dans la scène !");
            }
        }

        Item currentItem = content[contentCurrentIndex];
        vie_joueur.instance.HealPlayer(currentItem.hpGiven);
        playerEffects.AddSpeed(currentItem.speedGiven, currentItem.speedDuration);
        playerEffects.AddJump(currentItem.JumpBoostGiven, currentItem.JumpBoostDuration);
        playerEffects.StartSlowMotion(currentItem.TimeSlow, currentItem.TimeSlowDuration);
        content.Remove(currentItem);
        GetNextItem();
        UpdateInventory();
    }

    public void GetNextItem()
    {
        if (content.Count == 0)
        {
            return;
        }

        contentCurrentIndex++;
        if (content.Count - 1 < contentCurrentIndex)
        {
            contentCurrentIndex = 0;
        }
        UpdateInventory();
    }

    public void GetPreviousItem()
    {
        if (content.Count == 0)
        {
            return;
        }

        contentCurrentIndex--;
        if (0 > contentCurrentIndex)
        {
            contentCurrentIndex = content.Count - 1;
        }
        UpdateInventory();
    }

    public void UpdateInventory()
    {
        if (content.Count > 0)
        {
            ItemImageUI.sprite = content[contentCurrentIndex].image;
            itemNameUI.text = content[contentCurrentIndex].name;
        }else
        {
            ItemImageUI.sprite = emptyItemImage;
            itemNameUI.text = "";
        }
    }

    public void AddCoins(int count)
    {
        coinsCount += count;
        UpdateTextUI();
    }

    public void RemoveCoins(int count)
    {
        coinsCount -= count;
        UpdateTextUI();
    }

    public void UpdateTextUI()
    {
        coinsCountText.text = coinsCount.ToString();
    }
}
