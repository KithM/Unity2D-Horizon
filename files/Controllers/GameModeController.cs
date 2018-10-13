using UnityEngine;
using UnityEngine.UI;

public class GameModeController : MonoBehaviour {

	// Instance
	public static GameModeController current;

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
	void Awake(){
		current = this;
	}
	void Start () {
		ToggleMenu (teamDeathmatchPanel);
		ToggleMenu (freeForAllPanel);
		ToggleMenu (scenarioPanel);
		ToggleMenu (customMatchPanel);
		ToggleMenu (settingsPanel);
		ToggleMenu (configurationPanel);
	}

	public void ToggleMenu(GameObject panel){
		if (!panel.activeSelf) {
			panel.SetActive (true);
		} else {
			panel.SetActive (false);
		}
	}

	public void DeleteAllNPCs () {
		var ships = NPCManager.current.GetAllShips ();
		int i;
		for (i = 0; i < ships.Length; i++) {
			ships[i].SetHealth (0);
		}
	}
	public void RandomTeams(int numPerTeam){
		DeleteAllNPCs ();

		int r = Random.Range (0,2);

		switch (r) {
		case 0:
			SpawnManager.current.SpawnRandomAlly ( SpawnManager.current.GetRandomAllyPosition(), true );
			for (int i = 0; i < numPerTeam - 1; i++) {
				SpawnManager.current.SpawnRandomAlly ( SpawnManager.current.GetRandomAllyPosition() );			
			}
			for (int i = 0; i < numPerTeam; i++) {
				SpawnManager.current.SpawnRandomEnemy ( SpawnManager.current.GetRandomEnemyPosition() );			
			}
			break;
		case 1:
			SpawnManager.current.SpawnRandomEnemy ( SpawnManager.current.GetRandomEnemyPosition(), true );
			for (int i = 0; i < numPerTeam; i++) {
				SpawnManager.current.SpawnRandomAlly ( SpawnManager.current.GetRandomAllyPosition() );			
			}
			for (int i = 0; i < numPerTeam - 1; i++) {
				SpawnManager.current.SpawnRandomEnemy ( SpawnManager.current.GetRandomEnemyPosition() );			
			}
			break;
		}
	}
	public void HeavyTeams(int numPerTeam){
		DeleteAllNPCs();

		int r = Random.Range (0,2);

		switch (r) {
		case 0:
			SpawnManager.current.SpawnRandomHeavyAlly ( SpawnManager.current.GetRandomAllyPosition(), true );
			for (int i = 0; i < numPerTeam - 1; i++) {
				SpawnManager.current.SpawnRandomHeavyAlly ( SpawnManager.current.GetRandomAllyPosition() );			
			}
			for (int i = 0; i < numPerTeam; i++) {
				SpawnManager.current.SpawnRandomHeavyEnemy ( SpawnManager.current.GetRandomEnemyPosition() );			
			}
			break;
		case 1:
			SpawnManager.current.SpawnRandomHeavyEnemy ( SpawnManager.current.GetRandomEnemyPosition(), true );
			for (int i = 0; i < numPerTeam; i++) {
				SpawnManager.current.SpawnRandomHeavyAlly ( SpawnManager.current.GetRandomAllyPosition() );			
			}
			for (int i = 0; i < numPerTeam - 1; i++) {
				SpawnManager.current.SpawnRandomHeavyEnemy ( SpawnManager.current.GetRandomEnemyPosition() );			
			}
			break;
		}
	}
	public void LightTeams(int numPerTeam){
		DeleteAllNPCs();

		int r = Random.Range (0,2);

		switch (r) {
		case 0:
			SpawnManager.current.SpawnRandomLightAlly ( SpawnManager.current.GetRandomAllyPosition(), true );
			for (int i = 0; i < numPerTeam - 1; i++) {
				SpawnManager.current.SpawnRandomLightAlly ( SpawnManager.current.GetRandomAllyPosition() );			
			}
			for (int i = 0; i < numPerTeam; i++) {
				SpawnManager.current.SpawnRandomLightEnemy ( SpawnManager.current.GetRandomEnemyPosition() );			
			}
			break;
		case 1:
			SpawnManager.current.SpawnRandomLightEnemy ( SpawnManager.current.GetRandomEnemyPosition(), true );
			for (int i = 0; i < numPerTeam; i++) {
				SpawnManager.current.SpawnRandomLightAlly ( SpawnManager.current.GetRandomAllyPosition() );			
			}
			for (int i = 0; i < numPerTeam - 1; i++) {
				SpawnManager.current.SpawnRandomLightEnemy ( SpawnManager.current.GetRandomEnemyPosition() );			
			}
			break;
		}
	}
	public void CompetitiveTeams(int numPerTeam){
		DeleteAllNPCs();

		int r = Random.Range (0,3);

		switch (r) {
		case 0:
			SpawnManager.current.SpawnRandomAlly ( SpawnManager.current.GetRandomPosition(), true );			
			for (int i = 0; i < numPerTeam - 1; i++) {
				SpawnManager.current.SpawnRandomAlly ( SpawnManager.current.GetRandomPosition() );			
			}
			for (int i = 0; i < numPerTeam; i++) {
				SpawnManager.current.SpawnRandomNeutral ( SpawnManager.current.GetRandomPosition() );			
			}
			for (int i = 0; i < numPerTeam; i++) {
				SpawnManager.current.SpawnRandomEnemy ( SpawnManager.current.GetRandomPosition() );			
			}
			break;
		case 1:
			SpawnManager.current.SpawnRandomNeutral ( SpawnManager.current.GetRandomPosition(), true );			
			for (int i = 0; i < numPerTeam; i++) {
				SpawnManager.current.SpawnRandomAlly ( SpawnManager.current.GetRandomPosition() );			
			}
			for (int i = 0; i < numPerTeam - 1; i++) {
				SpawnManager.current.SpawnRandomNeutral ( SpawnManager.current.GetRandomPosition() );			
			}
			for (int i = 0; i < numPerTeam; i++) {
				SpawnManager.current.SpawnRandomEnemy ( SpawnManager.current.GetRandomPosition() );			
			}
			break;
		case 2:
			SpawnManager.current.SpawnRandomEnemy ( SpawnManager.current.GetRandomPosition(), true );			
			for (int i = 0; i < numPerTeam; i++) {
				SpawnManager.current.SpawnRandomAlly ( SpawnManager.current.GetRandomPosition() );			
			}
			for (int i = 0; i < numPerTeam; i++) {
				SpawnManager.current.SpawnRandomNeutral ( SpawnManager.current.GetRandomPosition() );			
			}
			for (int i = 0; i < numPerTeam - 1; i++) {
				SpawnManager.current.SpawnRandomEnemy ( SpawnManager.current.GetRandomPosition() );			
			}
			break;
		}
	}

