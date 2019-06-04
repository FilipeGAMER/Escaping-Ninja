using UnityEngine;
using System.Collections;

public class LevelExit : MonoBehaviour {

    private bool playerInZone;

    public string levelToLoad;

    // Use this for initialization
    void Start() {
        playerInZone = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow) && playerInZone) {
            Application.LoadLevel(levelToLoad);
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.name == "Player") {
            playerInZone = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other) {
        if (other.name == "Player") {
            playerInZone = true;
        }
    }
}
