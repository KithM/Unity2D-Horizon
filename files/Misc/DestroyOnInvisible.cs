using UnityEngine;

public class DestroyOnInvisible : MonoBehaviour {

	void OnBecameInvisible(){
		Destroy(gameObject);
	}
}
