using UnityEngine;

public class Player : Character {

	public void FixedUpdate(){
		targetPosition = Camera.main.ScreenToWorldPoint (new Vector2 (Input.mousePosition.x, Input.mousePosition.y));

		if (gameObject.transform.position != targetPosition) {
			float delta = Speed * Time.deltaTime;

			gameObject.transform.position = Vector2.MoveTowards (gameObject.transform.position, targetPosition, delta);

			Vector2 vectorToTarget = targetPosition - gameObject.transform.position;
			float angle = (Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp (transform.rotation, q, delta);
		}

		if (Input.GetMouseButtonDown (0) && fireCountdown <= 0f) {
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
		if (Vector2.Distance(transform.position, targetPosition) < fireRange) {

			var bulletGO = Object.Instantiate (GameController.sc.playerBulletPrefab, transform.position, transform.rotation);
			var bullet = bulletGO.GetComponent<Bullet> ();
			bulletGO.transform.position = gameObject.transform.position;
			bulletGO.transform.SetParent (GameController.canvas.transform);

			if (bullet != null) {
				bullet.Setup (targetPosition, this, fireDamage, shipBullet);
			}
		}
	}
}
