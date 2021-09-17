using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plataformer2d
{
    public class ProyectileShooter : MonoBehaviour //este script es para el comportamiento del gameobject que va a disparar 
    {
        public GameObject proyectileprefab; //asignamos un gameobject a la variable proyectileprefab
        [SerializeField] private float bulletImpulse = 0f;
        public void Shoot()//metodo que se llama cuando se dispara una bala
        {
            GameObject bullet = Instantiate(proyectileprefab);//instanciamos en una variable de metodo el proyectileprefab
            Rigidbody2D bulletRigidBody = bullet.GetComponent<Rigidbody2D>();
            bullet.transform.position = transform.position;//le assignamos la posicion del game object de este componente
            bullet.transform.rotation = transform.rotation;
            Vector2 upvect = new Vector2(0, 0.5f);
            Vector2 direction =((Vector2)(transform.right) + upvect).normalized;
            bulletRigidBody.AddForce(direction * bulletImpulse, ForceMode2D.Impulse);
        }
    }
}
