using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {

        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            return;
        }

        instance = this;
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
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public Animator deathImage;


    // Logic
    public int pesos;
    public int experience;

    // Floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    { 
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    // Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        // is the weapon max level?
        if (weaponPrices.Count <= weapon.weaponLevel) return false;

        if (pesos >= weaponPrices[weapon.weaponLevel])
        {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    //Experience System
    public int GetCurrentLevel()
    {
        int counter = 0;
        int add = 0;

        while (experience >= add)
        {
            add += xpTable[counter];
            counter++;

            if (counter == xpTable.Count) // Max level
            {
                return counter;
            }
        }

        return counter;

    }

    public int GetXpToLevel(int level)
    {
        int counter = 0;
        int xp = 0;
        while (counter < level)
        {
            xp += xpTable[counter];
            counter++;
        }

        return xp;
    }

    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel())
        {
            OnLevelUp();
        }
    }

    public void OnLevelUp()
    {
        Debug.Log("LEVELED UP!");
        player.OnLevelUp();
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
        s += weapon.weaponLevel.ToString();


        PlayerPrefs.SetString("SaveState", s);

        Debug.Log("Save state!");
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        Debug.Log("Load state!");

        if (!PlayerPrefs.HasKey("SaveState")) return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');


        // Change player skin
        pesos = int.Parse(data[1]);

        //Experience
        experience = int.Parse(data[2]);
        if (GetCurrentLevel() != 1)
        {
            player.SetLevel(GetCurrentLevel());
        }

        // Change weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));

        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    // Deathmenu and respawn
    public void Respawn()
    {
        deathImage.SetTrigger("Hide");
        SceneManager.LoadScene("Main");
    }
}
