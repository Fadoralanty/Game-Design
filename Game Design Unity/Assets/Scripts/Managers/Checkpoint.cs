using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plataformer2d
{
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField]
        private Transform checkPoint = null;
        [SerializeField] private bool firstCheckpoint;
        private void Start()
        {
            if (firstCheckpoint)
            {
                GameManager.instance.ChangeCheckpoint(checkPoint);
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                GameManager.instance.ChangeCheckpoint(checkPoint);
                gameObject.SetActive(false);
            }
        }
    }
}