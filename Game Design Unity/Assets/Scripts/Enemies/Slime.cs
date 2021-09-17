﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Plataformer2d
{
    public class Slime : Enemy
    {
        //Slime Behaviour
        [SerializeField]
        private Transform player = null;
        protected override void Awake()
        {
            base.Awake();
            if (player == null)
            {
                player = GameObject.Find("player").transform;
            }
        }
        private void Update()
        {
            if (player != null)
            {
                Vector3 dir = new Vector3(player.position.x - transform.position.x, 0, 0);
                //Vector3 dir = new Vector3(transform.position.x - player.position.x, 0, 0);//para que se aleje del jugador, invertis el orden de la resta y asi, ya se aleja el slime del player
                dir.Normalize();
                float distance = Vector3.Distance(player.position, transform.position);
                if (distance > 1 && distance < 12)
                {
                    transform.position += dir * speed * Time.deltaTime;
                    animatorcontroller.SetBool("IsMoving", true);
                    if (dir.x > 0)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                }
                else
                {
                    animatorcontroller.SetBool("IsMoving", false);
                }
            }
        }
        
    }
}