using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plataformer2d
{
    public class PlayerMeleeAttack : MonoBehaviour
    {
        private AudioSource audiosource;
        private float TimeToPlayAudio = 0.2f;
        private float AudioCooldown;
        [SerializeField] private PlayerController player=null;
        [SerializeField] float addedDamage=0;
        private void Start()
        {
            AudioCooldown = 0f;
            audiosource = GetComponent<AudioSource>();
        }
        private void Update()
        {
            AudioCooldown -= Time.deltaTime;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // 1 manera de hacerlo
            if (collision.CompareTag("Enemy"))
            {
                //colisiono contra un enemigo

                Enemy enemy = collision.GetComponent<Enemy>();
                Rigidbody2D enemyRB = collision.GetComponent<Rigidbody2D>();

                if (enemy != null)
                {
                    //hacer daño
                    if (AudioCooldown <= 0)
                    {
                        enemy.GetDamage(player.damage + addedDamage);
                        enemyRB.AddForce(transform.right * player.knockback, ForceMode2D.Impulse);
                        audiosource.Play();
                        AudioCooldown = TimeToPlayAudio;
                    }

                }
                else
                {
                    Debug.Log("null enemy");
                }
            }
        }
    }
}