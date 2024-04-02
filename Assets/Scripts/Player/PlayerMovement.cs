using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    #region ATTRIBUTES
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject PlayerSprite;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private float jumpTime = 0.3f;
    [SerializeField] private float slideTime = 0.5f;

    [SerializeField] private PolygonCollider2D[] colliders;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float timePerFrame = 0.2f;

    private bool isGrounded = false;
    private bool isJumping = false;
    private bool isSliding = false;
    private float jumpTimer;
    private float slideTimer;

    private int currentSpriteIndex = 0;
    private int previousSpriteIndex = 3;
    private int jumpIndex = 4;
    private int slideIndex = 5;

    #endregion

    void Start()
    {
        StartCoroutine(AlternateSprites());
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

        #region SLIDING

        if (isGrounded && Input.GetKeyDown(KeyCode.DownArrow))
        {
            isSliding = true;
        }

        if (isSliding && Input.GetKey(KeyCode.DownArrow))
        {
            if (slideTimer < slideTime)
            {
                slideTimer += Time.deltaTime;
            }
            else
            {
                isSliding = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isSliding = false;
            slideTimer = 0f;
        }

        #endregion
    }

    #region ANIMATIONS

    IEnumerator AlternateSprites()
    {
        while (true)
        {
            // Switch to the next sprite
            spriteRenderer.sprite = sprites[currentSpriteIndex];
            SetColliderForSprite();
            previousSpriteIndex = currentSpriteIndex;
            // Set state index
            if (!isJumping && !isSliding)
            {
                currentSpriteIndex = (currentSpriteIndex + 1) % 4;
                yield return new WaitForSeconds(timePerFrame);
            }
            if (isJumping)
            {
                currentSpriteIndex = jumpIndex;
            }
            if (isSliding)
            {
                currentSpriteIndex = slideIndex;
            }
            yield return null;
        }
    }

    void SetColliderForSprite()
    {
        colliders[previousSpriteIndex].enabled = false;
        colliders[currentSpriteIndex].enabled = true;

    }

    #endregion

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene("DeathScreen");
        }
    }
}
