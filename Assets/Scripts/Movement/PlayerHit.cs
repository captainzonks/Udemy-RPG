﻿using UnityEngine;
using Core;

namespace Movement
{
    public class PlayerHit : MonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Breakable") && other.GetComponent<Core.Breakable>() != null)
            {
                other.GetComponent<Core.Breakable>().Smash();
            }
        }
    }
}
