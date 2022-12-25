using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player, pause, pausedMenu;
    void Start()
    {
        pausedMenu = GameObject.Find("PausedMenu");
        pausedMenu.SetActive(false);
        player = GameObject.Find("Player");
        pause = GameObject.Find("Pause");
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) player = GameObject.Find("Player");
        if (player != null) transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 15, player.transform.position.z);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pause.SetActive(false);
        pausedMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pausedMenu.SetActive(false);
        pause.SetActive(true);
    }
}
