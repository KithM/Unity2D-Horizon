using UnityEngine;

public class GameController : MonoBehaviour {

	public static SpriteController sc { get; protected set; }
	public static Canvas canvas { get; protected set; }

	void Awake(){
		// Set up our references
		canvas = GameObject.Find ("WORLDCANVAS").GetComponent<Canvas>();
		sc = FindObjectOfType<SpriteController> ();

		InvokeRepeating ("ClearLag", 1f, 1f);
		InvokeRepeating ("UpdateNPCRanks", 1f, 1f);
	}

	void ClearLag(){
		if (Time.deltaTime > 0.1f) {
			Time.timeScale = 0.5f;
			return;
		} else if (Time.deltaTime > 0.5f){
			Time.timeScale = 0.25f;
			return;
		}
		Time.timeScale = 1f;
	}

	void UpdateNPCRanks(){
		NPCManager.UpdateShipRanks ();
	}

	public void ExitGame () {
		Application.Quit ();
	}
}
