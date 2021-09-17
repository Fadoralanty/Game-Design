using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Plataformer2d
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField]
        public float damage = 0f;
        [SerializeField]
        public float speed = 0f;
        private void FixedUpdate()
        {
            
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject);
        }
    }
}