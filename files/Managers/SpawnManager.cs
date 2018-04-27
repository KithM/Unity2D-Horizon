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
			fireRate = 5f;
			fireDamage = 5f;
			fireBurstCount = 2f;
			fireRange = 20f; //10f

			bulletSprite = GameController.sc.traderShipBullet;
			shipSprite = GameController.sc.traderShip;
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
			fireRange = 22f; //12f

			bulletSprite = GameController.sc.prisonShipBullet;
			shipSprite = GameController.sc.prisonShip;
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
			fireRange = 25f; //12f

			bulletSprite = GameController.sc.fighterShipBullet;
			shipSprite = GameController.sc.fighterShip;
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
			fireRange = 30f; //20f

			bulletSprite = GameController.sc.advancedFighterShipBullet;
			shipSprite = GameController.sc.advancedFighterShip;
			break;
		case Ship.Type.HeavyFighterShip:
			minHealth = 250;
			maxHealth = 500;
			engineOffset = 0.300f;//0.275f;
			healthBarOffset = 1.75f;
			shipSpeed = 2.325f;
			fireRate = 4.5f;
			fireDamage = 25f;
			fireBurstCount = 3f;
			fireRange = 36f; //20f

			bulletSprite = GameController.sc.heavyFighterShipBullet;
			shipSprite = GameController.sc.heavyFighterShip;
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
			fireRange = 50f; //50f

			bulletSprite = GameController.sc.destroyerShipBullet;
			shipSprite = GameController.sc.destroyerShip;
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
			fireRange = 28f; //18f

			bulletSprite = GameController.sc.droneShipBullet;
			shipSprite = GameController.sc.droneShip;
			break;
		default:
			minHealth = 50;
			maxHealth = 125;
			shipSpeed = 2.5f;
			fireDamage = 5f;
			fireRate = 2f;
			fireRange = 20f; //10f

			bulletSprite = GameController.sc.fighterShipBullet;
			shipSprite = GameController.sc.fighterShip;
			break;
		}

		// Create the body and attach sprites
		var npc_body = Object.Instantiate (GameController.sc.shipPrefab, GameController.canvas.transform);

		Character npc = null;
		if (isPlayer == true) {
			npc = npc_body.AddComponent <Player> ();
		} else {
			npc = npc_body.AddComponent <NPC> ();
		}

		var npc_sprite = npc_body.GetComponent<SpriteRenderer> ();
		npc_sprite.sprite = shipSprite;

		var npc_thrusters = Object.Instantiate (GameController.sc.enginePrefab, npc_body.transform);
		npc_thrusters.transform.position = new Vector3(npc_body.transform.position.x, npc_body.transform.position.y - engineOffset, npc_body.transform.position.z);

		var npc_healthBar = Object.Instantiate (GameController.sc.healthBarPrefab, npc_body.transform);
		npc_healthBar.transform.position = new Vector3(npc_body.transform.position.x, npc_body.transform.position.y + healthBarOffset, npc_body.transform.position.z);

		// Set up RigidBody2D and Collider
		var npc_rb = npc_body.AddComponent<Rigidbody2D> ();
		npc_rb.gravityScale = 0f;
		npc_rb.isKinematic = false;
		npc_rb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;

		var npc_col = npc_body.AddComponent<PolygonCollider2D> ();

		var npc_m = npc_col.sharedMaterial = new PhysicsMaterial2D ();
		npc_m.bounciness = 1f;
		npc_m.friction = 0f;

		// Initial Setup
		npc.SetupShip (shipType, shipFaction, Random.Range (minHealth, maxHealth), shipSpeed, fireRate, fireBurstCount, fireDamage, fireRange, bulletSprite, isPlayer);

		npc_body.transform.position = position;
		npc.gameObject.name = shipType + "" + Random.Range (1000, 10000) + " [" + shipFaction + "]";

		// Final Setup
		npc_rb.mass = npc.MaxHealth;

		Debug.Log ("Spawned Ship of type \'<b>" + npc.shipType + "</b>\' and of faction \'<b>" + npc.shipFaction + "</b>\'.");
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
