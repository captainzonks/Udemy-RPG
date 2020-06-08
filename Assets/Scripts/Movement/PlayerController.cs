using System;
using System.Collections;
using System.Linq;
using Core;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Movement
{
    public enum PlayerState
    {
        Walk,
        Attack,
        Interact
    }

    public class PlayerController : MonoBehaviour
    {
        public PlayerState currentState;
        public float moveSpeed;

        // player movement
        private Rigidbody2D _playerRigidBody;
        private Vector3 _change;
        private Animator _playerAnimator;

        public static PlayerController Instance;
        public string areaTransitionName;

        private Vector3 _bottomLeftLimit;
        private Vector3 _topRightLimit;

        //public bool canMove = true;

        // cached properties
        private static readonly int MoveX = Animator.StringToHash("moveX");
        private static readonly int MoveY = Animator.StringToHash("moveY");
        private static readonly int Moving = Animator.StringToHash("moving");
        private static readonly int Attacking = Animator.StringToHash("attacking");

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                if (Instance != this)
                {
                    Destroy(gameObject);
                }
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _playerRigidBody = GetComponent<Rigidbody2D>();
            _playerAnimator = GetComponent<Animator>();
            currentState = PlayerState.Walk;

            // start the player facing downwards
            _playerAnimator.SetFloat(MoveX, 0);
            _playerAnimator.SetFloat(MoveY, -1);
        }

        private void Update()
        {
            _change = Vector3.zero;
            _change.x = Input.GetAxisRaw("Horizontal");
            _change.y = Input.GetAxisRaw("Vertical");
            if (GameManager.Instance.consoleOpen) return;

            if (Input.GetButtonDown("attack") && currentState != PlayerState.Attack)
            {
                StartCoroutine(AttackCo());
            }
            else switch (currentState)
            {
                case PlayerState.Walk:
                    UpdateAnimationAndMove();
                    break;
                case PlayerState.Interact:
                    _playerAnimator.SetBool(Moving, false);
                    break;
                case PlayerState.Attack:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // keep player inside the bounds
            var position = transform.position;
            position = new Vector3(Mathf.Clamp(position.x, _bottomLeftLimit.x, _topRightLimit.x),
                Mathf.Clamp(position.y, _bottomLeftLimit.y, _topRightLimit.y), position.z);
            transform.position = position;
        }

        private IEnumerator AttackCo()
        {
            _playerAnimator.SetBool(Attacking, true);
            currentState = PlayerState.Attack;
            yield return null;
            _playerAnimator.SetBool(Attacking, false);
            yield return new WaitForSeconds(0.3f);
            currentState = PlayerState.Walk;
        }

        private void UpdateAnimationAndMove()
        {
            if (_change != Vector3.zero && currentState == PlayerState.Walk)
            {
                MoveCharacter();
                _playerAnimator.SetFloat(MoveX, _change.x);
                _playerAnimator.SetFloat(MoveY, _change.y);
            }
            else
            {
                _playerAnimator.SetBool(Moving, false);
            }
        }

        private void MoveCharacter()
        {
            _playerAnimator.SetBool(Moving, true);
            _change.Normalize();
            _playerRigidBody.MovePosition(
                transform.position + _change * (moveSpeed * Time.deltaTime)
            );
        }

        public void SetBounds(Vector3 botLeft, Vector3 topRight)
        {
            _bottomLeftLimit = botLeft + new Vector3(0.5f, 1f, 0f);
            _topRightLimit = topRight + new Vector3(-0.5f, -1f, 0f);
        }
    }
}