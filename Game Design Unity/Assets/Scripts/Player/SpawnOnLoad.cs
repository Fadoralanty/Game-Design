using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Plataformer2d
{
    public class SpawnOnLoad : MonoBehaviour
    {
        [SerializeField]
        private Transform spawnPosition=null;
        [SerializeField]
        private CinemachineVirtualCamera vcam=null;
        private void Awake()
        {
            GameObject.Find("player").transform.position = spawnPosition.position;
            vcam.Follow = GameObject.Find("player").transform;
        }
    }
}