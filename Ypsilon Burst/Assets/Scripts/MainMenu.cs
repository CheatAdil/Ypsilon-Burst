using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField] GameObject tutorial;
	[SerializeField] GameObject tutorialButton;
	public void Play()
	{
		SceneManager.LoadSceneAsync(4, LoadSceneMode.Single);
		PlayerPrefs.SetInt("scene", 2);
		PlayerPrefs.Save();
	}
	public void Menu()
	{
		SceneManager.LoadSceneAsync(4, LoadSceneMode.Single);
		PlayerPrefs.SetInt("scene", 1);
		PlayerPrefs.Save();
	}
	public void Skin()
	{
		SceneManager.LoadScene(4, LoadSceneMode.Single);
		PlayerPrefs.SetInt("scene", 3);
	}
	public void Weapons()
	{
		SceneManager.LoadScene(4, LoadSceneMode.Single);
		PlayerPrefs.SetInt("scene", 5);
	}
	public void EnterTutorial()
	{
		tutorial.SetActive(true);
		tutorialButton.SetActive(false);
	}
	public void ExitTutorial()
	{
		tutorial.SetActive(false);
		tutorialButton.SetActive(true);
	}
}
