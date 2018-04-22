using UnityEngine;

public static class SpawnManager {

	public static void Spawn(NPC.Type shipType, NPC.Faction shipFaction, Vector2 position){

		Sprite shipSprite;
		int minHealth;
		int maxHealth;
		float engineOffset = 0f;
		float healthBarOffset = 0f;
		float shipSpeed = 0f;
		float fireRate = 0f;
		float fireDamage = 0f;

		switch (shipType) {
		case NPC.Type.TraderShip:
			minHealth = 20;
			maxHealth = 50;
			engineOffset = 0f;
			healthBarOffset = 1.25f;
			shipSpeed = 3.25f;
			fireRate = 5f;
			fireDamage = 5f;

			shipSprite = GameController.sc.traderShip;
			break;
		case NPC.Type.PrisonShip:
			minHealth = 50;
			maxHealth = 150;
			engineOffset = 0.25f;
			healthBarOffset = 1.625f;
			shipSpeed = 2.5f;
			fireRate = 3.5f;
			fireDamage = 7.5f;

			shipSprite = GameController.sc.prisonShip;
			break;
		case NPC.Type.FighterShip:
			minHealth = 50;
			maxHealth = 100;
			engineOffset = -0.125f;
			healthBarOffset = 1f;
			shipSpeed = 3.5f;
			fireRate = 2f;
			fireDamage = 10f;

			shipSprite = GameController.sc.fighterShip;
			break;
		case NPC.Type.AdvancedFighterShip:
			minHealth = 100;
			maxHealth = 350;
			engineOffset = 0.175f;
			healthBarOffset = 1.275f;
			shipSpeed = 4.75f;
			fireRate = 1f;
			fireDamage = 15f;

			shipSprite = GameController.sc.advancedFighterShip;
			break;
		case NPC.Type.HeavyFighterShip:
			minHealth = 250;
			maxHealth = 500;
			engineOffset = 0.300f;//0.275f;
			healthBarOffset = 1.75f;
			shipSpeed = 2.325f;
			fireRate = 2.5f;
			fireDamage = 25f;

			shipSprite = GameController.sc.heavyFighterShip;
			break;
		case NPC.Type.DestroyerShip:
			minHealth = 500;
			maxHealth = 1000;

			shipSprite = GameController.sc.destroyerShip;
			break;
		default:
			minHealth = 50;
			maxHealth = 125;
			shipSpeed = 2.5f;
			fireRate = 2f;

			shipSprite = GameController.sc.fighterShip;
			break;
		}

		switch (shipFaction) {
		case NPC.Faction.Ally:
			break;
		case NPC.Faction.Enemy:
			break;
		case NPC.Faction.Neutral:
			break;
		}

		// Create the body and attach sprites
		var npc_body = Object.Instantiate (GameController.sc.shipPrefab, GameController.canvas.transform);
		var npc = npc_body.GetComponent <NPC>();
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

		// Set up combat info
		float fireCountdown = 4f / fireRate;

		// Initial Setup
		npc.SetupShip (shipType, shipFaction, Random.Range (minHealth, maxHealth), shipSpeed, fireRate, fireCountdown, fireDamage);

		npc_body.transform.position = position;
		npc.gameObject.name = shipType + "" + Random.Range (1000,10000) + " [" + shipFaction + "]";

		// Final Setup
		npc_rb.mass = npc.MaxHealth;

		Debug.Log ("Spawned NPC of type \'<b>" + npc.shipType +  "</b>\' and of faction \'<b>" + npc.shipFaction + "</b>\'.");
	}
}
