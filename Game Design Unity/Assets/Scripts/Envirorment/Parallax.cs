using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//El parallax loopeable infinito esta en la parte 1 de la clase 8
namespace Plataformer2d
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField, Range(0f,1f)]
        private float parallaxEffectMultiplierX=0;

        [SerializeField, Range(0f,1f)]
        private float parallaxEffectMultiplierY=0;

        private Transform cameraTransform;
        private Vector3 lastCameraPosition;
        private float width;
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            cameraTransform = Camera.main.transform;
            lastCameraPosition = cameraTransform.position;
            spriteRenderer = GetComponent<SpriteRenderer>();
            width = spriteRenderer.bounds.size.x;

        }
        //segun la clase hay que usar fixedupdate() pero anda mejor en el update() comun

        private void Update()
        {
            Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
            transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplierX, deltaMovement.y * parallaxEffectMultiplierY, 0f);
            lastCameraPosition = cameraTransform.position;
            //repetir capas en X
            float distanceWithCamera = cameraTransform.position.x - transform.position.x;
            if (Mathf.Abs(distanceWithCamera) >= width)
            {
                float movement = 0;
                if (distanceWithCamera > 0)
                {
                    movement = width * 2f;//la imagen salta 2 veces su ancho a la derecha
                }
                else if (distanceWithCamera < 0)
                {
                    movement = width * -2f;//la imagen salta 2 veces su ancho a la izquierda
                }
                transform.position += new Vector3(movement, 0f, 0f);
            }
        }
    }
}