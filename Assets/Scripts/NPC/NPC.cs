using System.Collections;
using UnityEngine;

namespace NPC
{
    public enum NpcState
    {
        Idle,
        Walk,
        Attack,
        Stagger
    }

    public class NPC : MonoBehaviour
    {
        [SerializeField] public NpcState currentState;
        [SerializeField] protected int health;
        [SerializeField] protected string npcName;
        [SerializeField] protected int baseAttack;
        [SerializeField] protected float moveSpeed;

        public void Knock(Rigidbody2D myRigidbody, float knockTime)
        {
            StartCoroutine(KnockCo(myRigidbody, knockTime));
        }

        private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = NpcState.Idle;
            myRigidbody.velocity = Vector2.zero;
        }

    }
}
