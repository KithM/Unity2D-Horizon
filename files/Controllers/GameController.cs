using UnityEngine;

public class GameController : MonoBehaviour {

	public static SpriteController sc { get; protected set; }
	public static Canvas canvas { get; protected set; }

	void Awake(){
		// Set up our references
		canvas = GameObject.Find ("WORLDCANVAS").GetComponent<Canvas>();
		sc = FindObjectOfType<SpriteController> ();
	}

	// Use this for initialization
	void Start () {
		// TODO: DEBUG ONLY
		// InvokeRepeating ("SpawnRandomShip", 1f, 10f);
	}
}
