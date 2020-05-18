using UnityEngine;

namespace Movement
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D _playerRigidbody2D;
        private float _playerMoveSpeed;
        private Animator _playerAnimator;

        public static PlayerController Instance { get; set; }

        public string AreaTransitionName { get; set; }
        private Vector3 _bottomLeftLimit;
        private Vector3 _topRightLimit;

        private bool _canMove = true;

        private void Start()
        {
            _playerRigidbody2D = GetComponent<Rigidbody2D>();
            _playerMoveSpeed = 4f;
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
            if (_canMove)
            {
                _playerRigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * _playerMoveSpeed;
            }
            else
            {
                _playerRigidbody2D.velocity = Vector2.zero;
            }

            _playerAnimator.SetFloat("moveX", _playerRigidbody2D.velocity.x);
            _playerAnimator.SetFloat("moveY", _playerRigidbody2D.velocity.y);

            if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 ||
                Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
            {
                if (_canMove)
                {
                    _playerAnimator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                    _playerAnimator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
                }
            }

            // keep player inside the bounds
            var position = transform.position;
            position = new Vector3(Mathf.Clamp(position.x, _bottomLeftLimit.x, _topRightLimit.x),
                Mathf.Clamp(position.y, _bottomLeftLimit.y, _topRightLimit.y),
                position.z);
            transform.position = position;
        }

        public void SetBounds(Vector3 botLeft, Vector3 topRight)
        {
            _bottomLeftLimit = botLeft + new Vector3(0.5f, 1f, 0f);
            _topRightLimit = topRight + new Vector3(-0.5f, -1f, 0f);
        }

        public bool CanMove()
        {
            return _canMove;
        }

        public void ModifyMovement(bool movement)
        {
            _canMove = movement;
        }
    }
}
