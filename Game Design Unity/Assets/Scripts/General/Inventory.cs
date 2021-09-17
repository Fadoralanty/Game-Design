using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Plataformer2d
{
    public class Inventory : MonoBehaviour
    {
        public bool[] isfull;
        public GameObject[] slots;

        [SerializeField]
        private GameObject inventory=null;

        private bool isEnabled=false;
        private float time = 1f;
        private void Start()
        {
            inventory.SetActive(isEnabled);
        }
        private void Update()
        {
            if (isEnabled)
            {
                time = 1f;
            }
            else
            {
                time = 0f;
            }
            if (Input.GetButtonDown("Inventory"))
            {
                isEnabled = !isEnabled;
                inventory.SetActive(isEnabled);
                Time.timeScale = time;
            }
        }
    }
}