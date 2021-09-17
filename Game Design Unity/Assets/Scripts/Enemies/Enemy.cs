using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Plataformer2d
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        public float currentlife;

        [SerializeField]
        public float maxlife;

        [SerializeField]
        public float damage;

        [SerializeField]
        public float knockback = 0;

        [SerializeField]
        protected float speed = 0;

        [SerializeField]
        protected float timeToDestroy = 0f;

        protected Animator animatorcontroller;
        private bool isDead = false;
        public UnityEvent onDie = new UnityEvent();
        public Action<float> onLifeChange;
        protected virtual void Awake()
        {
            currentlife = maxlife;
            animatorcontroller = GetComponent<Animator>();
        }

        public void GetDamage(float damage)
        {
            if (currentlife > 0)
            {
                currentlife -= damage;
                //myRigidBody.AddForce(-transform.right * knockback, ForceMode2D.Impulse);//Effecto knockback con fisicas
            }
            onLifeChange?.Invoke(currentlife);
            if (currentlife <= 0)
            {
                die();
            }
            if (!isDead)
            {
                animatorcontroller.SetTrigger("GetHit");
            }
            //transform.Translate(speed*Time.deltaTime, 0, 0);//knockback
        }

        public void GetHealing(int healnum)
        {
            if (currentlife == maxlife)
            {
                return;
            }
            currentlife += healnum;
            if (currentlife > maxlife)
            {
                currentlife = maxlife;
            }
            onLifeChange?.Invoke(currentlife);
        }
        
        public virtual void die()
        {
            Destroy(gameObject,timeToDestroy);
            onDie.Invoke();
            isDead = true;
        }
        public float GetLifePercentage()
        {
            return (float)currentlife / maxlife;
        }
    }
}