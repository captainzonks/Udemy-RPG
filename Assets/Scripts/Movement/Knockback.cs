using NPC;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Movement
{
    public class Knockback : MonoBehaviour
    {
        public float thrust;
        public float knockTime;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Breakable") && other.GetComponent<Core.Breakable>() != null
            && gameObject.CompareTag("Player"))
            {
                other.GetComponent<Core.Breakable>().Smash();
            }

            if (other.CompareTag("NPC") || other.CompareTag("Player"))
            {
                var hit = other.GetComponent<Rigidbody2D>();

                if (hit != null)
                {
                    Vector2 difference = hit.transform.position - transform.position;
                    difference = difference.normalized * thrust;

                    hit.AddForce(difference, ForceMode2D.Impulse);

                    if (other.CompareTag("NPC"))
                    {
                        hit.GetComponent<NPC.NPC>().currentState = NpcState.Stagger;
                        other.GetComponent<NPC.NPC>().Knock(hit, knockTime);
                    }

                    if (other.CompareTag("Player"))
                    {
                        hit.GetComponent<PlayerController>().currentState = PlayerState.Stagger;
                        other.GetComponent<PlayerController>().Knock(knockTime);
                    }
                }
            }
        }
    }
}
