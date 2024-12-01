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
    [SerializeField] private Sprite glitchedSprite;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite spriteWithGun;
    private SpriteRenderer spriteRenderer;
    private Animator _animator;
    private int _movingBoolHash;
    private bool _inGlitchAnimation;

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
    private bool isPlayingFootsteps;

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

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _movingBoolHash = Animator.StringToHash("IsMoving");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<float>();
        if (moveInput != 0 && isGrounded && !isPlayingFootsteps)
        {
            StartCoroutine(PlayFootsteps());
        }
    }
    private IEnumerator PlayFootsteps()
    {
        isPlayingFootsteps = true;
        while (moveInput != 0 && isGrounded)
        {
            int i = Random.Range(1, 4);
            string footstep1 = "footstep" + i;
            AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.footstep1);
            yield return new WaitForSeconds(0.7f); // Ajustez l'intervalle selon vos besoins
        }
        isPlayingFootsteps = false;
    }

    public void OnJump(InputAction.CallbackContext context)
    {

        if (GameManager.Instance.unlockedJump)
        {
            if (isGrounded)
            {
                AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.jumpSound);
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
            GameManager.Instance.returnedOnce = true;
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
        _animator.enabled = (GameManager.Instance.unlockedJump || !GameManager.Instance.hasPressedStart) && !_inGlitchAnimation && !GameManager.Instance.hasGun;
        if (!_animator.enabled  && !_inGlitchAnimation)
        {
            spriteRenderer.sprite = glitchedSprite;
        }
        
        if (GameManager.Instance.hasGun)
        {
            spriteRenderer.sprite = spriteWithGun;
        }
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
        
        if(!GameManager.Instance.returnedOnce && GameManager.Instance.leftOnce) canMove = false;
        
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
        
        if(rb.velocity.x > 0.5 || rb.velocity.x < -0.5) _animator.SetBool(_movingBoolHash, true);
        else _animator.SetBool(_movingBoolHash, false);

        // Vérifier si le joueur descend
        if (rb.velocity.y < 0)
        {
            // Activer les collisions avec les plateformes
            myCollider.enabled = true;
        }
    }

    public void GlitchAnim()
    {
        StartGlitchAnimation(spriteRenderer, glitchedSprite, normalSprite, 1f, 0.2f);
    }

    public void StartAnim()
    {
        StartGlitchAnimation(spriteRenderer, normalSprite, glitchedSprite, 1f, 0.2f);
    }

    void WallJump()
    {
        canMove = false;
        rb.velocity = new Vector2(-wallDirection * wallJumpForce, jumpForce);
        Flip();
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.wallJumpSound);
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
        _inGlitchAnimation = true;

        while (elapsedTime < glitchDuration)
        {
            spriteRenderer.sprite = useSpriteA ? spriteA : spriteB;
            useSpriteA = !useSpriteA;
            elapsedTime += glitchInterval;
            yield return new WaitForSeconds(glitchInterval);
        }
        _inGlitchAnimation = false;
        // À la fin de l'animation, définissez le sprite final
        spriteRenderer.sprite = spriteB;
    }
}