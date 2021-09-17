using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Plataformer2d
{
    public class BossMeleeAttack : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private float knockBack;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // 1 manera de hacerlo
            if (collision.CompareTag("Player"))
            {
                //colisiono contra un enemigo

                PlayerController player = collision.GetComponent<PlayerController>();
                Rigidbody2D playerRB = collision.GetComponent<Rigidbody2D>();

                if (player != null)
                {
                    //hacer daño
                    player.GetDamage(damage);
                    playerRB.AddForce(transform.right * knockBack, ForceMode2D.Impulse);
                }
                else
                {
                    Debug.Log("null player");
                }
            }
        }
    }
}