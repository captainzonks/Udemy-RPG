using System;
using UnityEngine;

namespace NPC
{
    public class Loggy : NPC
    {
        private Rigidbody2D myRigidbody2D;
        private Animator anim;

        [SerializeField] private Transform target;
        [SerializeField] private float chaseRadius;
        [SerializeField] private float attackRadius;
        [SerializeField] private Transform homePosition;

        // cache
        private static readonly int WakeUp = Animator.StringToHash("wakeUp");
        private static readonly int MoveX = Animator.StringToHash("moveX");
        private static readonly int MoveY = Animator.StringToHash("moveY");

        private void Start()
        {
            currentState = NpcState.Idle;
            target = GameObject.FindWithTag("Player").transform;
            myRigidbody2D = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            CheckDistance();
        }

        private void CheckDistance()
        {
            if (Vector3.Distance(target.position, transform.position) <= chaseRadius
            && Vector3.Distance(target.position, transform.position) > attackRadius)
            {
                if (currentState == NpcState.Idle || currentState == NpcState.Walk && currentState != NpcState.Stagger)
                {
                    var temp = Vector3.MoveTowards(
                        transform.position, 
                        target.position, 
                        moveSpeed * Time.deltaTime);

                    ChangeAnim(temp - transform.position);

                    myRigidbody2D.MovePosition(temp);
                    ChangeState(NpcState.Walk);
                    anim.SetBool(WakeUp, true);
                }
            }
            else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
            {
                currentState = NpcState.Idle;
                anim.SetBool(WakeUp, false);
            }
        }

        private void SetAnimFloat(Vector2 setVector)
        {
            anim.SetFloat(MoveX, setVector.x);
            anim.SetFloat(MoveY, setVector.y);
        }

        private void ChangeAnim(Vector2 direction)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
                {
                    SetAnimFloat(Vector2.right);
                }
                else if (direction.x < 0)
                {
                    SetAnimFloat(Vector2.left);
                }
            }
            else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
            {
                if (direction.y > 0)
                {
                    SetAnimFloat(Vector2.up);
                }
                else if (direction.y < 0)
                {
                    SetAnimFloat(Vector2.down);
                }
            }
        }

        private void ChangeState(NpcState newState)
        {
            currentState = newState;
        }
    }
}
