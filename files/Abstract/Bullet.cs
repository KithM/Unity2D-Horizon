using UnityEngine;

public class Bullet : MonoBehaviour {

	Rigidbody2D rb;
	Character firedBy;
	SpriteRenderer sprite;
	Vector3 target;
	float speed;
	float damage;

	void Awake(){
		sprite = gameObject.GetComponent<SpriteRenderer> ();
		sprite.sortingOrder = -1;
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}

	void Start(){
		if (gameObject != null && firedBy != null) {
			Physics2D.IgnoreCollision (gameObject.GetComponent<Collider2D> (), firedBy.GetComponent<Collider2D> ());
		}
		RotateToPosition ();
	}

	void FixedUpdate () {
		Vector2 dir = target - transform.position; //Vector3
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame){
			HitTarget ();
			return;
		}
		transform.Translate (dir.normalized * distanceThisFrame, Space.World);
	}

	public void Setup(Vector3 t, Character c, float d, Sprite s){
		firedBy = c;
		target = t;
		damage = d;
		speed = 25f;
		sprite.sprite = s;
	}

	void RotateToPosition(){
		if (gameObject.name.Contains ("NPC")) {
			var rotation = Quaternion.LookRotation (target - transform.position, transform.TransformDirection(Vector3.up));
			transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
			return;
		}

		Vector3 mouse_pos = Input.mousePosition;
		Vector3 object_pos = Camera.main.WorldToScreenPoint (transform.position);
		mouse_pos.x = mouse_pos.x - object_pos.x;
		mouse_pos.y = mouse_pos.y - object_pos.y;
		mouse_pos.z = -5f;
		float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}

	void OnCollisionEnter2D(Collision2D collision){
		var n = collision.gameObject.GetComponent<Character> ();

		if (n == null || n == firedBy) {
			return;
		}

		// For now, friendly fire is disabled
		Damage (n.transform);
		HitTarget ();
	}

	void HitTarget(){
		Destroy (gameObject);
	}

	void Damage (Transform enemy){
		var e = enemy.GetComponent<Character> ();

		if (e != null) {
			if(e.Health - damage <= 0f){
				firedBy.IncreaseLevel(e.Level);
			}
			e.DecreaseHealth (damage);
		}
	}
}
