using UnityEngine;
using System.Collections;

public class EnemyPatrol : MonoBehaviour {

    public float moveSpeed;
    public bool moveRight;
    private Rigidbody2D enemy;

    public Transform wallCheck;   // Transform = point in space
    public float wallCheckRadius; 
    public LayerMask whatIsWall;  
    private bool hittingWall;

    private bool notAtEdge;
    public Transform edgeCheck;

    private bool facingRight = true;

    void Awake () {
        enemy = GetComponent<Rigidbody2D>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        notAtEdge = Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, whatIsWall);

        if (hittingWall || !notAtEdge) {
            moveRight = !moveRight;
        }

        if (moveRight) {
            enemy.velocity = new Vector2(moveSpeed, enemy.velocity.y);
        } else {
            enemy.velocity = new Vector2(-moveSpeed, enemy.velocity.y);
        }

        if (enemy.velocity.x > 0 && !facingRight) {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (enemy.velocity.x < 0 && facingRight) {
            // ... flip the player.
            Flip();
        }
    }

    private void Flip() {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;
        
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
