using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;

        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    // Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // References
    public Player player;
    public FloatingTextManager floatingTextManager;

    // Logic
    public int pesos;
    public int experience;

    // Floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    // Save State
    //int preferedSkin
    //int pesos
    //int experience
    //int weaponLevel

    public void SaveState()
    {
        string s = "";
        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += "0";


        PlayerPrefs.SetString("SaveState", s);

        Debug.Log("Save state!");
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        if (!PlayerPrefs.HasKey("SaveState")) return;

        // Change player skin
        pesos = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        // Change weapon level

        Debug.Log("Load state!");
    }


}
