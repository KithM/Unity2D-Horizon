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
		if(NPCManager.IsGameFinished() == true){
			SetVictoryText ();
			return;
		} else {
			SetEmptyText ();
		}

		if(NPCManager.IsPlayerAlive() == false && NPCManager.IsGameFinished() == false){
			if (string.IsNullOrEmpty (spectatingText.text)) {
				spectatingText.text = "Free Camera";
			}
			return;
		}
		spectatingText.text = "";
	}

	void SetVictoryText(){
		if (string.IsNullOrEmpty(titleText.text)) {
			titleText.text = "VICTORY";
		}

		if (NPCManager.GetWinningTeam () == Ship.Faction.Ally) {
			titleText.color = Color.green;
		} else if (NPCManager.GetWinningTeam () == Ship.Faction.Neutral){
			titleText.color = Color.blue;
		} else if (NPCManager.GetWinningTeam () == Ship.Faction.Enemy){
			titleText.color = Color.red;
		}
	}
	void SetEmptyText(){
		titleText.text = "";
	}
}
