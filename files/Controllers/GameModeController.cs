using UnityEngine;
using UnityEngine.UI;

public class GameModeController : MonoBehaviour {

	[Header("Panels")]
	public GameObject teamDeathmatchPanel;
	public GameObject freeForAllPanel;
	public GameObject scenarioPanel;
	public GameObject customMatchPanel;
	public GameObject settingsPanel;
	public GameObject configurationPanel;

	[Header("InputFields")]
	public InputField customLevelInput;
	public InputField customAmountInput;

	// Use this for initialization
	void Start () {
		ToggleMenu (teamDeathmatchPanel);
		ToggleMenu (freeForAllPanel);
		ToggleMenu (scenarioPanel);
		ToggleMenu (customMatchPanel);
		ToggleMenu (settingsPanel);
		ToggleMenu (configurationPanel);
	}

	public void ToggleMenu(GameObject panel){
		if (panel.activeSelf == false) {
			panel.SetActive (true);
		} else {
			panel.SetActive (false);
		}
	}

	public Character[] GetAllNPCs(){
		return NPCManager.GetAllShips ().ToArray ();
	}
	public void DeleteAllNPCs () {
		GetAllNPCs ();

		foreach(Character n in NPCManager.GetAllShips ()){
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

	public void CustomMatch(){
		// Get custom level and custom number per team from inputFields
		RandomScenarioTeams (int.Parse(customAmountInput.text), int.Parse(customLevelInput.text));
		ToggleMenu (customMatchPanel);
	}
	public void CustomMatchRandomIntInput(InputField input){
		input.text = Random.Range(1,101).ToString();
	}
	public void RandomScenarioTeams(int numPerTeam, int level){
		DeleteAllNPCs();

		RandomTeams (numPerTeam);
		IncreaseNPCLevels (level - 1);
	}

	void IncreaseNPCLevels(int amount){
		GetAllNPCs ();

		if(NPCManager.IsGameFinished ()){
			return;
		}

		foreach (Character n in NPCManager.GetAllShips()) {
			n.IncreaseLevel(amount);
		}
	}
}
