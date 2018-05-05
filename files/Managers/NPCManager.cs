using UnityEngine;
using System.Collections.Generic;

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

	public static List<Character> GetAllShips(){
		var npcs = new List<Character> ();

		foreach (GameObject a in allies) {
			if(a == null){
				continue;
			}
			var n = a.GetComponent<Character> ();
			npcs.Add (n);
		}
		foreach (GameObject a in neutrals) {
			if(a == null){
				continue;
			}
			var n = a.GetComponent<Character> ();
			npcs.Add (n);
		}
		foreach (GameObject a in enemies) {
			if(a == null){
				continue;
			}
			var n = a.GetComponent<Character> ();
			npcs.Add (n);
		}

		return npcs;
	}

	public static bool IsPlayerAlive () {
		if(allies == null || neutrals == null || enemies == null ){
			return false;
		}
		foreach (GameObject a in allies) {
			if(a == null){
				continue;
			}
			var n = a.GetComponent<Player> ();
			if(n == null){
				continue;
			}
			return true;
		}
		foreach (GameObject a in neutrals) {
			if(a == null){
				continue;
			}
			var n = a.GetComponent<Player> ();
			if(n == null){
				continue;
			}
			return true;
		}
		foreach (GameObject a in enemies) {
			if(a == null){
				continue;
			}
			var n = a.GetComponent<Player> ();
			if(n == null){
				continue;
			}
			return true;
		}

		return false;
	}

	public static Vector2 GetPlayerPosition () {
		foreach (GameObject a in allies) {
			if(a == null){
				continue;
			}
			var n = a.GetComponent<Player> ();
			if(n == null){
				continue;
			}
			return n.transform.position;
		}
		foreach (GameObject a in neutrals) {
			if(a == null){
				continue;
			}
			var n = a.GetComponent<Player> ();
			if(n == null){
				continue;
			}
			return n.transform.position;
		}
		foreach (GameObject a in enemies) {
			if(a == null){
				continue;
			}
			var n = a.GetComponent<Player> ();
			if(n == null){
				continue;
			}
			return n.transform.position;
		}

		return Vector2.zero;
	}

	public static float GetAllTotalHealth() {
		float totalHealth = 0f;

		foreach (GameObject a in allies) {
			if(a == null){
				continue;
			}
			var n = a.GetComponent<Character> ();
			totalHealth += n.Health;
		}
		foreach (GameObject a in neutrals) {
			if(a == null){
				continue;
			}
			var n = a.GetComponent<Character> ();
			totalHealth += n.Health;
		}
		foreach (GameObject a in enemies) {
			if(a == null){
				continue;
			}
			var n = a.GetComponent<Character> ();
			totalHealth += n.Health;
		}

		return totalHealth;
	}
	public static float GetFactionTotalHealth(Ship.Faction faction) {
		float health = 0f;
		switch (faction) {
		case Ship.Faction.Ally:
			foreach (GameObject a in allies) {
				if(a == null){
					continue;
				}
				var n = a.GetComponent<Character> ();
				health += n.Health;
			}
			break;
		case Ship.Faction.Neutral:
			foreach (GameObject a in neutrals) {
				if(a == null){
					continue;
				}
				var n = a.GetComponent<Character> ();
				health += n.Health;
			}
			break;
		case Ship.Faction.Enemy:
			foreach (GameObject a in enemies) {
				if(a == null){
					continue;
				}
				var n = a.GetComponent<Character> ();
				health += n.Health;
			}
			break;
		default:
			break;
		}

		return health;
	}
	public static float GetFactionTotalLevel(Ship.Faction faction) {
		float level = 0f;
		switch (faction) {
		case Ship.Faction.Ally:
			foreach (GameObject a in allies) {
				if(a == null){
					continue;
				}
				var n = a.GetComponent<Character> ();
				level += n.Level;
			}
			break;
		case Ship.Faction.Neutral:
			foreach (GameObject a in neutrals) {
				if(a == null){
					continue;
				}
				var n = a.GetComponent<Character> ();
				level += n.Level;
			}
			break;
		case Ship.Faction.Enemy:
			foreach (GameObject a in enemies) {
				if(a == null){
					continue;
				}
				var n = a.GetComponent<Character> ();
				level += n.Level;
			}
			break;
		default:
			break;
		}

		return level;
	}

	public static bool IsGameFinished(){
		if(GetAlly() == 0 && GetNeutral() == 0 && GetTotal() > 0){
			return true;
		} else if (GetAlly() == 0 && GetEnemy() == 0 && GetTotal() > 0){
			return true;
		} else if (GetNeutral() == 0 && GetEnemy() == 0 && GetTotal() > 0){
			return true;
		}
		return false;
	}

	public static Ship.Faction GetWinningTeam(){
		if(GetAlly() == 0 && GetNeutral() == 0 && GetTotal() > 0){
			return Ship.Faction.Enemy;
		} else if (GetAlly() == 0 && GetEnemy() == 0 && GetTotal() > 0){
			return Ship.Faction.Neutral;
		} else if (GetNeutral() == 0 && GetEnemy() == 0 && GetTotal() > 0){
			return Ship.Faction.Ally;
		}
		Debug.LogError ("NPCManager: GetWinningTeam did not return a valid winning team.");
		return Ship.Faction.Neutral;
	}

	public static void UpdateShipRanks(){
		if(GetTotal() < 1){
			return;
		}
		foreach (GameObject a in allies) {
			if(a == null){
				continue;
			}
			var n = a.GetComponent<Character> ();
			SetShipRank (n);
		}
		foreach (GameObject a in neutrals) {
			if(a == null){
				continue;
			}
			var n = a.GetComponent<Character> ();
			SetShipRank (n);
		}
		foreach (GameObject a in enemies) {
			if(a == null){
				continue;
			}
			var n = a.GetComponent<Character> ();
			SetShipRank (n);
		}
	}

	static void SetShipRank(Character n){
		var totalLevel = GetFactionTotalLevel (n.shipFaction);
		var totalHealth = GetFactionTotalHealth (n.shipFaction);
		if(n.MaxHealth >= totalHealth / 1.25f || n.Level >= totalLevel / 1.05f){
			n.SetShipClass(Ship.Rank.FleetCommander);
			return;
		} else if(n.MaxHealth >= totalHealth / 2.5f || n.Level >= totalLevel / 1.125f){
			n.SetShipClass(Ship.Rank.Commander);
			return;
		} else if(n.MaxHealth >= totalHealth / 5f || n.Level >= totalLevel / 1.5f){
			n.SetShipClass(Ship.Rank.General);
			return;
		} else if(n.MaxHealth >= totalHealth / 10f || n.Level >= totalLevel / 3f){
			n.SetShipClass(Ship.Rank.SecondGeneral);
			return;
		} else if(n.MaxHealth >= totalHealth / 15f || n.Level >= totalLevel / 6f){
			n.SetShipClass(Ship.Rank.Captain);
			return;
		} else if(n.MaxHealth >= totalHealth / 20f || n.Level >= totalLevel / 12f){
			n.SetShipClass(Ship.Rank.SecondCaptain);
			return;
		} else if(n.MaxHealth >= totalHealth / 25f || n.Level >= totalLevel / 18f){
			n.SetShipClass(Ship.Rank.SquadronCaptain);
			return;
		} else if(n.MaxHealth >= totalHealth / 30f || n.Level >= totalLevel / 24f){
			n.SetShipClass(Ship.Rank.SquadronFighter);
			return;
		} else if(n.MaxHealth >= totalHealth / 35f || n.Level >= totalLevel / 28f){
			n.SetShipClass(Ship.Rank.Fighter);
			return;
		}
		n.SetShipClass(Ship.Rank.Recruit);
	}
}
