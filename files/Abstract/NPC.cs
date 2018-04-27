using UnityEngine;

public class NPC : Character {

	Character FindNearestEnemy (){
		Character[] npcList = FindObjectsOfType<Character> ();
		Character closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = gameObject.transform.position;

		foreach (Character n in npcList) {
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

	public void UpdateNearestEnemy(){
		// FIXME: Instead of calling FindNearestEnemy for all these checks, let's try and call it once, save the Character, and use that Character's values until 
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

	public void FixedUpdate(){
		if (gameObject.transform.position != targetPosition) {
			float delta = Speed * Time.deltaTime;

			gameObject.transform.position = Vector2.MoveTowards (gameObject.transform.position, targetPosition, delta);

			Vector2 vectorToTarget = targetPosition - gameObject.transform.position;
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

	public void OnCollisionEnter2D(Collision2D collision){ // Collision2D
		var n = collision.gameObject.GetComponent<Character> ();
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

	public void Shoot(){
		if (target != null && Vector2.Distance(transform.position, target.position) < fireRange) {

			var bulletGO = Object.Instantiate (GameController.sc.npcBulletPrefab, transform.position, transform.rotation);
			var bullet = bulletGO.GetComponent<Bullet> ();
			bulletGO.transform.position = gameObject.transform.position;
			bulletGO.transform.SetParent (GameController.canvas.transform);

			if (bullet != null) {
				bullet.Setup (target.position, this, fireDamage, shipBullet);
			}
		}
	}
}
