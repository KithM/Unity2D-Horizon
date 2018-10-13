using UnityEngine;

public class HideOnInvisible : MonoBehaviour {

	Renderer s;

	// Use this for initialization
	void Start () {
		s = gameObject.GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(s.isVisible){
			s.enabled = true;
			return;
		}
		s.enabled = false;
	}
}
