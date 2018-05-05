using UnityEngine;

public class GameController : MonoBehaviour {

	public static Camera mainCamera { get; protected set; }
	public static ObjectController oc { get; protected set; }
	public static Canvas canvas { get; protected set; }
	public static float defaultTimeScale;

	void Awake(){
		// Set up our references
		mainCamera = Camera.main;
		canvas = GameObject.Find ("WORLDCANVAS").GetComponent<Canvas>();
		oc = FindObjectOfType<ObjectController> ();

		InvokeRepeating ("ClearLag", 1f, 1f);
		InvokeRepeating ("UpdateShipRanks", 1f, 1f);
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
			if (IsGamePaused () == true) {
				PauseGame (false);
				return;
			}
			PauseGame ();
		}
	}

	void ClearLag(){
		if (Time.deltaTime > 0.5f) {
			Time.timeScale = defaultTimeScale / 2f;
			return;
		}
		Time.timeScale = defaultTimeScale;
	}

	void UpdateShipRanks(){
		NPCManager.UpdateShipRanks ();
	}

	public static bool IsGamePaused(){
		if(Time.timeScale <= 0f){
			return true;
		}
		return false;
	}

	public static void PauseGame(bool doPause = true){
		// We are not changing our defaultTimeScale because it would interfere with the user's choice
		// and (since time isn't advancing anyway) the timeScale will not be reset with deltaTime
		if (doPause == true) {
			Time.timeScale = 0f;
			oc.pausePanel.SetActive(true);
			return;
		}
		Time.timeScale = defaultTimeScale;
		oc.pausePanel.SetActive(false);
	}

	public static void ExitGame () {
		Application.Quit ();
	}
}
