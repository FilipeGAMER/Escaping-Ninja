using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public GameObject currentCheckpoint;
    private PlayerController player;
    private CameraController camera;

    public GameObject deathParticle;
    public GameObject respawnParticle;

    public float respawnDelay;
    public bool playerIsDead;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerController>();
        camera = FindObjectOfType<CameraController>();
        playerIsDead = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RespawnPlayer() {
        StartCoroutine("RespawnPlayerCo");
    }

    public IEnumerator RespawnPlayerCo() {
        Instantiate(deathParticle, player.transform.position, player.transform.rotation);
        player.enabled = false;
        player.GetComponent<Renderer>().enabled = false;
        //player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        camera.isFollowing = false;

        Debug.Log("Player Respawn");
        yield return new WaitForSeconds(respawnDelay);

        player.transform.position = currentCheckpoint.transform.position;
        player.enabled = true;
        player.GetComponent<Renderer>().enabled = true;
        playerIsDead = false;
        camera.isFollowing = true;
        Instantiate(respawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
    }
}
