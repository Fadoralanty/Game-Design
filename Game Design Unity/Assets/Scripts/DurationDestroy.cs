using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Plataformer2d
{
    public class DurationDestroy : MonoBehaviour
    {
        private void Awake()
        {
            Destroy(gameObject, 3f);
        }
    }
}