	public void RandomHeavyLightTeams(int num){
		DeleteAllNPCs();

		int rand = Random.Range (0,4);

		switch (rand) {
		case 0:
			SpawnManager.current.SpawnRandomHeavyAlly ( SpawnManager.current.GetRandomPosition(), true );			
			for (int i = 0; i < num - 1; i++) {
				SpawnManager.current.SpawnRandomHeavyAlly ( SpawnManager.current.GetRandomPosition() );			
			}
			for (int i = 0; i < num * 3; i++) {
				SpawnManager.current.SpawnRandomLightEnemy ( SpawnManager.current.GetRandomPosition() );			
			}
			break;
		case 1:
			SpawnManager.current.SpawnRandomLightAlly ( SpawnManager.current.GetRandomPosition(), true );
			for (int i = 0; i < (num * 3) - 1; i++) {
				SpawnManager.current.SpawnRandomLightAlly ( SpawnManager.current.GetRandomPosition() );			
			}
			for (int i = 0; i < num; i++) {
				SpawnManager.current.SpawnRandomHeavyEnemy ( SpawnManager.current.GetRandomPosition() );			
			}
			break;
		case 2:
			SpawnManager.current.SpawnRandomLightEnemy ( SpawnManager.current.GetRandomPosition(), true );			
			for (int i = 0; i < num; i++) {
				SpawnManager.current.SpawnRandomHeavyAlly ( SpawnManager.current.GetRandomPosition() );			
			}
			for (int i = 0; i < (num * 3) - 1; i++) {
				SpawnManager.current.SpawnRandomLightEnemy ( SpawnManager.current.GetRandomPosition() );			
			}
			break;
		case 3:
			SpawnManager.current.SpawnRandomHeavyEnemy ( SpawnManager.current.GetRandomPosition(), true );			
			for (int i = 0; i < num - 1; i++) {
				SpawnManager.current.SpawnRandomLightAlly ( SpawnManager.current.GetRandomPosition() );			
			}
			for (int i = 0; i < num * 3; i++) {
				SpawnManager.current.SpawnRandomHeavyEnemy ( SpawnManager.current.GetRandomPosition() );			
			}
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

	public void IncreaseNPCLevels(int amount){
		if(NPCManager.current.IsGameFinished ()){
			return;
		}

		int i;
		var ships = NPCManager.current.GetAllShips ();
		for (i = 0; i < ships.Length; i++) {
			ships[i].IncreaseLevel (amount);
		}
	}
}
