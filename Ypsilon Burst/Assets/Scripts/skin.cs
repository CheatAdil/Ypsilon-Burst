using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class skin : MonoBehaviour
{
    [SerializeField] private GameObject[] models; 
    private bool[] unlocked;
    private int i;

    private GameObject player, playerpos;
    private Transform placement;

    private GameObject SaveButton;
    private TextMeshProUGUI SaveText;

    private int highScore;
    private int scoreToUnlock = 2500;
    private void Awake()
    {
        unlocked = new bool[models.Length];
        highScore = PlayerPrefs.GetInt("HighScore");
        for (int a = 0; a <= highScore / scoreToUnlock; a++)
            unlocked[a] = true;
        SaveButton = GameObject.Find("Save");
        SaveText = GameObject.Find("SaveT").GetComponent<TextMeshProUGUI>();
        playerpos = GameObject.Find("Player");
        placement = playerpos.transform;
        Destroy(playerpos);
        if (PlayerPrefs.HasKey("skin"))
        {
            i = PlayerPrefs.GetInt("skin");
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
        SetSkin(i);
    }
    public void GoRight() 
    {
        if (i < models.Length - 1) 
        {
            i++;
            Destroy(player); 
            SetSkin(i);
        }   
    }
    public void GoLeft() 
    {
        if (i > 0) 
        {
            i--;
            Destroy(player);
            SetSkin(i);
        }
    }
    private void SetSkin(int index) 
    {

        if (models[index] != null) 
        {
            player = Instantiate(models[index], placement);
            player.name = "Player";          
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
            PlayerPrefs.SetInt("skin", i);
            PlayerPrefs.Save();
        }      
    }
}