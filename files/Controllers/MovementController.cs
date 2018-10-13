using UnityEngine;

public class MovementController : MonoBehaviour {

	Rigidbody2D rb;
	public float movementSpeed;

	// Use this for initialization
	void Start () {
		rb = Camera.main.GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		UpdateCameraMovement ();
	}

	void UpdateCameraMovement() {
		if(NPCManager.current.IsPlayerAlive()){
			// Follow Player Mode
			var pos = NPCManager.current.GetPlayerPosition ();
			Camera.main.transform.position = new Vector3(pos.x, pos.y, -10f);
			return;
		}
		// Spectator Mode
		rb.AddForce (transform.right * Input.GetAxis ("Horizontal") * movementSpeed);
		rb.AddForce (transform.up * Input.GetAxis ("Vertical") * movementSpeed);
	}
}
