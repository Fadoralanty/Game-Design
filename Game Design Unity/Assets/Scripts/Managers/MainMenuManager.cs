using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Plataformer2d
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private Button buttonStart=null;
        [SerializeField] private Button buttonQuit=null;
        private void Awake()
        {
            buttonStart.onClick.AddListener(StartOnClickHandler);
            buttonQuit.onClick.AddListener(QuitOnClickHandler);
        }
        private void StartOnClickHandler()
        {
            SceneManager.LoadScene("Level 1");
        }
        private void QuitOnClickHandler()
        {
            Application.Quit();
        }
    }
}