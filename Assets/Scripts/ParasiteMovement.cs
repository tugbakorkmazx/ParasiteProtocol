using UnityEngine;

public class ParasiteMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float sneakSpeed = 2f;
    public FixedJoystick joystick;

    [HideInInspector] public bool isSneaking = false;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private SpriteRenderer sr;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)) isSneaking = true;

        if (joystick != null && joystick.Direction.magnitude > 0.1f)
            moveInput = joystick.Direction;
        else
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput.Normalize();
        }

        if (moveInput.x > 0.1f) sr.flipX = false;
        else if (moveInput.x < -0.1f) sr.flipX = true;

        if (anim != null)
            anim.SetFloat("Speed", moveInput.magnitude);
    }

    void FixedUpdate()
    {
        float speed = isSneaking ? sneakSpeed : moveSpeed;
        rb.velocity = moveInput * speed;
    }

    public void SetSneak(bool value)
    {
        isSneaking = value;
    }
}