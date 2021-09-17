﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Plataformer2d
{
    public class KeyCheckTrigger : MonoBehaviour
    {
        public UnityEvent triggerEnter = new UnityEvent();
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!enabled)
            {
                return;
            }
            if (collision.CompareTag("Player"))
            {
                PlayerController player = collision.GetComponent<PlayerController>();
                if (player.hasKey)
                {
                    triggerEnter.Invoke();
                }
            }
        }
    }
}