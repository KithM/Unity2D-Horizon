using UnityEngine;

public class Bullet : MonoBehaviour {

	public NPC firedBy;
	public SpriteRenderer sprite;
	public Vector3 target;//public Transform target;
	public float speed;
	public float damage;
	public float explosionRadius;

	public void Seek(Vector3 _target, NPC _firedBy){
		firedBy = _firedBy;
		target = _target;
	}

	void Awake(){
		sprite = gameObject.GetComponent<SpriteRenderer> ();
		sprite.sortingOrder = -1;
	}

	// Update is called once per frame
	void Update () {
		Vector3 dir = target - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame){
			HitTarget ();
			return;
		}

		transform.Translate (dir.normalized * distanceThisFrame, Space.World);

		Quaternion rotation = Quaternion.LookRotation (target - transform.position, transform.TransformDirection(Vector3.up));
		transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
	}

	void HitTarget(){
		if (explosionRadius > 0f) {
			Explode ();
		}

		Destroy (gameObject);
	}

	void Explode (){
		var colliders = new Collider2D[1];
		var con = new ContactFilter2D ();

		Physics2D.OverlapCircle(transform.position, explosionRadius, con, colliders);

		foreach (Collider2D c in colliders) {
			//var npc = collider.GetComponent<NPC> ();
			Damage (c.transform);
		}
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

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, explosionRadius);
	}
}
