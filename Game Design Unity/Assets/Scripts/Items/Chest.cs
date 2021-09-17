using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Plataformer2d
{
    public class Chest : MonoBehaviour
    {
        private bool isOpen = false;
        [SerializeField] ButtonPrompt buttonPrompt;
        [SerializeField] private Animator animatorcontroller;
        [SerializeField] private List<GameObject> Items = new List<GameObject>();
        private void Awake()
        {
            buttonPrompt.promptPressed.AddListener(PromptPressedHandler);
        }
        private void PromptPressedHandler()
        {
            OpenChest();
        }
        private void OpenChest()
        {
            if (!isOpen) 
            {
                animatorcontroller.SetTrigger("Open");
                Invoke("InstantiateItems", 0.2f);
                isOpen = true;
                buttonPrompt.enabled = false;
            }
        }
        private void InstantiateItems()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                GameObject item = Instantiate(Items[i],transform.position,transform.rotation);
                Rigidbody2D itemRB = item.GetComponent<Rigidbody2D>();
                float randomFloat = Random.Range(-1f, 1f);
                Vector2 randomDirection = new Vector2(randomFloat, 0);
                itemRB.AddForce( ((Vector2)transform.up + randomDirection).normalized * 3, ForceMode2D.Impulse);
            }
        }
    }
}