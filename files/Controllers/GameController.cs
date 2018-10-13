using UnityEngine;

public class GameController : MonoBehaviour {

	// Instance
	public static GameController current;

	public Canvas canvas { get; protected set; }
	public float defaultTimeScale;

	void Awake(){
		// Set up our references
		current = this;
		canvas = GameObject.Find ("WORLDCANVAS").GetComponent<Canvas>();

		InvokeRepeating ("ClearLag", 1f, 1f);
		InvokeRepeating ("UpdateShipRanks", 1f, 1f);
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
			if (IsGamePaused ()) {
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
		NPCManager.current.UpdateShipRanks ();
	}

	public bool IsGamePaused(){
		if(Time.timeScale <= 0f){
			return true;
		}
		return false;
	}

	public void PauseGame(bool doPause = true){
		// We are not changing our defaultTimeScale because it would interfere with the user's choice
		// and (since time isn't advancing anyway) the timeScale will not be reset with deltaTime
		if (doPause) {
			Time.timeScale = 0f;
			ObjectController.current.pausePanel.SetActive(true);
			return;
		}
		Time.timeScale = defaultTimeScale;
		ObjectController.current.pausePanel.SetActive(false);
	}

	public void ExitGame () {
		Application.Quit ();
	}
}
