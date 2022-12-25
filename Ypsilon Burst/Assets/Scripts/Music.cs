using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] music;
    private float timer;
    private float clipLength;
    private int index;
    void Awake()
    {
        index = Random.Range(0, music.Length);
        audioSource.clip = music[index];
        audioSource.Play();
        timer = 0f;
        switch (index)
        {
            case 0:
                clipLength = 61f;
                break;
            case 1:
                clipLength = 45f;
                break;
            case 2:
                clipLength = 76f;
                break;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= clipLength) Awake();
    }
}
