using UnityEngine;
using System.Linq;

public class NPC : Character {

	Character FindNearestEnemy (){
		var ships = NPCManager.current.GetAllShips ();
		Character closest = null;
		Vector2 position = gameObject.transform.position;

		ships = ships.OrderBy (x => Vector2.SqrMagnitude (x.transform.position)).ToArray();

		int i;
		for (i = 0; i < ships.Length; i++) {
			if(ships[i].shipFaction == shipFaction){
				continue;
			}
			closest = ships[i];
			break;
		}
		return closest;
	}

	void UpdateNearestEnemy(){
		// Instead of calling FindNearestEnemy for all these checks, call it once, save the Character, and use that Character's values until 
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

	void FixedUpdate(){
		if (gameObject.transform.position != targetPosition) {
			DoMovement ();
		}
			
		if (fireCountdown <= 0f && target) {
			for (int i = 0; i < fireBurstCount; i++) {
				Invoke ("Shoot", (i * 1.5f) * Time.deltaTime);
			}
			fireCountdown = fireRate;
		}
		fireCountdown -= Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D collision){ // Collision2D
		var n = collision.gameObject.GetComponent<Character> ();

		if (n == null || n.shipFaction == shipFaction) {
			return;
		}

		// For now, friendly fire will keep friendly units from hitting eachother
		n.DecreaseHealth (Random.Range (Health / 100f, Health / 50f));
	}

	void Shoot(){
		if (target != null && Vector2.Distance(transform.position, target.position) < fireRange) {

			var bulletGO = Object.Instantiate (ObjectController.current.npcBulletPrefab, transform.position, transform.rotation);
			var bullet = bulletGO.GetComponent<Bullet> ();
			bulletGO.transform.position = gameObject.transform.position;
			bulletGO.transform.SetParent (GameController.current.canvas.transform);

			if (bullet != null) {
				bullet.Setup (target.position, this, fireDamage, shipBullet);
			}
		}
	}

	void DoMovement(){
		float delta = Speed * Time.deltaTime;

		gameObject.transform.position = Vector2.MoveTowards (gameObject.transform.position, targetPosition, delta);

		Vector2 vectorToTarget = targetPosition - gameObject.transform.position;
		float angle = (Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
		Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp (transform.rotation, q, delta);
	}
}
