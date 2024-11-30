using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallJumpForce = 10f;
    [SerializeField] private float wallSlideSpeed = 2f;
    [SerializeField] private float wallJumpDuration = 0.2f; // Durée pendant laquelle le mouvement horizontal est désactivé

    private Rigidbody2D rb;
    public float moveInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    public bool isFacingRight = true;
    public bool canMove = true;
    private int wallDirection;
    private Collider2D myCollider;

    void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.unlockedJump)
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            if (GameManager.Instance.unlockedWallJump)
            {
                if (isTouchingWall && !isGrounded)
                {
                    WallJump();
                }
            }
            
        }
        
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //Debug.Log("OnClick");
            CollectiblePlaform.MouseClicked();
            PlatPlaceholder.MouseClicked();
        }
    }

    public void ReturnMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            print("swapping");
            SceneManager.LoadScene("Baptiste");
            GameManager.Instance.ResetGame();
        }
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            print("pew! pew!");
        }
    }

    void Update()
    {
        if (canMove)
        {
            if (!isTouchingWall || (isTouchingWall && moveInput != wallDirection))
            {
                rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
            }

            if (isFacingRight && moveInput < 0)
            {
                Flip();
            }
            else if (!isFacingRight && moveInput > 0)
            {
                Flip();
            }
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, 0.1f, groundLayer);

        if (isTouchingWall && !isGrounded && rb.velocity.y < 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            wallDirection = isFacingRight ? 1 : -1;
        }
        else
        {
            isWallSliding = false;
        }

        // Vérifier si le joueur descend
        if (rb.velocity.y < 0)
        {
            // Activer les collisions avec les plateformes
            myCollider.enabled = true;
        }

    }

    void WallJump()
    {
        canMove = false;
        rb.velocity = new Vector2(-wallDirection * wallJumpForce, jumpForce);
        Flip();
        StartCoroutine(EnableMovementAfterDelay());
    }

    IEnumerator EnableMovementAfterDelay()
    {
        yield return new WaitForSeconds(wallJumpDuration);
        canMove = true;
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}