using UnityEngine;

public static class SpawnManager {

	public static void Spawn(Ship.Type shipType, Ship.Faction shipFaction, Vector2 position, bool isPlayer = false){

		Sprite shipSprite;
		Sprite bulletSprite;
		int minHealth;
		int maxHealth;
		float engineOffset = 0f;
		float healthBarOffset = 0f;
		float shipSpeed = 0f;
		float fireRate = 0f;
		float fireDamage = 0f;
		float fireRange = 0f;
		float fireBurstCount = 1f;

		switch (shipType) {
		case Ship.Type.TraderShip:
			minHealth = 20;
			maxHealth = 50;
			engineOffset = 0f;
			healthBarOffset = 1.25f;
			shipSpeed = 3.25f;
			fireRate = 3f;
			fireDamage = 5f;
			fireBurstCount = 4f;
			fireRange = 20f;

			bulletSprite = GameController.oc.traderShipBullet;
			shipSprite = GameController.oc.traderShip;
			break;
		case Ship.Type.PrisonShip:
			minHealth = 50;
			maxHealth = 150;
			engineOffset = 0.25f;
			healthBarOffset = 1.625f;
			shipSpeed = 2.5f;
			fireRate = 3.5f;
			fireDamage = 7.5f;
			fireBurstCount = 5f;
			fireRange = 22f;

			bulletSprite = GameController.oc.prisonShipBullet;
			shipSprite = GameController.oc.prisonShip;
			break;
		case Ship.Type.FighterShip:
			minHealth = 50;
			maxHealth = 100;
			engineOffset = -0.125f;
			healthBarOffset = 1f;
			shipSpeed = 3.5f;
			fireRate = 2f;
			fireDamage = 5f;
			fireBurstCount = 9f;
			fireRange = 25f;

			bulletSprite = GameController.oc.fighterShipBullet;
			shipSprite = GameController.oc.fighterShip;
			break;
		case Ship.Type.AdvancedFighterShip:
			minHealth = 100;
			maxHealth = 350;
			engineOffset = 0.175f;
			healthBarOffset = 1.275f;
			shipSpeed = 4.75f;
			fireRate = 2.5f;
			fireDamage = 15f;
			fireBurstCount = 4f;
			fireRange = 30f;

			bulletSprite = GameController.oc.advancedFighterShipBullet;
			shipSprite = GameController.oc.advancedFighterShip;
			break;
		case Ship.Type.HeavyFighterShip:
			minHealth = 250;
			maxHealth = 500;
			engineOffset = 0.300f;
			healthBarOffset = 1.75f;
			shipSpeed = 2.325f;
			fireRate = 4.5f;
			fireDamage = 25f;
			fireBurstCount = 3f;
			fireRange = 36f;

			bulletSprite = GameController.oc.heavyFighterShipBullet;
			shipSprite = GameController.oc.heavyFighterShip;
			break;
		case Ship.Type.DestroyerShip:
			minHealth = 500;
			maxHealth = 1000;
			engineOffset = 0.500f;
			healthBarOffset = 2.00f;
			shipSpeed = 2.125f;
			fireRate = 4f;
			fireDamage = 10f;
			fireBurstCount = 8f;
			fireRange = 50f;

			bulletSprite = GameController.oc.destroyerShipBullet;
			shipSprite = GameController.oc.destroyerShip;
			break;
		case Ship.Type.DroneShip:
			minHealth = 25;
			maxHealth = 65;
			engineOffset = -0.5f;
			healthBarOffset = 0.75f;
			shipSpeed = 5f;
			fireRate = 2f;
			fireDamage = 2.5f;
			fireBurstCount = 12f;
			fireRange = 28f;

			bulletSprite = GameController.oc.droneShipBullet;
			shipSprite = GameController.oc.droneShip;
			break;
		default:
			minHealth = 50;
			maxHealth = 125;
			shipSpeed = 2.5f;
			fireDamage = 5f;
			fireRate = 2f;
			fireRange = 20f;

			bulletSprite = GameController.oc.fighterShipBullet;
			shipSprite = GameController.oc.fighterShip;
			break;
		}

		// Create the body and attach sprites
		var npc_body = Object.Instantiate (GameController.oc.shipPrefab, GameController.canvas.transform);

		Character npc = null;
		if (isPlayer == true) {
			npc = npc_body.AddComponent <Player> ();
		} else {
			npc = npc_body.AddComponent <NPC> ();
		}

		var npc_sprite = npc_body.GetComponent<SpriteRenderer> ();
		npc_sprite.sprite = shipSprite;

		var npc_thrusters = Object.Instantiate (GameController.oc.enginePrefab, npc_body.transform);
		npc_thrusters.transform.position = new Vector3(npc_body.transform.position.x, npc_body.transform.position.y - engineOffset, npc_body.transform.position.z);

		var npc_healthBar = Object.Instantiate (GameController.oc.healthBarPrefab, npc_body.transform);
		npc_healthBar.transform.position = new Vector3(npc_body.transform.position.x, npc_body.transform.position.y + healthBarOffset, npc_body.transform.position.z);

		// Set up RigidBody2D and Collider
		var npc_rb = npc_body.AddComponent<Rigidbody2D> ();
		npc_rb.gravityScale = 0f;
		npc_rb.isKinematic = false;
		npc_rb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;

		var npc_col = npc_body.AddComponent<PolygonCollider2D> ();

		int identifier = Random.Range (100, 999);
		npc.gameObject.name = shipType + "" + identifier + " [" + shipFaction + "]";

		// Initial Setup
		npc.SetupShip (shipType, shipFaction, identifier, Random.Range (minHealth, maxHealth), shipSpeed, fireRate, fireBurstCount, fireDamage, fireRange, bulletSprite, isPlayer);

		npc_body.transform.position = position;

		// Final Setup
		npc_rb.mass = npc.MaxHealth;

		Debug.Log ("Spawned Ship of type \'<b>" + shipType + "</b>\' and of faction \'<b>" + shipFaction + "</b>\'.");
	}

