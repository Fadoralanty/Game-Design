using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Plataformer2d
{
    public class HPbarController : MonoBehaviour
    {
        [SerializeField] PlayerController playerController;
        [SerializeField] Image lifeBar;
        private void Awake()
        {
            playerController.onLifeChange += OnLifeChangeHandler;
            UpdateLifeBar();
        }
        private void OnLifeChangeHandler(float currentLife)
        {
            UpdateLifeBar();
        }
        private void UpdateLifeBar()
        {
            lifeBar.fillAmount = playerController.GetLifePercentage();
        }
    }
}