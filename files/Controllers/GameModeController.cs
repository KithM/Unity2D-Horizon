using UnityEngine;

public class GameModeController : MonoBehaviour {

	[Header("NPCs")]
	Character[] NPCs;

	[Header("Panels")]
	public GameObject teamDeathmatchPanel;
	public GameObject freeForAllPanel;
	public GameObject scenarioPanel;
	public GameObject settingsPanel;

	// Use this for initialization
	void Start () {
		ToggleMenu (teamDeathmatchPanel);
		ToggleMenu (freeForAllPanel);
		ToggleMenu (scenarioPanel);
		ToggleMenu (settingsPanel);
	}

	public void ToggleMenu(GameObject panel){
		if (panel.activeInHierarchy == false) {
			panel.SetActive (true);
		} else {
			panel.SetActive (false);
		}
	}

	public Character[] GetAllNPCs(){
		NPCs = FindObjectsOfType<Character> ();
		return NPCs;
	}
	public void DeleteAllNPCs () {
		GetAllNPCs ();

		foreach(Character n in NPCs){
			n.SetHealth (0);
		}
	}
	public void RandomTeams(int numPerTeam){
		DeleteAllNPCs();

		int r = Random.Range (0,2);

		switch (r) {
		case 0:
			SpawnManager.SpawnRandomAlly ( SpawnManager.GetRandomAllyPosition(), true );
			for (int i = 0; i < numPerTeam - 1; i++) {
				SpawnManager.SpawnRandomAlly ( SpawnManager.GetRandomAllyPosition() );			
			}
			for (int i = 0; i < numPerTeam; i++) {
				SpawnManager.SpawnRandomEnemy ( SpawnManager.GetRandomEnemyPosition() );			
			}
			break;
		case 1:
			SpawnManager.SpawnRandomEnemy ( SpawnManager.GetRandomEnemyPosition(), true );
			for (int i = 0; i < numPerTeam; i++) {
				SpawnManager.SpawnRandomAlly ( SpawnManager.GetRandomAllyPosition() );			
			}
			for (int i = 0; i < numPerTeam - 1; i++) {
				SpawnManager.SpawnRandomEnemy ( SpawnManager.GetRandomEnemyPosition() );			
			}
			break;
		default:
			break;
		}
	}
	public void HeavyTeams(int numPerTeam){
		DeleteAllNPCs();

		int r = Random.Range (0,2);

		switch (r) {
		case 0:
			SpawnManager.SpawnRandomHeavyAlly ( SpawnManager.GetRandomAllyPosition(), true );
			for (int i = 0; i < numPerTeam - 1; i++) {
				SpawnManager.SpawnRandomHeavyAlly ( SpawnManager.GetRandomAllyPosition() );			
			}
			for (int i = 0; i < numPerTeam; i++) {
				SpawnManager.SpawnRandomHeavyEnemy ( SpawnManager.GetRandomEnemyPosition() );			
			}
			break;
		case 1:
			SpawnManager.SpawnRandomHeavyEnemy ( SpawnManager.GetRandomEnemyPosition(), true );
			for (int i = 0; i < numPerTeam; i++) {
				SpawnManager.SpawnRandomHeavyAlly ( SpawnManager.GetRandomAllyPosition() );			
			}
			for (int i = 0; i < numPerTeam - 1; i++) {
				SpawnManager.SpawnRandomHeavyEnemy ( SpawnManager.GetRandomEnemyPosition() );			
			}
			break;
		default:
			break;
		}
	}
	public void LightTeams(int numPerTeam){
		DeleteAllNPCs();

		int r = Random.Range (0,2);

		switch (r) {
		case 0:
			SpawnManager.SpawnRandomLightAlly ( SpawnManager.GetRandomAllyPosition(), true );
			for (int i = 0; i < numPerTeam - 1; i++) {
				SpawnManager.SpawnRandomLightAlly ( SpawnManager.GetRandomAllyPosition() );			
			}
			for (int i = 0; i < numPerTeam; i++) {
				SpawnManager.SpawnRandomLightEnemy ( SpawnManager.GetRandomEnemyPosition() );			
			}
			break;
		case 1:
			SpawnManager.SpawnRandomLightEnemy ( SpawnManager.GetRandomEnemyPosition(), true );
			for (int i = 0; i < numPerTeam; i++) {
				SpawnManager.SpawnRandomLightAlly ( SpawnManager.GetRandomAllyPosition() );			
			}
			for (int i = 0; i < numPerTeam - 1; i++) {
				SpawnManager.SpawnRandomLightEnemy ( SpawnManager.GetRandomEnemyPosition() );			
			}
			break;
		default:
			break;
		}
	}
	public void CompetitiveTeams(int numPerTeam){
		DeleteAllNPCs();

		int r = Random.Range (0,3);

		switch (r) {
		case 0:
			SpawnManager.SpawnRandomAlly ( SpawnManager.GetRandomPosition(), true );			
			for (int i = 0; i < numPerTeam - 1; i++) {
				SpawnManager.SpawnRandomAlly ( SpawnManager.GetRandomPosition() );			
			}
			for (int i = 0; i < numPerTeam; i++) {
				SpawnManager.SpawnRandomNeutral ( SpawnManager.GetRandomPosition() );			
			}
			for (int i = 0; i < numPerTeam; i++) {
				SpawnManager.SpawnRandomEnemy ( SpawnManager.GetRandomPosition() );			
			}
			break;
		case 1:
			SpawnManager.SpawnRandomNeutral ( SpawnManager.GetRandomPosition(), true );			
			for (int i = 0; i < numPerTeam; i++) {
				SpawnManager.SpawnRandomAlly ( SpawnManager.GetRandomPosition() );			
			}
			for (int i = 0; i < numPerTeam - 1; i++) {
				SpawnManager.SpawnRandomNeutral ( SpawnManager.GetRandomPosition() );			
			}
			for (int i = 0; i < numPerTeam; i++) {
				SpawnManager.SpawnRandomEnemy ( SpawnManager.GetRandomPosition() );			
			}
			break;
		case 2:
			SpawnManager.SpawnRandomEnemy ( SpawnManager.GetRandomPosition(), true );			
			for (int i = 0; i < numPerTeam; i++) {
				SpawnManager.SpawnRandomAlly ( SpawnManager.GetRandomPosition() );			
			}
			for (int i = 0; i < numPerTeam; i++) {
				SpawnManager.SpawnRandomNeutral ( SpawnManager.GetRandomPosition() );			
			}
			for (int i = 0; i < numPerTeam - 1; i++) {
				SpawnManager.SpawnRandomEnemy ( SpawnManager.GetRandomPosition() );			
			}
			break;
		default:
			break;
		}
	}

