using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public GameObject PauseUI;

	private bool paused = false;

	private void Start() {
		PauseUI.SetActive(false);	// when we start the game, disable pause menu
	}

	private void Update() {
		if (Input.GetButtonDown("Pause")) {
			paused = !paused;
		}

		PauseUI.SetActive(paused);

		Time.timeScale = paused ? 0 : 1;
	}

	public void Resume() {	// continue playing the game
		paused = false;
	}

	public void Restart() {	// restart the current level
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void MainMenu() {    // return the main menu
		SceneManager.LoadScene(0);	// TODO change this line when the main menu is ready
	}

	public void Quit() {
		Application.Quit();
	}
}
