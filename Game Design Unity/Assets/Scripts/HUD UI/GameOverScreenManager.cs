using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Plataformer2d
{
    public class GameOverScreenManager : MonoBehaviour
    {
        [SerializeField] private Button buttonRestart = null;
        [SerializeField] private Button buttonMainMenu = null;
        [SerializeField] private Button buttonQuit = null;
        private void Awake()
        {
            buttonRestart.onClick.AddListener(RestartOnClickHandler);
            buttonMainMenu.onClick.AddListener(MainMenuOnClickHandler);
            buttonQuit.onClick.AddListener(QuitOnClickHandler);
        }
        private void RestartOnClickHandler()
        {
            SceneManager.LoadScene("Level 1");
        }
        private void MainMenuOnClickHandler()
        {
            SceneManager.LoadScene("Main Menu");
        }
        private void QuitOnClickHandler()
        {
            Application.Quit();
        }
    }
}