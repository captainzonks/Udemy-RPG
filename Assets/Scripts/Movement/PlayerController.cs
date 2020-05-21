using UnityEngine;

namespace Movement
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 6f;

        // player movement
        private Rigidbody2D _playerRigidBody;
        private Vector3 _change;
        private Animator _playerAnimator;

        public static PlayerController Instance;

        public string areaTransitionName;
        private Vector3 _bottomLeftLimit;
        private Vector3 _topRightLimit;

        public bool canMove = true;

        // cache
        private static readonly int MoveX = Animator.StringToHash("moveX");
        private static readonly int MoveY = Animator.StringToHash("moveY");
        private static readonly int Moving = Animator.StringToHash("moving");

        private void Start()
        {
            _playerRigidBody = GetComponent<Rigidbody2D>();
            _playerAnimator = GetComponent<Animator>();

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

        private void Update()
        {
            _change = Vector3.zero;
            _change.x = Input.GetAxisRaw("Horizontal");
            _change.y = Input.GetAxisRaw("Vertical");
            UpdateAnimationAndMove();

            //_playerAnimator.SetFloat("moveX", _playerRigidBody.velocity.x);
            //_playerAnimator.SetFloat("moveY", _playerRigidBody.velocity.y);

            //if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 ||
            //    Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
            //{
            //    if (canMove)
            //    {
            //        _playerAnimator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            //        _playerAnimator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            //    }
            //}

            // keep player inside the bounds
            var position = transform.position;
            position = new Vector3(Mathf.Clamp(position.x, _bottomLeftLimit.x, _topRightLimit.x),
                Mathf.Clamp(position.y, _bottomLeftLimit.y, _topRightLimit.y), position.z);
            transform.position = position;
        }

        private void UpdateAnimationAndMove()
        {
            if (_change != Vector3.zero && canMove)
            {
                //_playerRigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
                
                MoveCharacter();
                _playerAnimator.SetFloat(MoveX, _change.x);
                _playerAnimator.SetFloat(MoveY, _change.y);
                _playerAnimator.SetBool(Moving, true);
            }
            else
            {
                _playerAnimator.SetBool(Moving, false);
                //_playerRigidBody.velocity = Vector2.zero;
            }
        }

        private void MoveCharacter()
        {
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