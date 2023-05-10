using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseControl : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public static bool isGamePaused;

    GameObject gun, player;

    void Start() {
        gun = FindObjectOfType<RotateTowardMouse>().gameObject;
        player = GameObject.FindWithTag("Player");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && player) {
            if (isGamePaused) Resume();
            else Pause();
        }
    }

    void Pause() {
        Time.timeScale = 0;
        isGamePaused = true;
        pauseMenu.SetActive(true);
        gun.SetActive(false);
    }

    public void Resume() {
        Time.timeScale = 1;
        isGamePaused = false;
        pauseMenu.SetActive(false);
        gun.SetActive(true);
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Resume();
    }

    public void MainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
