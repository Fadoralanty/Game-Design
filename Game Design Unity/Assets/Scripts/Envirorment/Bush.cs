using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Plataformer2d
{
    public class Bush : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem particle=null;

        private ParticleSystem leafsway;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Meleeattack"))
            {
                PlayerMeleeAttack hitbox = collision.GetComponent<PlayerMeleeAttack>();
                if (hitbox != null)
                {
                    Explode();
                    Destroy(gameObject);
                }
                Arrow arrow = collision.GetComponent<Arrow>();
                if (arrow != null)
                {
                    Explode();
                    Destroy(gameObject);
                }
            }
        }
        private void Explode()
        {
            leafsway=Instantiate(particle,transform.position,transform.rotation,null);
            particle.Play();
        }
    }
}