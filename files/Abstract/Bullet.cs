using UnityEngine;

public class Bullet : MonoBehaviour {

	SpriteRenderer sprite;
	Vector3 target;
	NPC firedBy;
	float speed;
	float damage;

	public void Setup(Vector3 t, NPC f, float d, Sprite s){
		firedBy = f;
		target = t;
		damage = d;
		speed = 25f;
		sprite.sprite = s;
	}

	void Awake(){
		sprite = gameObject.GetComponent<SpriteRenderer> ();
		sprite.sortingOrder = -1;
	}

	// Update is called once per frame
	void FixedUpdate () {
		Vector2 dir = target - transform.position; //Vector3
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame){
			HitTarget ();
			return;
		}

		transform.Translate (dir.normalized * distanceThisFrame, Space.World);

		var rotation = Quaternion.LookRotation (target - transform.position, transform.TransformDirection(Vector3.up));
		transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
	}

	void OnCollisionEnter2D(Collision2D collision){
		var n = collision.gameObject.GetComponent<NPC> ();

		if (n == null) {
			Destroy (gameObject);
			return;
		}
		if(n == firedBy){
			return;
		}

		// For now, friendly fire will keep friendly units from hitting eachother
		Damage (n.transform); // decreaseHealth (damage)
		Destroy (gameObject);
	}

	void HitTarget(){
		Destroy (gameObject);
	}

	void Damage (Transform enemy){
		var e = enemy.GetComponent<NPC> ();

		if (e != null) {
			if(e.Health - damage <= 0f){
				firedBy.IncreaseLevel(e.Level);
			}
			e.DecreaseHealth (damage);
		}
	}
}
