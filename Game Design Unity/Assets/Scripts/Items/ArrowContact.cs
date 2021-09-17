using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Plataformer2d
{
    public class ArrowContact : MonoBehaviour
    {
        [SerializeField]
        private Arrow arrow = default;
        private void OnTriggerEnter2D(Collider2D collision)//se ejecuta 1 vez
        {       
            if (collision.CompareTag("Enemy"))
            {
                //colisiono contra un Enemy
                Enemy enemy = collision.GetComponent<Enemy>();
                if (enemy != null)
                {
                    //hacer daño
                    enemy.GetDamage(arrow.damage);
                }
            }
        }
    }
}