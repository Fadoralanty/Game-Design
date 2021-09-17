using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Plataformer2d
{
    public class BossBehaviour : Enemy
    {
        [SerializeField] private List<string> attacksAnimations = new List<string>();
        protected override void Awake()
        {
            base.Awake();
            InvokeRepeating("SelectAttack", 0f, 3f);
        }
        public override void die()
        {
            base.die();
            animatorcontroller.SetTrigger("IsDead");
            GameManager.instance.StartVictorySequence();
        }
        private void SelectAttack()
        {
            int randomNum = Random.Range(0, attacksAnimations.Count);
            Attack(attacksAnimations[randomNum]);
        }
        private void Attack(string attackName)
        {
            animatorcontroller.SetTrigger(attackName);
        }
    }
}