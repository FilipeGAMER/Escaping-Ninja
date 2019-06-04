using UnityEngine;
using System.Collections;

public class LadderZone : MonoBehaviour {

    private PlayerController thePlayer;

    public Transform playerOnTopCheck;   // Transform = point in space
    public float playerOnTopRadius; // How long will be the radius that checks the if the player is climbing

    public LayerMask whatIsPlayer;  // The layer that the grounds will be
    private bool onTopLadder;       // True or False the player is on top of the ladder

    public BoxCollider2D metalGround;   // BoxCollider2D = Object to interact with

    // Use this for initialization
    void Start() {
        thePlayer = FindObjectOfType<PlayerController>();
    }

    // Use this for physics - Occurs a certain amaunt of time every second
    void FixedUpdate() {

        onTopLadder = Physics2D.OverlapCircle(playerOnTopCheck.position, playerOnTopRadius, whatIsPlayer);

    }

    //TODO arrumar a saida da escada em cima, verificar como fazer ele entender para mudar o onLadder

    void Update() {

        if (thePlayer.onLadder && onTopLadder) {

            if (!thePlayer.goingDown) {
                metalGround.isTrigger = false;
                thePlayer.onLadder = false;
                Debug.Log("3 - THIS HAPPENS???");
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.name == "Player" && onTopLadder == false) {
            thePlayer.onLadder = true;
            metalGround.isTrigger = true;
        }
    }

    public void OnTriggerStay2D(Collider2D collider) {
        if (collider.name == "Player" && onTopLadder == true && thePlayer.goingDown) {
            metalGround.isTrigger = true;
            thePlayer.onLadder = true;
            Debug.Log("2 - THIS HAPPENS???");
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.name == "Player") {
            thePlayer.onLadder = false;
            metalGround.isTrigger = false;
        }
    }
}
