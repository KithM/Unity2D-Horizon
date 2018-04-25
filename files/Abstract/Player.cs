using UnityEngine;

public class Player : NPC {

	public Transform target { get; protected set; } // What is our target?
	public Vector3 targetPosition { get; protected set; } // Where do we want to go?
	public Sprite shipBullet { get; protected set; } // What bullet do we fire?
	public float fireRate { get; protected set; } // How fast do we fire bullets?
	public float fireBurstCount { get; protected set; } // How many times do we fire every burst?
	public float fireDamage { get; protected set; } // How much damage do we deal with one bullet?
	public float fireCountdown { get; protected set; } // How much time until we start a burst?
	public float fireRange { get; protected set; } // How far away can we be to fire?
	public float MaxHealth { get; protected set; } // The maximum hitpoints this NPC can have
	public float Health { get; protected set; } // The current hitpoints this NPC has
	public float Speed { get; protected set; } // The speed of the ship, based on its type
	public int Level { get; protected set; } // What is our experience level?
	public Ship.Type shipType { get; protected set; } // The current Type of the ship
	public Ship.Faction shipFaction { get; protected set; } // The current Faction of the ship

	public void SetupShip (Ship.Type playerType, Ship.Faction playerFaction, float playerHealth, float playerSpeed, float playerFireRate, float playerBurstRate, float playerFireDamage, float playerFireRange, Sprite playerBullet){
		SetShipType (playerType);
		SetShipFaction (playerFaction);
		SetMaxHealth (playerHealth);
		SetSpeed (playerSpeed);
		IncreaseLevel (1);
		HealthToMaxHealth ();
		shipBullet = playerBullet;
		fireRate = playerFireRate;
		fireBurstCount = playerBurstRate;
		fireDamage = playerFireDamage;
		fireRange = playerFireRange;

		gameObject.tag = shipFaction.ToString ();
	}

	// GETTERS AND SETTERS
	public void SetSpeed(float speed){
		Speed = speed;
	}
	public void SetShipType(Ship.Type newType){
		shipType = newType;
	}
	public void SetShipFaction(Ship.Faction newFaction){
		shipFaction = newFaction;
	}
	public void SetMaxHealth(float hitPoints){
		MaxHealth = hitPoints;
	}
	public void HealthToMaxHealth(){
		Health = MaxHealth;
	}
	public void SetHealth(float hitPoints){
		if (hitPoints > MaxHealth) {
			// If we try and set our health larger than our current maxHealth, 
			// just set it to maxHealth
			Health = MaxHealth;
			return;
		}
		if(hitPoints <= 0){
			// We have no health left, so we should die
			Die ();
			return;
		}

		Health = hitPoints;
	}
	public void DecreaseHealth(float hitPoints){
		if(hitPoints > Health){
			hitPoints = Health;
		}
		SetHealth (Health - hitPoints);
	}
	public void IncreaseHealth(float hitPoints){
		SetHealth (Health + hitPoints);
	}
	public void IncreaseLevel(int lvl){
		Level += lvl;
		IncreaseHealth (lvl * Level);
		fireDamage += lvl;
	}
	public void Die(){
		var deatheffect = Instantiate (GameController.sc.shipDeathEffect, GameController.canvas.transform);
		deatheffect.transform.position = gameObject.transform.position;
		deatheffect.Play ();

		Destroy (gameObject);
	}

	void FixedUpdate(){
		if (gameObject.transform.position != targetPosition) {
			float delta = Speed * Time.deltaTime;

			gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, targetPosition, delta);

			Vector3 vectorToTarget = targetPosition - gameObject.transform.position;
			float angle = (Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp (transform.rotation, q, delta);
		}

		if (fireCountdown <= 0f) {
			for (int i = 0; i < fireBurstCount; i++) {
				Invoke ("Shoot", (i * 1.5f) * Time.deltaTime);
			}
			fireCountdown = fireRate;
		}
		fireCountdown -= Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D collision){
		var n = collision.gameObject.GetComponent<NPC> ();

		if (n == null) {
			return;
		}

		if(n.shipFaction == shipFaction){
			return;
		}

		// For now, friendly fire will keep friendly units from hitting eachother
		n.DecreaseHealth (Random.Range (Health / 100f, Health / 50f));
	}

	void Shoot(){
		
	}
}
