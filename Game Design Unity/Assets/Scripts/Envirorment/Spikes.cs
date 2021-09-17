using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Plataformer2d
{
    public class Spikes : MonoBehaviour
    {
        [SerializeField]
        private float damage=0;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                //colisiono contra un jugador
                PlayerController player = collision.GetComponent<PlayerController>();
                if (player != null)
                {
                    //hacer daño
                    player.GetDamage(damage);
                    GameManager.instance.RespawnPlayer();
                }
            }
        }
    }
}