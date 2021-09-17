using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Plataformer2d
{
    public class BossHPbar : MonoBehaviour
    {
        [SerializeField] BossBehaviour Boss;
        [SerializeField] Image lifeBar;
        private void Awake()
        {
            Boss.onLifeChange += OnLifeChangeHandler;
            UpdateLifeBar();
        }
        private void OnLifeChangeHandler(float currentLife)
        {
            UpdateLifeBar();
        }
        private void UpdateLifeBar()
        {
            lifeBar.fillAmount = Boss.GetLifePercentage();
        }

    }
}