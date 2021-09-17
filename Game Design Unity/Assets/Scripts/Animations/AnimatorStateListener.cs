using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
namespace Plataformer2d
{
    public class AnimatorStateListener : MonoBehaviour
    {
        private AnimatorStateWatcher watcher;
        public Action<AnimatorStateInfo> OnStateEntered;
        public Action<AnimatorStateInfo> OnStateExited;
        public void SetWatcher(AnimatorStateWatcher watcher)
        {
            this.watcher = watcher;
            watcher.OnStateEntered += OnStateEntered;
            watcher.OnStateExited += OnStateExited;
        }

    }
}