	public static void SpawnRandomShip( Vector2 pos, bool isPlayer = false ){
		Ship.Type npcType = GetRandomShipType ();
		Ship.Faction npcFaction = GetRandomShipFaction();
		SpawnManager.Spawn (npcType, npcFaction, pos, isPlayer);
	}

	public static void SpawnRandomAlly( Vector2 pos, bool isPlayer = false ){
		SpawnManager.Spawn (GetRandomShipType (), Ship.Faction.Ally, pos, isPlayer);
	}
	public static void SpawnRandomNeutral( Vector2 pos, bool isPlayer = false ){
		SpawnManager.Spawn (GetRandomShipType (), Ship.Faction.Neutral, pos, isPlayer);
	}
	public static void SpawnRandomEnemy( Vector2 pos, bool isPlayer = false ){
		SpawnManager.Spawn (GetRandomShipType (), Ship.Faction.Enemy, pos, isPlayer);
	}

	public static void SpawnRandomHeavyAlly( Vector2 pos, bool isPlayer = false ){
		SpawnManager.Spawn (GetRandomHeavyShipType (), Ship.Faction.Ally, pos, isPlayer);
	}
	public static void SpawnRandomHeavyNeutral( Vector2 pos, bool isPlayer = false ){
		SpawnManager.Spawn (GetRandomHeavyShipType (), Ship.Faction.Neutral, pos, isPlayer);
	}
	public static void SpawnRandomHeavyEnemy( Vector2 pos, bool isPlayer = false ){
		SpawnManager.Spawn (GetRandomHeavyShipType (), Ship.Faction.Enemy, pos, isPlayer);
	}

	public static void SpawnRandomLightAlly( Vector2 pos, bool isPlayer = false ){
		SpawnManager.Spawn (GetRandomLightShipType (), Ship.Faction.Ally, pos, isPlayer);
	}
	public static void SpawnRandomLightNeutral( Vector2 pos, bool isPlayer = false ){
		SpawnManager.Spawn (GetRandomLightShipType (), Ship.Faction.Neutral, pos, isPlayer);
	}
	public static void SpawnRandomLightEnemy( Vector2 pos, bool isPlayer = false ){
		SpawnManager.Spawn (GetRandomLightShipType (), Ship.Faction.Enemy, pos, isPlayer);
	}

	public static Ship.Type GetRandomShipType(){		
		int randT = Random.Range (0, 7);
		Ship.Type npcType;

		switch (randT) {
		case 0:
			npcType = Ship.Type.FighterShip;
			break;
		case 1:
			npcType = Ship.Type.PrisonShip;
			break;
		case 2:
			npcType = Ship.Type.TraderShip;
			break;
		case 3:
			npcType = Ship.Type.AdvancedFighterShip;
			break;
		case 4:
			npcType = Ship.Type.HeavyFighterShip;
			break;
		case 5:
			npcType = Ship.Type.DestroyerShip;
			break;
		case 6:
			npcType = Ship.Type.DroneShip;
			break;
		default:
			npcType = Ship.Type.FighterShip;
			break;
		}

		return npcType;
	}

	public static Ship.Faction GetRandomShipFaction(){
		int randF = Random.Range (0, 3);
		Ship.Faction npcFaction;

		switch (randF) {
		case 0:
			npcFaction = Ship.Faction.Ally;
			break;
		case 1:
			npcFaction = Ship.Faction.Neutral;
			break;
		case 2:
			npcFaction = Ship.Faction.Enemy;
			break;
		default:
			npcFaction = Ship.Faction.Neutral;
			break;
		}

		return npcFaction;
	}

	public static Ship.Type GetRandomHeavyShipType(){		
		int randT = Random.Range (0, 3);
		Ship.Type npcType;

		switch (randT) {
		case 0:
			npcType = Ship.Type.AdvancedFighterShip;
			break;
		case 1:
			npcType = Ship.Type.HeavyFighterShip;
			break;
		case 2:
			npcType = Ship.Type.DestroyerShip;
			break;
		default:
			npcType = Ship.Type.AdvancedFighterShip;
			break;
		}

		return npcType;
	}

	public static Ship.Type GetRandomLightShipType(){		
		int randT = Random.Range (0, 4);
		Ship.Type npcType;

		switch (randT) {
		case 0:
			npcType = Ship.Type.DroneShip;
			break;
		case 1:
			npcType = Ship.Type.FighterShip;
			break;
		case 2:
			npcType = Ship.Type.PrisonShip;
			break;
		case 3:
			npcType = Ship.Type.TraderShip;
			break;
		default:
			npcType = Ship.Type.DroneShip;
			break;
		}

		return npcType;
	}

	public static Vector2 GetRandomAllyPosition(){
		return new Vector2 (Random.Range (-125f, -100f), Random.Range (-125f, 125f));
	}
	public static Vector2 GetRandomEnemyPosition(){
		return new Vector2 (Random.Range (125f, 100f), Random.Range (-125f, 125f));
	}
	public static Vector2 GetRandomPosition(){
		return new Vector2 (Random.Range (-125f, 125f), Random.Range (-125f, 125f));
	}
}
