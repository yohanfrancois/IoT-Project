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
    [SerializeField] private Transform wallCheck2;
    [SerializeField] private Transform wallCheck3WhatIsThisCode;
    [SerializeField] private float wallJumpForce = 10f;
    [SerializeField] private float wallSlideSpeed = 2f;
    [SerializeField] private SpriteRenderer eyesRenderer;

    [SerializeField]
    private float wallJumpDuration = 0.2f; // Durée pendant laquelle le mouvement horizontal est désactivé

    [SerializeField] private GameObject platform; //plateforme qui bouche le trou au début du jeu

    private Rigidbody2D rb;
    public float moveInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isHalfTouchingWall;
    private bool kindaTouchingWallAtThisPointIDontCareAboutVariableNames;
    private bool isWallSliding;
    public bool isFacingRight = true;
    public bool canMove = true;
    private int wallDirection;
    private Collider2D myCollider;

    void Awake()
    {
        if (Instance)
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
                AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.jumpSound);
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
        eyesRenderer.enabled = GameManager.Instance.hasEyes;
        if (platform != null)
        {
            if (!GameManager.Instance.unlockedPlatform)
            {
                platform.SetActive(false);
            }
            else
            {
                platform.SetActive(true);
            }
        }

        if (canMove)
        {
            if ((!isTouchingWall && !isHalfTouchingWall && !kindaTouchingWallAtThisPointIDontCareAboutVariableNames) ||
                ((isTouchingWall || isHalfTouchingWall || kindaTouchingWallAtThisPointIDontCareAboutVariableNames) && moveInput != wallDirection))
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
        isHalfTouchingWall = Physics2D.OverlapCircle(wallCheck2.position, 0.1f, groundLayer);
        kindaTouchingWallAtThisPointIDontCareAboutVariableNames = Physics2D.OverlapCircle(wallCheck3WhatIsThisCode.position, 0.1f, groundLayer);

        if ((isTouchingWall || isHalfTouchingWall || kindaTouchingWallAtThisPointIDontCareAboutVariableNames) && !isGrounded && rb.velocity.y < 0)
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
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.jumpSound);
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

    public void StartGlitchAnimation(SpriteRenderer spriteRenderer, Sprite spriteA, Sprite spriteB, float glitchDuration = 1f, float glitchInterval = 0.1f)
    {
        StartCoroutine(GlitchAnimation(spriteRenderer, spriteA, spriteB, glitchDuration, glitchInterval));
    }

    private IEnumerator GlitchAnimation(SpriteRenderer spriteRenderer, Sprite spriteA, Sprite spriteB, float glitchDuration, float glitchInterval)
    {
        float elapsedTime = 0f;
        bool useSpriteA = true;

        while (elapsedTime < glitchDuration)
        {
            spriteRenderer.sprite = useSpriteA ? spriteA : spriteB;
            useSpriteA = !useSpriteA;
            elapsedTime += glitchInterval;
            yield return new WaitForSeconds(glitchInterval);
        }

        // À la fin de l'animation, définissez le sprite final
        spriteRenderer.sprite = spriteB;
    }
}