using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Plataformer2d
{
    public class TheEndScreenManager : MonoBehaviour
    {
        [SerializeField] private Button buttonMainMenu = null;
        [SerializeField] private Button buttonQuit = null;
        private void Awake()
        {
            buttonMainMenu.onClick.AddListener(MainMenuOnClickHandler);
            buttonQuit.onClick.AddListener(QuitOnClickHandler);
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