using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plataformer2d
{
    public class proyectile : MonoBehaviour // este script define el comportamiento de la 'bala' o proyectil
    {
        [SerializeField]
        private float lifetime=5f;//tiempo de vida del proyectil en segundos

        [SerializeField]
        private int damage=10;

        private float currentlifetime;//tiempo de vida actual

        private void Update()
        {
            //Timer para destruir el proyectil
            currentlifetime += Time.deltaTime;//le sumo el paso de tiempo transcurrido al actual

            if (currentlifetime >= lifetime)//si supera el tiempo de vida lo destruyo 
            {
                Destroy(gameObject); //gameObject con g minuscula referencia al objeto en el que estoy al cual esta asignado este script como componente
                //Destroy() elimina el gameobject del juego, entonces libera la memoria/destruye al objeto 
            }
        }
        //OnTriggerEnter2D() es colision entre triggers
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // 1 manera de hacerlo
            if (collision.CompareTag("Player"))
            {
                //colisiono contra un jugador

                PlayerController player = collision.GetComponent<PlayerController>();
                Rigidbody2D playerRigidBody = collision.GetComponent<Rigidbody2D>();
                //Debug.Log(player);
                if (player != null)
                {
                    //hacer daño
                    player.GetDamage(damage);
                    playerRigidBody.AddForce(transform.right * 8, ForceMode2D.Impulse);
                    Destroy(gameObject);
                }
            }
            if (collision.CompareTag("TilemapFloor"))
            {
                Destroy(gameObject);
            }
        }
        
        // otra manera de hacer daño por colisiones 
        /*private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "player") 
            {

                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                if (player != null)
                {
                    //hacer daño
                    player.GetDamage(damage);
                    Destroy(gameObject);

                }
            }
        }*/
    }
}
