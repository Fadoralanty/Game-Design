using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Plataformer2d
{
    public class Bat : Enemy
    {
        //Slime Behaviour
        [SerializeField]
        private Transform player = null;
        private Rigidbody2D myRigidBody = null;
        private Vector2 dirFollow;
        private float rotZ;
        private float rotationSpeed = 4;
        protected override void Awake()
        {
            base.Awake();
            myRigidBody = GetComponent<Rigidbody2D>();
        }
        private void FixedUpdate()
        {
            if (player != null)
            {
                dirFollow = Vector2.zero;
                //Vector3 dirFlee = new Vector3(transform.position.x - player.position.x, transform.position.y - player.position.y, 0);//para que se aleje del jugador, invertis el orden de la resta y asi, ya se aleja el Bat del player
                float distance = Vector3.Distance(player.position, transform.position);

                if (distance < 0.5f) //perseguir al player si esta dentro de ese intervalo de distancia
                {
                    //me alejo del player
                    dirFollow = transform.position - player.position;
                    animatorcontroller.SetBool("IsMoving", true);
                }
                else if (distance > 1 && distance < 10)
                {
                    //Me acerco al player
                    dirFollow = player.position - transform.position;
                    animatorcontroller.SetBool("IsMoving", true);
                }
                else
                {
                    animatorcontroller.SetBool("IsMoving", false);
                }

                dirFollow.Normalize();
                Vector3 targetPosition = transform.position + (Vector3)dirFollow * speed * Time.deltaTime;
                myRigidBody.MovePosition(targetPosition);
                LerpRotation();
            }
        }
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
                }
            }
        }
        private void LerpRotation()
        {
            rotZ = Mathf.Atan2(dirFollow.y, dirFollow.x) * Mathf.Rad2Deg;//saco el angulo para que apunte al player
            Quaternion desiredRotation= Quaternion.Euler(0, 0, rotZ-90);
            if (dirFollow != Vector2.zero)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
            }
        }

    }
}