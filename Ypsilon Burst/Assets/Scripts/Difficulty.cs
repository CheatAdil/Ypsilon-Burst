using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Difficulty : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI difficultyText;

    private bool isEasy;
    private bool isNormal;
    private bool isHard;
    private bool isImpossible;

    private float easy;
    private float normal;
    private float hard;
    private float impossible;
    private float timer;

    public int enemyDamage;
    public int enemyHealth;
    public int smallAsteroidHealth;
    public int bigAsteroidHealth;
    public int goldAsteroidHealth;
    public int bonusAsteroidHealth;
    public float asteroidDamage;
    public float bonusChance;
    public int energySpend;
    
    void Start()
    {
        easy = 0f;
        normal = 300f;
        hard = 600f;
        impossible = 900f;
        Easy();
    }
    private void Easy()
    {
        difficultyText.text = "Difficulty: Easy";
        enemyDamage = 20;
        enemyHealth = 60;
        asteroidDamage = 5f;
        smallAsteroidHealth = 40;
        bigAsteroidHealth = 60;
        goldAsteroidHealth = 80;
        bonusAsteroidHealth = 100;
        energySpend = 1;
        Invoke("Normal", 50);
    }
    private void Normal()
    {
        difficultyText.text = "Difficulty: normal";
        enemyDamage = 30;
        enemyHealth = 80;
        asteroidDamage = 10f;
        smallAsteroidHealth = 60;
        bigAsteroidHealth = 80;
        goldAsteroidHealth = 100;
        bonusAsteroidHealth = 120;
        energySpend = 1;
        Invoke("Hard", 50);
    }
    private void Hard()
    {
        difficultyText.text = "Difficulty: hard";
        enemyDamage = 40;
        enemyHealth = 100;
        asteroidDamage = 35f;
        smallAsteroidHealth = 60;
        bigAsteroidHealth = 80;
        goldAsteroidHealth = 120;
        bonusAsteroidHealth = 200;
        energySpend = 2;
        Invoke("Impossible", 50);
    }
    private void Impossible()
    {
        difficultyText.text = "Difficulty: impossible";
        enemyDamage = 50;
        enemyHealth = 120;
        asteroidDamage = 60f;
        smallAsteroidHealth = 60;
        bigAsteroidHealth = 100;
        goldAsteroidHealth = 140;
        bonusAsteroidHealth = 200;
        energySpend = 3;
    }
}
