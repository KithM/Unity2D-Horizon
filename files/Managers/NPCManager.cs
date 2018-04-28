using UnityEngine;

public static class NPCManager {

	static GameObject[] allies;
	static GameObject[] neutrals;
	static GameObject[] enemies;

	public static int GetAlly () {
		allies = GameObject.FindGameObjectsWithTag ("Ally");
		return allies.Length;
	}
	public static int GetNeutral () {
		neutrals = GameObject.FindGameObjectsWithTag ("Neutral");
		return neutrals.Length;
	}
	public static int GetEnemy () {
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		return enemies.Length;
	}

	public static int GetTotal(){
		return GetAlly () + GetNeutral () + GetEnemy ();
	}

	public static bool IsPlayerAlive () {
		var p = Object.FindObjectOfType<Player> ();
		if(p != null){ 
			return true; 
		}
		return false;
	}

	public static Vector2 GetPlayerPosition () {
		var p = Object.FindObjectOfType<Player> ();
		if(p != null){ 
			return p.transform.position; 
		}
		return Vector2.zero;
	}

	public static bool IsGameFinished(){
		if(GetAlly() == 0 && GetNeutral() == 0 && GetTotal() >= 1){
			return true;
		} else if (GetAlly() == 0 && GetEnemy() == 0 && GetTotal() >= 1){
			return true;
		} else if (GetNeutral() == 0 && GetEnemy() == 0 && GetTotal() >= 1){
			return true;
		}
		return false;
	}

	public static Ship.Faction GetWinningTeam(){
		if(GetAlly() == 0 && GetNeutral() == 0 && GetTotal() >= 1){
			return Ship.Faction.Enemy;
		} else if (GetAlly() == 0 && GetEnemy() == 0 && GetTotal() >= 1){
			return Ship.Faction.Neutral;
		} else if (GetNeutral() == 0 && GetEnemy() == 0 && GetTotal() >= 1){
			return Ship.Faction.Ally;
		}
		Debug.LogError ("NPCManager: GetWinningTeam did not return a valid winning team.");
		return Ship.Faction.Neutral;
	}
}
