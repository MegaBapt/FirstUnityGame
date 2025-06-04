using UnityEngine;
using UnityEngine.UI;

public class mouvement_joueur : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private bool isJumping;
    private bool isGrounded = true;
    [HideInInspector]
    public bool isClimbing;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D player_Collider;

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;
    private float verticalMovement;

    public static mouvement_joueur instance;

    // FLY MODE & DEBUG
    private bool flyMode = false;
    private float defaultGravityScale;
    [HideInInspector]
    public bool isInvincible = false;
    public Sprite newSprite; 
    public Text debugText; // ou TextMeshProUGUI si tu l’utilises  
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("fait gaffe il y a trop d'instances de mouvement_joueur");
            return;
        }

        instance = this;
    }

    void Start()
    {
        defaultGravityScale = rb.gravityScale;
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        // Toggle fly/debug mode
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleFlyMode();
        }

        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
        verticalMovement = Input.GetAxis("Vertical") * moveSpeed * Time.fixedDeltaTime;

        if (!flyMode && Input.GetButtonDown("Jump") && isGrounded && !isClimbing)
        {
            isJumping = true;
        }

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
        animator.SetBool("isClimbing", isClimbing);
    }

    void FixedUpdate()
    {
        float adjustedDeltaTime = Time.fixedDeltaTime / PlayerEffects.SlowMotionFactor;

        if (flyMode)
        {
            rb.velocity = new Vector2(horizontalMovement, verticalMovement);
        }
        else
        {
            MovePlayer(horizontalMovement, verticalMovement);
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);
        }
    }

    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        if (!isClimbing)
        {
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
            if (isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                isJumping = false;
            }
        }
        else
        {
            Vector3 targetVelocity = new Vector2(0, _verticalMovement);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        }
    }

    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    // === DEBUG MODE TOGGLE ===
    void ToggleFlyMode()
    {
        flyMode = !flyMode;

        if (flyMode)
        {
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
            player_Collider.enabled = false; // Noclip
            isInvincible = true;
            moveSpeed *= 2;
            animator.enabled = false;   
            spriteRenderer.sprite = newSprite;
        }
        else
        {
            rb.gravityScale = defaultGravityScale;
            player_Collider.enabled = true;
            isInvincible = false;
            moveSpeed /= 2;
            animator.enabled = true;
        }
        debugText.gameObject.SetActive(flyMode);
    }
}
