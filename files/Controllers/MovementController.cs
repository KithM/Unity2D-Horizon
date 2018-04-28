using UnityEngine;

public class MovementController : MonoBehaviour {

	Rigidbody2D rb;
	public float movementSpeed;

	// Use this for initialization
	void Awake () {
		rb = Camera.main.GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		UpdateCameraMovement ();
	}

	void UpdateCameraMovement() {
		if(NPCManager.IsPlayerAlive() == true){
			// Follow Player Mode
			Camera.main.transform.position = new Vector3(NPCManager.GetPlayerPosition ().x, NPCManager.GetPlayerPosition ().y, -10f);
			return;
		}
		// Spectator Mode
		rb.AddForce (transform.right * Input.GetAxis ("Horizontal") * movementSpeed);
		rb.AddForce (transform.up * Input.GetAxis ("Vertical") * movementSpeed);
	}
}
