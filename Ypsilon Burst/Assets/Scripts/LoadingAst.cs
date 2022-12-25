using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class LoadingAst : MonoBehaviour
{
    [SerializeField]private GameObject[] demo;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Light light;
    [SerializeField] private string[] hints;
    private int x, y, z;

    private GameObject s;
    private Color a, b;
    private TextMeshProUGUI hint;
    private TextMeshProUGUI lvl;
    private void Start()
    {        hint = GameObject.Find("hint").GetComponent<TextMeshProUGUI>();
        lvl = GameObject.Find("lvl").GetComponent<TextMeshProUGUI>();
        x = Random.Range(0, 61);
        y = Random.Range(0, 61);
        z = Random.Range(0, 61);

        a = new Color(Random.Range(0 , 1f), Random.Range(0, 1f) , Random.Range(0, 1f));
        b = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));

        s = Instantiate(demo[Random.Range(0, demo.Length)], spawnPoint.position, Quaternion.identity) as GameObject;
        hint.text = hints[Random.Range(0, hints.Length)];
        lvl.text = "level " + Random.Range(5, 25).ToString();
        //Invoke("Loaded", 0.5f);
        SceneManager.LoadScene(PlayerPrefs.GetInt("scene"), LoadSceneMode.Single);
    }
    private void Update()
    {
        s.transform.Rotate(x * Time.deltaTime, y * Time.deltaTime, z * Time.deltaTime);
        light.color = Color.Lerp(a , b, Mathf.PingPong(Time.time, 1));
    }
    private void Loaded()
    {
        Debug.Log("");
        SceneManager.LoadScene(PlayerPrefs.GetInt("scene"), LoadSceneMode.Single);
        Debug.Log("3");
    }
}
