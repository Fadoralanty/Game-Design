using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Plataformer2d
{
    public class BetterJump : MonoBehaviour
    {
        private Rigidbody2D myRigidBody;
        public float fallMultiplier = 2.5f;
        public float lowJumpMultiplier = 2f;
        private void Awake()
        {
            myRigidBody = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            if (myRigidBody.velocity.y < 0)
            {
                myRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (myRigidBody.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                myRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
    }
}