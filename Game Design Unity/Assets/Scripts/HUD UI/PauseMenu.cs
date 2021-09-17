using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Codigo sacado de video de Brackeys en youtube
//Link:https://www.youtube.com/watch?v=JivuXdrIHK0&list=WL&index=91&t=4s&ab_channel=Brackeys
//TODO implementar animacion de transicion al menu de pausa
namespace Plataformer2d {
    public class PauseMenu : MonoBehaviour
    {
        public static bool gameIsPaused = false;
        [SerializeField]
        public GameObject pauseMenuUI=null;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.Joystick1Button9))
            {
                if (gameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
        }
        private void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;

        }
        public void LoadCheckpoint()
        {
            GameManager.instance.RestartPlayer();
            Time.timeScale = 1f;
            gameIsPaused = false;
            pauseMenuUI.SetActive(false);
        }
        public void LoadMenu()
        {
            Time.timeScale=1f;
            SceneManager.LoadScene("Main Menu");
        }
        public void QuitGame()
        {
            Debug.Log("quit game");
            Application.Quit();
        }
    }
}