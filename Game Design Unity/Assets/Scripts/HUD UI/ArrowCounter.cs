using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Plataformer2d
{
    public class ArrowCounter : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private TextMeshProUGUI arrowCounter;

        private void Awake()
        {
            player.onArrowsCounterChange += OnArrowCounterChangeHandler;
        }
        private void OnArrowCounterChangeHandler(int num)
        {
            UpdateCounter(num);
        }
        private void UpdateCounter(int num)
        {
            string updateNum = num.ToString();
            arrowCounter.text = updateNum;
        }
    }
}