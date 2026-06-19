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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        if (moveInput.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
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