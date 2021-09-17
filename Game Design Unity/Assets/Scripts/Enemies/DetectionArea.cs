using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plataformer2d 
{
    public class DetectionArea : MonoBehaviour
    {
        [SerializeField]
        private GameObject Activar=null;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // 1 manera de hacerlo
            if (collision.gameObject.CompareTag("Player"))
            {
                //colisiono contra un jugador
                //si colisiono/detecto a un jugador, entoces activo al gameobject al que esta asignado este script
                Activar.SetActive(true);
            }
        }
    }
}