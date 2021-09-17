using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Plataformer2d
{
    public class ButtonPrompt : MonoBehaviour
    {
        public UnityEvent promptPressed = new UnityEvent();
        private bool playerOnTrigger = false;
        [SerializeField] GameObject buttonSprite;
        private void Update()
        {
            if (playerOnTrigger)
            {
                if (Input.GetButtonDown("Fire1")) 
                {
                    promptPressed.Invoke();
                    //Debug.Log("entro al evento");
                }
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!enabled)
            {
                return;
            }
            if (collision.CompareTag("Player"))
            {
                playerOnTrigger = true;
                buttonSprite.SetActive(true);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!enabled)
            {
                return;
            }
            if (collision.CompareTag("Player"))
            {
                playerOnTrigger = false;
                buttonSprite.SetActive(false);
            }
        }
        private void OnDisable()
        {
            buttonSprite.SetActive(false);
        }
    }
}