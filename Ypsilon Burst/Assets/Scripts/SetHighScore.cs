using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHighScore : MonoBehaviour
{
    // For testing purposes
    [SerializeField] private int Score;
    void Start()
    {
        PlayerPrefs.SetInt("HighScore", Score);
    }

}
