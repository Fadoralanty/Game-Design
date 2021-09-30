using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Plataformer2d
{
    public class HPbarController : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public void SetMaxHealth(float amount)
        {
            _slider.maxValue = amount;
            _slider.value = amount;
        }

        public void SetHealth(float amount)
        {
            _slider.value = amount;
        }
    }
}