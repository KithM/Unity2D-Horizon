using UnityEngine;

public class NPC : MonoBehaviour {

	public Transform target { get; protected set; } // What is our target?
	public Vector3 targetPosition { get; protected set; } // Where does this NPC want to go?
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

	public void SetupShip (Ship.Type npcType, Ship.Faction npcFaction, float npcHealth, float npcSpeed, float npcFireRate, float npcBurstRate, float npcFireDamage, float npcFireRange, Sprite npcBullet){
		SetShipType (npcType);
		SetShipFaction (npcFaction);
		SetMaxHealth (npcHealth);
		SetSpeed (npcSpeed);
		IncreaseLevel (1);
		HealthToMaxHealth ();
		shipBullet = npcBullet;
		fireRate = npcFireRate;
		fireBurstCount = npcBurstRate;
		fireDamage = npcFireDamage;
		fireRange = npcFireRange;

		gameObject.tag = shipFaction.ToString ();

		InvokeRepeating ("UpdateNearestEnemy", Random.Range(1.25f, 6.75f), Random.Range(1.25f, 6.75f));
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

	NPC FindNearestEnemy (){
		NPC[] npcList = FindObjectsOfType<NPC> ();
		NPC closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = gameObject.transform.position;

		foreach (NPC n in npcList) {
			if(n.shipFaction == shipFaction){
				continue;
			}

			Vector3 diff = n.gameObject.transform.position - position;
			float curDistance = diff.sqrMagnitude;

			if (curDistance < distance || n.target == gameObject.transform) { //&& (shipFaction != n.shipFaction || shipFaction == Faction.None) )
				closest = n;
				distance = curDistance;
			}
		}
		return closest;
	}

	void UpdateNearestEnemy(){
		// FIXME: Instead of calling FindNearestEnemy for all these checks, let's try and call it once, save the NPC, and use that NPC's values until 
		// we call this again
		var n = FindNearestEnemy ();

		if (n != null) {
			var pos = n.gameObject.transform.position;
			targetPosition = new Vector2(pos.x + Random.Range(-fireRange, fireRange), pos.y + Random.Range(-fireRange, fireRange)); // -7.5 - 7.5

			target = n.transform;
			return;
		}
		targetPosition = new Vector2(gameObject.transform.position.x + Random.Range(-100f,100f), gameObject.transform.position.y + Random.Range(-100f,100f));
		target = null;
	}

	void LateUpdate(){
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
				Invoke ("Shoot", (i * 1.5f) * Time.deltaTime);//Shoot ();
			}
			fireCountdown = fireRate;
		}
		fireCountdown -= Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D collision){ // Collision2D
		var n = collision.gameObject.GetComponent<NPC> ();
		//var rb = collision.gameObject.GetComponent<Rigidbody2D> ();

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
		if (target != null && Vector2.Distance(transform.position, target.position) < fireRange) {
			Debug.Log ("Firing at " + target.gameObject.name);

			var bulletGO = Object.Instantiate (GameController.sc.bulletPrefab, gameObject.transform.position, transform.rotation);
			bulletGO.transform.position = gameObject.transform.position;
			bulletGO.transform.SetParent (GameController.canvas.transform);

			Bullet bullet = bulletGO.GetComponent<Bullet> ();

			if (bullet != null) {
				bullet.sprite.sprite = shipBullet;
				bullet.damage = fireDamage;
				bullet.Seek (target.position, this);
			}
		}
	}
}
