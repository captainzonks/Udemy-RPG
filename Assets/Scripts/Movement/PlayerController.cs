using UnityEngine;

namespace Movement
{
    public class PlayerController : MonoBehaviour
    {
        Rigidbody2D _playerRigidbody2D;
        float _playerMoveSpeed;
        Animator _playerAnimator;

        public static PlayerController instance;

        [SerializeField] string areaTransitionName;
        Vector3 _bottomLeftLimit;
        Vector3 _topRightLimit;

        bool _canMove = true;

        void Start()
        {
            _playerRigidbody2D = GetComponent<Rigidbody2D>();
            _playerMoveSpeed = 4f;
            _playerAnimator = GetComponent<Animator>();

            if (instance == null)
            {
                instance = this;
            }
            else
            {
                if (instance != this)
                {
                    Destroy(gameObject);
                }
            }

            DontDestroyOnLoad(gameObject);
        }

        void Update()
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

        public string GetAreaTransitionName()
        {
            return areaTransitionName;
        }

        public void SetAreaTransitionName(string set)
        {
            areaTransitionName = set;
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
