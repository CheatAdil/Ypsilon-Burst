using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChoose : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;
    [SerializeField] private GameObject[] models;
    private bool[] unlocked;
    private int i;

    private GameObject player, playerpos;
    private Transform placement;

    private GameObject SaveButton;
    private TextMeshProUGUI SaveText;

    private int highScore;
    private WeaponShow weaponshow;
    //private int scoreToUnlock = 2500;
    private void Awake()
    {
        unlocked = new bool[weapons.Length];
        highScore = PlayerPrefs.GetInt("HighScore");
        for (int a = 0; a < weapons.Length; a++)
            unlocked[a] = true;
        SaveButton = GameObject.Find("Save");
        SaveText = GameObject.Find("SaveT").GetComponent<TextMeshProUGUI>();
        playerpos = GameObject.Find("Player");
        placement = playerpos.transform;
        Destroy(playerpos);
        if (PlayerPrefs.HasKey("weapon"))
        {
            i = PlayerPrefs.GetInt("weapon");
        }
        else i = 0;
        Debug.Log(i);
    }
    private void Start()
    {
        Invoke("Starter", 0.001f);
    }
    private void Update()
    {
        if (player != null) player.transform.Rotate(0, 60 * Time.deltaTime, 0);
    }
    public void Starter() 
    {
        player = Instantiate(models[PlayerPrefs.GetInt("skin")], placement);
        player.name = "Player";
        weaponshow = player.AddComponent(typeof(WeaponShow)) as WeaponShow;
        SetSkin(i);
    }
    public void GoRight() 
    {
        if (i < weapons.Length - 1) 
        {
            i++;
            SetSkin(i);
        }   
    }
    public void GoLeft() 
    {
        if (i > 0) 
        {
            i--;
            SetSkin(i);
        }
    }
    private void SetSkin(int index) 
    {

        if (weapons[index] != null) 
        {
            weaponshow.weapon = weapons[index];
            weaponshow.Start();
        }
        else
        {
            Debug.LogError("KORE GA REQUEM DA");
        }
        if (unlocked[index] == false) 
        {
            SaveButton.GetComponent<Button>().interactable = false;
            SaveText.text = "locked";
        }
        else 
        {
            SaveButton.GetComponent<Button>().interactable = true;
            SaveText.text = "use";
        }
    }
    public void SaveSkin() 
    {
        if (unlocked[i] == true)
        {
            PlayerPrefs.SetInt("weapon", i);
            PlayerPrefs.Save();
        }      
    }
}