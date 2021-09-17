using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Plataformer2d
{
    public class TriggerEvent : MonoBehaviour
    {
        [SerializeField] private string triggerTag = "Player";
        public UnityEvent triggerEnter = new UnityEvent();
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!enabled)
            {
                return;
            }
            if (collision.CompareTag(triggerTag))
            {
                triggerEnter.Invoke();
            }
        }
    }
}