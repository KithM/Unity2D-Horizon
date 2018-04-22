using UnityEngine;

public class MovementController : MonoBehaviour {

	// Movement
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
		// Handle screen panning
		rb.AddForce (transform.right * Input.GetAxis ("Horizontal") * movementSpeed);
		rb.AddForce (transform.up * Input.GetAxis ("Vertical") * movementSpeed);
	}
}
