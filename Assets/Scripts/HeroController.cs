using UnityEngine;

#pragma warning disable 0649 //don't mind it, i just hate useless warnings

//Just im case cuz i am lazy to set them up again for object lmao
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class HeroController : MonoBehaviour
{
    //basic components
    BoxCollider2D box;
    Rigidbody2D rb;
    Animator animator;
    AudioSource audioSource;

    float movement;

    [Header("Floor/ceiling checks")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField][Range(0,1)] float sensitivity = 0.5f;
    [Space(5)]

    [Header("Movement values")]
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float maxFallSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float gravity;
    [Space(5)]

    bool watchRight = true;
    bool isGrounded = false;
    bool isCeiled = false;
    bool controlled = true;

    void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        //custom gravity. We are cool
        rb.gravityScale = 0;
    }

    void Update()
    {
        //Check if it is standing firmly, for single ground check in a frame.
        isGrounded = Grounded();
        isCeiled = Ceiled();

        if (controlled)
        {
            InputControls();
        }

        //stops jump if hit the ceiling
        if (isCeiled)
        {
            rb.velocity = new Vector2(rb.velocity.x, -0.1f);
        }

        //turns image
        if (movement > 0 && !watchRight || movement < 0 && watchRight)
        {
            //animator.
            //Flip() should be called on trigger in animation but for now it will turn just like that
            Flip();
        }

        //hand made gravity
        if (controlled)
        {
            rb.velocity += new Vector2(0, -gravity);
        }
    }

    private void InputControls()
    {
        //Basic input. without windup. And inertia. Can be added tho
        movement = Input.GetAxisRaw("Horizontal") * movementSpeed;
        rb.velocity = new Vector2(movement, Mathf.Clamp(rb.velocity.y, -maxFallSpeed, jumpSpeed));

        //jump if it stands on ground
        if (Input.GetButtonDown("Jump"))
        {
            //no long/short jump. Yet
            if (isGrounded)
            {
                //It is raw. I can make it frame-perfect. Still works for now tho
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }
        }
    }

    //Define if it is standing according on box collider size
    bool Grounded() => Physics2D.OverlapCircle(new Vector3(box.bounds.center.x, box.bounds.center.y - box.bounds.size.y * 0.5f), sensitivity, groundLayer);

    //define if it is touching the ceiling
    bool Ceiled() => Physics2D.OverlapCircle(new Vector3(box.bounds.center.x, box.bounds.center.y + box.bounds.size.y * 0.5f), sensitivity, groundLayer);

    //roughly flips object. Calls on animation
    void Flip()
    {
        watchRight = !watchRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
