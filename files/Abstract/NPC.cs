using UnityEngine;

public class NPC : MonoBehaviour {

	public Transform target { get; protected set; } // What is our target?
	public Vector3 targetPosition { get; protected set; } // // Where does this NPC want to go?
	public float fireRate { get; protected set; } // How many bullets do we fire when shooting?
	public float fireDamage { get; protected set; } // How much damage do we deal with one bullet?
	public float fireCountdown { get; protected set; } // What is the delay between firing?
	public float MaxHealth { get; protected set; } // The maximum hitpoints this NPC can have
	public float Health { get; protected set; } // The current hitpoints this NPC has
	public float shipSpeed { get; protected set; } // The speed of the ship, based on its type
	public Type shipType { get; protected set; } // The current Type of the ship
	public Faction shipFaction { get; protected set; } // The current Faction of the ship

	public enum Type {
		DestroyerShip, HeavyFighterShip, AdvancedFighterShip, FighterShip, PrisonShip, TraderShip
	}
	public enum Faction {
		Ally, Enemy, Neutral
	}

	public void SetupShip (NPC.Type npcType, NPC.Faction npcFaction, float npcHealth, float npcSpeed, float npcFireRate, float npcFireCountdown, float npcFireDamage){
		SetShipType (npcType);
		SetShipFaction (npcFaction);
		SetMaxHealth (npcHealth);
		SetSpeed (npcSpeed);
		HealthToMaxHealth ();
		fireRate = npcFireRate;
		fireCountdown = npcFireCountdown;
		fireDamage = npcFireDamage;

		InvokeRepeating ("UpdateNearestEnemy", Random.Range(1.25f, 6.75f), Random.Range(1.25f, 6.75f));
	}

	// GETTERS AND SETTERS
	public void SetSpeed(float speed){
		shipSpeed = speed;
	}
	public void SetShipType(NPC.Type newType){
		shipType = newType;
	}
	public void SetShipFaction(NPC.Faction newFaction){
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
			if (shipFaction == Faction.Neutral) {
				continue;
			}

			Vector3 diff = n.gameObject.transform.position - position;
			float curDistance = diff.sqrMagnitude;

			if (curDistance < distance && shipFaction != n.shipFaction) {
				closest = n;
				distance = curDistance;
			}
		}
		return closest;
	}

	void UpdateNearestEnemy(){
		if (FindNearestEnemy () != null) {
			var pos = FindNearestEnemy ().gameObject.transform.position;
			targetPosition = new Vector2(pos.x + Random.Range(-5f,5f), pos.y + Random.Range(-5f,5f));

			target = FindNearestEnemy ().transform;
			return;
		}
		targetPosition = new Vector2(gameObject.transform.position.x + Random.Range(-100f,100f), gameObject.transform.position.y + Random.Range(-100f,100f));
		target = null;
	}

	void LateUpdate(){
		//var position = new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y);

		if (gameObject.transform.position != targetPosition) {
			//gameObject.transform.position = Vector2.Lerp (position, targetPosition, shipSpeed * Time.deltaTime);

			float delta = shipSpeed * Time.deltaTime;
			gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, targetPosition, delta);

			Vector3 vectorToTarget = targetPosition - gameObject.transform.position;
			float angle = (Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * shipSpeed);
		}

		if (fireCountdown <= 0f) {
			Shoot ();
			fireCountdown = 4f / fireRate;
		}
		fireCountdown -= Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D collision){ // Collision2D
		var n = collision.gameObject.GetComponent<NPC> ();

		if (n == null || n.shipFaction == shipFaction) {
			return;
		}

		// For now, friendly fire will keep friendly units from hitting eachother
		n.DecreaseHealth (Random.Range (MaxHealth / 25f, MaxHealth / 8f));
	}

	void Shoot(){
		if (target != null && Vector2.Distance(transform.position, target.position) < 10f) {
			Debug.Log ("Firing at " + target.gameObject.name);

			var bulletGO = Instantiate (GameController.sc.bulletPrefab, gameObject.transform.position, transform.rotation);
			bulletGO.transform.position = gameObject.transform.position;
			bulletGO.transform.SetParent (GameController.canvas.transform);

			Bullet bullet = bulletGO.GetComponent<Bullet> ();

			if (bullet != null) {
				bullet.damage = fireDamage;
				bullet.Seek (target);
			}
		}
	}
}
