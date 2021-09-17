using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Plataformer2d
{
    public class PotionCounterHUD : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private TextMeshProUGUI potionCounter;

        private void Awake()
        {
            player.onPotionCounterChange += OnPotionCounterChangeHandler;
        }
        private void OnPotionCounterChangeHandler(int num)
        {
            UpdateCounter(num);
        }
        private void UpdateCounter(int num)
        {
            string updateNum = num.ToString();
            potionCounter.text = updateNum;
        }
    }
}
