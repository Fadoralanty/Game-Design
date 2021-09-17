using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Plataformer2d
{
    public class ContactDamage : MonoBehaviour
    {
        [SerializeField]
        private Enemy enemy=default;
        private float lastDamageTime = 0f;
        private float damageCooldown = 2f;
        PlayerController lastPlayer;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                //colisiono contra un jugador
                PlayerController player = collision.GetComponent<PlayerController>();
                if (player != null)
                {
                    lastPlayer = player;
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                PlayerController player = collision.GetComponent<PlayerController>();
                if (player != null)
                {
                    lastPlayer = null;
                }
            }
        }
        private void Update()
        {
            if (Time.time - lastDamageTime >= damageCooldown && lastPlayer!=null)
            {
                lastPlayer.GetDamage(enemy.damage);
                lastDamageTime = Time.time;
            }
        }
    }
}