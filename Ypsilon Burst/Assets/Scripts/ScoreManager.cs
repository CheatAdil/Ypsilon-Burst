using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI EnergyText;
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI DeathMessage;
    [SerializeField] private TextMeshProUGUI TimerText;
    private float timer;
    public int Score;
    private Weapons weapons;
    private void Start()
    {
        weapons = GameObject.Find("Player").GetComponent<Weapons>();
        ScoreText.text = "Score: " + Score;
        EnergyText.text = "Energy: " + weapons.energy;
    }
    public void GiveScore(int amount) 
    {
        Score += amount;
        ScoreText.text = "Score: " + Score;
    }
    public void EnergyChange()
    {
        EnergyText.text = "Energy: " + weapons.energy;
    }
    private void Update()
    {
        if(weapons == null) weapons = GameObject.Find("Player").GetComponent<Weapons>();
        timer += Time.deltaTime;
        TimerText.text = "Mission Time: " + System.Convert.ToInt32(timer) +" s";
    }
    private void CheckScore()
    {
        if (PlayerPrefs.GetInt("HighScore") < Score)
        {
            PlayerPrefs.SetInt("HighScore", System.Convert.ToInt32(Score));
            PlayerPrefs.Save();
            DeathMessage.text = "New record!";
        }
    }
}
