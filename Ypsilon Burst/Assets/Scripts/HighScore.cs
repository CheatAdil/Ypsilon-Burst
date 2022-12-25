using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreText;
    void Start()
    {
        if (ScoreText != null) ScoreText.text = "High score: " + PlayerPrefs.GetInt("HighScore");
    }
}
