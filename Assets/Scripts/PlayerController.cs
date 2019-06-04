using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;         // The horizontal speed of the player
    public float jumpHeight;        // The vertical speed of the player

    private float moveVelocity;

    private float playerSpeed;         // The horizontal speed of the player
    [SerializeField]
    private float maxSpeed = 10f;   // The fastest the player can travel in the x axis.
    [SerializeField]
    private float JumpForce = 400f; // Amount of force added when the player jumps.

    private float maxClimb = 40f;   // 

    public Transform groundCheck;   // Transform = point in space
    public float groundCheckRadius; // How long will be the radius that checks the ground
    public LayerMask whatIsGround;  // The layer that the grounds will be
    private bool grounded;          // True or False if the player is on the ground

    private Animator anim;          // Enable the animation control
    
    private Rigidbody2D myrigidbody2D;
    
    private bool facingRight = true;  // For determining which way the player is currently facing.

    public bool onLadder;           // To check if the player is on ladder
    public float climbSpeed;        // The player speed of climbing
    private float climbVelocity;    // The player velocity of climbing
    private float gravityStore;     // The player gravity before entering the ladder

    public bool goingDown;           // To check if the player is pressing the down arrow

    public float i;

    public bool Grounded {
        get {
            return grounded;
        }

        set {
            grounded = value;
        }
    }

    //Use this before the Start
    void Awake() {

    }

    // Use this for initialization
    void Start() {

        anim = GetComponent<Animator>();

        myrigidbody2D = GetComponent<Rigidbody2D>();

        gravityStore = myrigidbody2D.gravityScale;

        Physics2D.IgnoreLayerCollision(8, 12, true);

        i = 0f;
    }

    // Use this for physics - Occurs a certain amaunt of time every second
    void FixedUpdate() {
        Grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        anim.SetBool("Grounded", Grounded);

        // Set the vertical animation
        anim.SetFloat("vSpeed", myrigidbody2D.velocity.y);

        Move();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Move() {
        
        playerSpeed = Input.GetAxis("Horizontal");

        if (Grounded) {

            anim.SetFloat("Speed", Mathf.Abs(myrigidbody2D.velocity.x));

            moveVelocity = playerSpeed * maxSpeed;

            // If D or left arrow is pressed we set the moveSpeed as the new X velocity 
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                //myrigidbody2D.velocity = new Vector2(playerSpeed * maxSpeed, myrigidbody2D.velocity.y);
               // moveVelocity = moveSpeed;
            }
            // If A or right arrow is pressed we set the moveSpeed as the new X velocity 
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
                //myrigidbody2D.velocity = new Vector2(playerSpeed * maxSpeed, myrigidbody2D.velocity.y);
                moveVelocity = -moveSpeed;
            }

            myrigidbody2D.velocity = new Vector2(moveVelocity, myrigidbody2D.velocity.y);

            if (myrigidbody2D.velocity.x > 0 && !facingRight) {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (myrigidbody2D.velocity.x < 0 && facingRight) {
                // ... flip the player.
                Flip();
            }
        }

        // Checks if the player cab jump
        if (Grounded && Input.GetKeyDown(KeyCode.Space) && anim.GetBool("Grounded")) {

            anim.SetFloat("vSpeed", 0f);

            // Add a vertical force to the player.
            Grounded = false;
            anim.SetBool("Grounded", false);
            myrigidbody2D.AddForce(new Vector2(0f, JumpForce));
            GetComponent<AudioSource>().Play();

        }

        // Set if the player is on top of the ladder AND is pressing the down arrow
        if (Input.GetKey(KeyCode.DownArrow)) {
            goingDown = true;
        }

        if (!Input.GetKey(KeyCode.DownArrow)) {
            goingDown = false;
        }

        // Check if the player is on the ladder, reset the gravity to zero
        // Get the climb velocity from climbSpeed times the value of the Vertical axis
        // Then puts the new velocity into the Rigidbody2D
        if (onLadder) {
            myrigidbody2D.gravityScale = 0f;
            anim.SetBool("OnLadder", true);

            climbVelocity = climbSpeed * Input.GetAxisRaw("Vertical");

            if (Input.GetAxisRaw("Vertical") != 0) {
                anim.SetBool("Climbing", true);
                if (i > maxClimb) {
                    i = 0;
                }
                anim.SetFloat("ClimbSpeed", i);
                i++;
            }
            if (Input.GetAxisRaw("Vertical") == 0) {
                anim.SetBool("Climbing", false);
            }
            //anim.SetFloat("ClimbSpeed", Mathf.Abs(Input.GetAxis("Vertical")));

            myrigidbody2D.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), climbVelocity);
        }

        if (!onLadder) {
            myrigidbody2D.gravityScale = gravityStore;

            anim.SetBool("OnLadder", false);
            i = 0;
            //anim.SetFloat("ClimbSpeed", 0f);
        }
    }

    //public void Jump() {
    //    myrigidbody2D.velocity = new Vector2(myrigidbody2D.velocity.x, jumpHeight);
    //}

    private void Flip() {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


}