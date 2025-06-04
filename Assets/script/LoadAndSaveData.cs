using UnityEngine;

public class LoadAndSaveData : MonoBehaviour
{
public static LoadAndSaveData instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de LoadAndSaveData dans la scène");
            return;
        }

        instance = this;
    }

    void Start()
    {
        inventaire.instance.coinsCount = PlayerPrefs.GetInt("CoinsCount", 0);
        inventaire.instance.UpdateTextUI();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("CoinsCount", inventaire.instance.coinsCount);
        if(CurrentSceneManager.instance.levelToUnlock > PlayerPrefs.GetInt("levelReached", 1))
        {
            PlayerPrefs.SetInt("levelReached", CurrentSceneManager.instance.levelToUnlock);
        }
    }
}
