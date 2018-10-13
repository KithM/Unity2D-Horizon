using UnityEngine;

public class Player : Character {

	void FixedUpdate(){
		GetTargetPosition ();

		if (gameObject.transform.position != targetPosition) {
			DoMovement ();
		}

		if (Input.GetMouseButtonDown (0) && fireCountdown <= 0f) {
			int i;
			for (i = 0; i < fireBurstCount; i++) {
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
		// TODO: The ship bullets can be fired clicking anywhere, but limit their distance to the maximum firerange
		// (the difference between the clicked position (our targetPosition), and our fireRange)
		if (Vector2.Distance (transform.position, targetPosition) < fireRange) {
			var bulletGO = Object.Instantiate (ObjectController.current.npcBulletPrefab, transform.position, transform.rotation);
			var bullet = bulletGO.GetComponent<Bullet> ();
			bulletGO.transform.position = gameObject.transform.position;
			bulletGO.transform.SetParent (GameController.current.canvas.transform);

			if (bullet != null) {
				bullet.Setup (targetPosition, this, fireDamage, shipBullet); //targetPosition
			}
		}
	}

	void GetTargetPosition(){
		targetPosition = Camera.main.ScreenToWorldPoint (new Vector2 (Input.mousePosition.x, Input.mousePosition.y));
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
