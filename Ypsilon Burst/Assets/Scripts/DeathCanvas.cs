using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCanvas : MonoBehaviour
{
    private GameObject deathButtons;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        deathButtons = GameObject.Find("Death Buttons");
        deathButtons.SetActive(false);
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.7f)
        {
            deathButtons.SetActive(true);
        }
    }
}
