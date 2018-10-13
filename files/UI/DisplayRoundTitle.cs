using UnityEngine.UI;
using UnityEngine;

public class DisplayRoundTitle : MonoBehaviour {

	[Header("Text Elements")]
	public Text titleText;
	public Text spectatingText;

	// Use this for initialization
	void Start () {
		SetEmptyText ();
		InvokeRepeating("UpdateTitle", 1f, 1f);
	}

	void UpdateTitle () {
		if(NPCManager.current.IsGameFinished()){
			SetVictoryText ();
			return;
		}
		SetEmptyText ();

		if(!NPCManager.current.IsPlayerAlive () && !NPCManager.current.IsGameFinished ()){
			if (string.IsNullOrEmpty (spectatingText.text)) {
				spectatingText.text = "Free Camera";
			}
			return;
		}
		spectatingText.text = "";
	}

	void SetVictoryText(){
		if (string.IsNullOrEmpty(titleText.text) || string.IsNullOrEmpty(spectatingText.text)) {
			titleText.text = "VICTORY";
			spectatingText.text = "";
		}

		if (NPCManager.current.GetWinningTeam () == Ship.Faction.Ally) {
			titleText.color = Color.green;
		} else if (NPCManager.current.GetWinningTeam () == Ship.Faction.Neutral){
			titleText.color = Color.blue;
		} else if (NPCManager.current.GetWinningTeam () == Ship.Faction.Enemy){
			titleText.color = Color.red;
		}
	}
	void SetEmptyText(){
		titleText.text = "";
	}
}
