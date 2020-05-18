using UnityEngine;

namespace Movement
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] Rigidbody2D playerRigidbody2D;
        [SerializeField] float playerMoveSpeed;

        [SerializeField] Animator playerAnimator;

        public static PlayerController instance;

        [SerializeField] string areaTransitionName;
        Vector3 _bottomLeftLimit;
        Vector3 _topRightLimit;

        [SerializeField] bool canMove = true;

        void Start()
        {
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
            if (canMove)
            {
                playerRigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * playerMoveSpeed;

            }
            else
            {
                playerRigidbody2D.velocity = Vector2.zero;
            }

            playerAnimator.SetFloat("moveX", playerRigidbody2D.velocity.x);
            playerAnimator.SetFloat("moveY", playerRigidbody2D.velocity.y);

            if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 ||
                Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
            {
                if (canMove)
                {
                    playerAnimator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                    playerAnimator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
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
            return canMove;
        }

        public void ModifyMovement(bool movement)
        {
            canMove = movement;
        }
    }
}
