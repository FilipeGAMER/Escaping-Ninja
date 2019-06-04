using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

    public LevelManager levelManager;

	// Use this for initialization
	void Start () {
        // Finds any object in the project that have the LevelManager script
        levelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Detects when the player enters a trigger zone
    void OnTriggerEnter2D (Collider2D other) {

        if (other.name == "Player" && !levelManager.playerIsDead) {
            levelManager.RespawnPlayer();
            levelManager.playerIsDead = true;
        }
    }
}
