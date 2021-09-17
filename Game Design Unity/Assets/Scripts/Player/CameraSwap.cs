using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plataformer2d
{
    public class CameraSwap : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera cinemachineVirtualCamera=null;
        [SerializeField]
        private CinemachineVirtualCamera cinemachineVirtualCameraStopFollow=null;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                cinemachineVirtualCameraStopFollow.transform.position = transform.position;
                cinemachineVirtualCamera.VirtualCameraGameObject.SetActive(false);
                cinemachineVirtualCameraStopFollow.VirtualCameraGameObject.SetActive(true);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                cinemachineVirtualCamera.VirtualCameraGameObject.SetActive(true);
                cinemachineVirtualCameraStopFollow.VirtualCameraGameObject.SetActive(false);
            }
        }
    }
}
