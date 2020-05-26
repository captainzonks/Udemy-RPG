using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Breakable : MonoBehaviour
    {
        private Animator _anim;

        // cache
        private static readonly int Break = Animator.StringToHash("break");

        void Start()
        {
            _anim = GetComponent<Animator>();
        }

        void Update()
        {

        }

        public void Smash()
        {
            _anim.SetBool(Break, true);
            Destroy(gameObject, 0.3f);
        }
    }
}
