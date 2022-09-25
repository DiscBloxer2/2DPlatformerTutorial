using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float JumpForce;
    public bool Grounded;
    public Transform GroundCheck;
    public float GroundRadius;
    public LayerMask GroundLayer;
    public bool RawAxis;
    public int ExtraJumps;
    public int CurrentExtraJumps;
    private float moveInput;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CurrentExtraJumps = ExtraJumps;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        if (RawAxis)
        {
            moveInput = Input.GetAxisRaw("Horizontal");
        }else
        {
            moveInput = Input.GetAxis("Horizontal");
        }
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        //Jump
        Grounded = Physics2D.OverlapCircle(GroundCheck.position, GroundRadius, GroundLayer);

        if (Grounded)
        {
            CurrentExtraJumps = ExtraJumps;
        }

        if (Input.GetButtonDown("Jump") && Grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }
        if (Input.GetButtonDown("Jump") && !Grounded && CurrentExtraJumps > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            CurrentExtraJumps--;
        }
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheck.position, GroundRadius);
    }
}