	public void RandomHeavyLightTeams(int num){
		DeleteAllNPCs();

		int rand = Random.Range (0,4);

		switch (rand) {
		case 0:
			SpawnManager.SpawnRandomHeavyAlly ( SpawnManager.GetRandomPosition(), true );			
			for (int i = 0; i < num - 1; i++) {
				SpawnManager.SpawnRandomHeavyAlly ( SpawnManager.GetRandomPosition() );			
			}
			for (int i = 0; i < num * 3; i++) {
				SpawnManager.SpawnRandomLightEnemy ( SpawnManager.GetRandomPosition() );			
			}
			break;
		case 1:
			SpawnManager.SpawnRandomLightAlly ( SpawnManager.GetRandomPosition(), true );
			for (int i = 0; i < (num * 3) - 1; i++) {
				SpawnManager.SpawnRandomLightAlly ( SpawnManager.GetRandomPosition() );			
			}
			for (int i = 0; i < num; i++) {
				SpawnManager.SpawnRandomHeavyEnemy ( SpawnManager.GetRandomPosition() );			
			}
			break;
		case 2:
			SpawnManager.SpawnRandomLightEnemy ( SpawnManager.GetRandomPosition(), true );			
			for (int i = 0; i < num; i++) {
				SpawnManager.SpawnRandomHeavyAlly ( SpawnManager.GetRandomPosition() );			
			}
			for (int i = 0; i < (num * 3) - 1; i++) {
				SpawnManager.SpawnRandomLightEnemy ( SpawnManager.GetRandomPosition() );			
			}
			break;
		case 3:
			SpawnManager.SpawnRandomHeavyEnemy ( SpawnManager.GetRandomPosition(), true );			
			for (int i = 0; i < num - 1; i++) {
				SpawnManager.SpawnRandomLightAlly ( SpawnManager.GetRandomPosition() );			
			}
			for (int i = 0; i < num * 3; i++) {
				SpawnManager.SpawnRandomHeavyEnemy ( SpawnManager.GetRandomPosition() );			
			}
			break;
		default:
			break;
		}
	}

	public void RandomScenarioLevel5Teams(int numPerTeam){
		DeleteAllNPCs();

		RandomTeams (numPerTeam);
		GetAllNPCs ();

		foreach (Character n in NPCs) {
			n.IncreaseLevel(4);
		}
	}
	public void RandomScenarioLevel10Teams(int numPerTeam){
		DeleteAllNPCs();

		RandomTeams (numPerTeam);
		GetAllNPCs ();

		foreach (Character n in NPCs) {
			n.IncreaseLevel(9);
		}
	}
}
