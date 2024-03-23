using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region ATTRIBUTES
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform PlayerSprite;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private float jumpTime = 0.3f;
    [SerializeField] private float crouchHeight = 0.5f;

    private bool isGrounded = false;
    private bool isJumping = false;
    private float jumpTimer;
    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);

        #region JUMPING

        if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (isJumping && Input.GetKey(KeyCode.UpArrow))
        {
            if (jumpTimer < jumpTime)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimer += Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isJumping = false;
            jumpTimer = 0f;
        }

        #endregion

        #region CROUCHING

        if (isGrounded && Input.GetKey(KeyCode.DownArrow))
        {
            Crouch(crouchHeight);

            if (isJumping)
            {
                Crouch(1f);
            }
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            Crouch(1f);
        }

        #endregion
    }

    private void Crouch(float height)
    {
        PlayerSprite.localScale = new Vector3(
            PlayerSprite.localScale.x, height, PlayerSprite.localScale.z
        );
    }
}
