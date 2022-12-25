using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    //public GameObject obj;
    public float minimum = 0.0f;
         public float maximum = 1f;
         public float duration = 5.0f;
         private float startTime;
    private float Buffer;
    float t;        
         public Image sprite;
         bool Faded_In = false;
    bool Faded_Out = false;
         void Start()
         {                        
        startTime = Time.time;
         }
    void Update()
    {
        if (Faded_In)
        {
            Faded_Out = true;
            t = (Time.time - startTime) / duration;
            sprite.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(maximum, minimum, t));
        }
        else
        {
            t = (Time.time - startTime) / duration;
            sprite.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(minimum, maximum, t));
        }
        if (sprite.color == new Color(1f, 1f, 1f, 1f))
        {
            Faded_In = true;
            startTime = Time.time;
        }
        if (Faded_Out && sprite.color == new Color(1f, 1f, 1f, 0f))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);



    }
     
}
