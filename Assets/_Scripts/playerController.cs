using UnityEngine;

public class playerController : MonoBehaviour
{
    private Transform cameraTransform;
    private Rigidbody rb;
    private Vector3 move;
    private Vector2 movement;

    private bool weaponShootToggle;

    private bool grounded;

    [Header("Movement Stats")]
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float groundDrag = 1f;
    [SerializeField] private float airMultiplier = 0.4f;
    [SerializeField] private float fallSpeed;

    [Header("Weapon")]
    [SerializeField] private WeaponBase dimePistol;
    [SerializeField] private WeaponBase laserGun;
    [SerializeField] private WeaponBase currentWeapon;

    private void Awake()
    {
        Inputs.InitMovement(this);
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
    {
        GroundedCheck();
        applyDrag();
        SpeedControl();
    }

    // CONTROLS
    public void Jump()
    {
        if (!grounded)
            return;

        //Reset vertical velocity for moving platforms and stuff
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }

    public void GetMove(Vector2 dir)
    {
        movement = dir;
    }

    // RUNS IN UPDATE

    private void GroundedCheck()
    {
        grounded = Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y);
    }

    private void applyDrag()
    {
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > playerSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * playerSpeed;
            rb.velocity = new Vector3(limitedVel.x, limitedVel.y, limitedVel.z);
        }
    }


    // RUN IN FIXED UPDATE
    public void MovePlayer()
    {
        move = new Vector3(movement.x, 0, movement.y);

        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0;

        // On ground
        if (grounded)
            rb.AddForce(move.normalized * playerSpeed, ForceMode.Force);
        // In air
        else
        {
            rb.AddForce(move.normalized * playerSpeed * airMultiplier, ForceMode.Force);
            
            if (rb.velocity.y < -0.05)
                rb.AddForce(-Vector3.up * fallSpeed, ForceMode.Force);
        }
            
    }
}
