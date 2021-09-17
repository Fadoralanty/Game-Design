using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace Plataformer2d
{
    public class Grenade : MonoBehaviour // ---*EJEMPLO OVERLAP CIRCLE ALL*---
    {
        [SerializeField]
        private float explosionTime=0;

        [SerializeField]
        private float explosionRadius=0;

        [SerializeField]
        private float explosionIntensity=0;

        [SerializeField]
        private LayerMask layerMask=default;

        private float currentExplosionTime=0;

        private void Update()
        {
            currentExplosionTime = Time.deltaTime;
            if(currentExplosionTime>= explosionTime)
            {
                Explosion();
            }
        }
        private void Explosion()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll((Vector2)transform.position, explosionRadius, layerMask);
            //Physics2D.OverlapBoxAll() OVERLAP CON CUADRADO
            foreach (var collider in colliders)
            {
               Rigidbody2D rigidBody = collider.gameObject.GetComponent<Rigidbody2D>();
                if (rigidBody != null)
                {
                    Vector3 direction = collider.transform.position - transform.position;
                    direction.Normalize();
                    float distance = direction.magnitude;
                    rigidBody.AddForce((direction * explosionIntensity) / distance, ForceMode2D.Impulse);//se aplica fuerza en direccion a cada objeto con el que toca el overlap y la intensidad de la fuerza depende de la distancia
                    Destroy(gameObject);
                }
            }
        }
    }
}