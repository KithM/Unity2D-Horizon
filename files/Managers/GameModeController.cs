using UnityEngine;

public class GameModeController : MonoBehaviour {

	[Header("NPCs")]
	NPC[] NPCs;

	[Header("Panels")]
	public GameObject teamDeathmatchPanel;
	public GameObject freeForAllPanel;
	public GameObject settingsPanel;

	// Use this for initialization
	void Start () {
		ToggleMenu (teamDeathmatchPanel);
		ToggleMenu (freeForAllPanel);
		ToggleMenu (settingsPanel);
	}

	public void ToggleMenu(GameObject panel){
		if (panel.activeInHierarchy == false) {
			panel.SetActive (true);
		} else {
			panel.SetActive (false);
		}
	}

	public void DeleteAllNPCs () {
		NPCs = FindObjectsOfType<NPC> ();

		foreach(NPC n in NPCs){
			n.SetHealth (0);
		}
	}
	public void RandomTeams(int numPerTeam){
		DeleteAllNPCs();

		for (int i = 0; i < numPerTeam; i++) {
			SpawnManager.SpawnRandomAlly ( SpawnManager.GetRandomAllyPosition() );			
		}
		for (int i = 0; i < numPerTeam; i++) {
			SpawnManager.SpawnRandomEnemy ( SpawnManager.GetRandomEnemyPosition() );			
		}
	}
	public void HeavyTeams(int numPerTeam){
		DeleteAllNPCs();

		for (int i = 0; i < numPerTeam; i++) {
			SpawnManager.SpawnRandomHeavyAlly ( SpawnManager.GetRandomAllyPosition() );			
		}
		for (int i = 0; i < numPerTeam; i++) {
			SpawnManager.SpawnRandomHeavyEnemy ( SpawnManager.GetRandomEnemyPosition() );			
		}
	}
	public void LightTeams(int numPerTeam){
		DeleteAllNPCs();

		for (int i = 0; i < numPerTeam; i++) {
			SpawnManager.SpawnRandomLightAlly ( SpawnManager.GetRandomAllyPosition() );			
		}
		for (int i = 0; i < numPerTeam; i++) {
			SpawnManager.SpawnRandomLightEnemy ( SpawnManager.GetRandomEnemyPosition() );			
		}
	}
	public void CompetitiveTeams(int numPerTeam){
		DeleteAllNPCs();

		for (int i = 0; i < numPerTeam; i++) {
			SpawnManager.SpawnRandomAlly ( SpawnManager.GetRandomPosition() );			
		}
		for (int i = 0; i < numPerTeam; i++) {
			SpawnManager.SpawnRandomNeutral ( SpawnManager.GetRandomPosition() );			
		}
		for (int i = 0; i < numPerTeam; i++) {
			SpawnManager.SpawnRandomEnemy ( SpawnManager.GetRandomPosition() );			
		}
	}

	public void RandomHeavyLightTeams(int num){
		DeleteAllNPCs();

		int rand = Random.Range (0,2);

		switch (rand) {
		case 0:
			for (int i = 0; i < num; i++) {
				SpawnManager.SpawnRandomHeavyAlly ( SpawnManager.GetRandomPosition() );			
			}
			for (int i = 0; i < num * 3; i++) {
				SpawnManager.SpawnRandomLightEnemy ( SpawnManager.GetRandomPosition() );			
			}
			break;
		case 1:
			for (int i = 0; i < num * 3; i++) {
				SpawnManager.SpawnRandomLightAlly ( SpawnManager.GetRandomPosition() );			
			}
			for (int i = 0; i < num; i++) {
				SpawnManager.SpawnRandomHeavyEnemy ( SpawnManager.GetRandomPosition() );			
			}
			break;
		default:
			break;
		}
	}
}
