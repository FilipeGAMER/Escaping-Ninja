using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    public LevelManager levelManager;

    // Use this for initialization
    void Start() {
        // Finds any object in the project that have the LevelManager script
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update () {
	
	}

    // Detects when the player enters a trigger zone
    void OnTriggerEnter2D(Collider2D other) {

        if (other.name == "Player") {
            // is setting that the checkpoit (when the player activates it, is the gameobject which the script is attached to
            levelManager.currentCheckpoint = gameObject;
            Debug.Log("Activated Checkpoint " + transform.name);
        }
    }
}
