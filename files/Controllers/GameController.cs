using UnityEngine;

public class GameController : MonoBehaviour {

	public static SpriteController sc;
	public static Canvas canvas;

	void Awake(){
		// Set up our references
		sc = FindObjectOfType<SpriteController> ();
		canvas = FindObjectOfType<Canvas> ();
	}

	// Use this for initialization
	void Start () {
		// TODO: DEBUG ONLY
		InvokeRepeating ("SpawnRandomShip", 1f, 10f);
	}

	void SpawnRandomShip(){
		int randT = Random.Range (0, 5);
		int randF = Random.Range (0, 3);
		NPC.Type npcType;
		NPC.Faction npcFaction;

		switch (randT) {
		case 0:
			npcType = NPC.Type.FighterShip;
			break;
		case 1:
			npcType = NPC.Type.PrisonShip;
			break;
		case 2:
			npcType = NPC.Type.TraderShip;
			break;
		case 3:
			npcType = NPC.Type.AdvancedFighterShip;
			break;
		case 4:
			npcType = NPC.Type.HeavyFighterShip;
			break;
		default:
			npcType = NPC.Type.FighterShip;
			break;
		}
		switch (randF) {
		case 0:
			npcFaction = NPC.Faction.Ally;
			break;
		case 1:
			npcFaction = NPC.Faction.Neutral;
			break;
		case 2:
			npcFaction = NPC.Faction.Enemy;
			break;
		default:
			npcFaction = NPC.Faction.Neutral;
			break;
		}

		SpawnManager.Spawn (npcType, npcFaction, new Vector2(Random.Range(-25f,25f), Random.Range(-25f,25f)));
	}
}
