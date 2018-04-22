using UnityEngine;

public class Zoom : MonoBehaviour {

	public float scrollSpeed;
	public float min;
	public float max;
	float newSize;

	void Start(){
		newSize = Camera.main.orthographicSize;
	}

	void Update(){
		
		if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
			newSize += scrollSpeed;
			newSize = Mathf.Clamp (newSize,min,max);
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
			newSize -= scrollSpeed;
			newSize = Mathf.Clamp (newSize,min,max);
		}

		if (Camera.main.orthographicSize != newSize) {
			Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, newSize, 5f * Time.deltaTime);
			Camera.main.orthographicSize = Mathf.Clamp (Camera.main.orthographicSize,min,max);
		}
		if (Camera.main.orthographicSize != newSize) {
			Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, newSize, 5f * Time.deltaTime);
			Camera.main.orthographicSize = Mathf.Clamp (Camera.main.orthographicSize,min,max);
		}
	}
}
