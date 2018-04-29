using UnityEngine;

public class MovementController : MonoBehaviour {

	Camera cam;
	Rigidbody2D rb;
	public float movementSpeed;

	// Use this for initialization
	void Awake () {
		cam = Camera.main;
		rb = cam.GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		UpdateCameraMovement ();
	}

	void UpdateCameraMovement() {
		if(NPCManager.IsPlayerAlive() == true){
			// Follow Player Mode
			var pos = NPCManager.GetPlayerPosition ();
			cam.transform.position = new Vector3(pos.x, pos.y, -10f);
			return;
		}
		// Spectator Mode
		rb.AddForce (transform.right * Input.GetAxis ("Horizontal") * movementSpeed);
		rb.AddForce (transform.up * Input.GetAxis ("Vertical") * movementSpeed);
	}
}
