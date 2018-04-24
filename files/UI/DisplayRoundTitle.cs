using UnityEngine.UI;
using UnityEngine;

public class DisplayRoundTitle : MonoBehaviour {

	[Header("Text Elements")]
	public Text titleText;

	// Use this for initialization
	void Start () {
		SetEmptyText ();
	}
	
	// Update is called once per frame
	void Update () {
		if(NPCManager.IsGameFinished() == true){
			SetVictoryText ();
		} else {
			SetEmptyText ();
		}
	}

	void SetVictoryText(){
		titleText.text = "VICTORY";

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
