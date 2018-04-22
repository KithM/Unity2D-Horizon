using UnityEngine;

public class Bullet : MonoBehaviour {

	SpriteRenderer sprite;

	Transform target;
	public float speed = 70f;
	public float damage = 50f;
	public float explosionRadius = 0f;

	public void Seek(Transform _target){
		target = _target;
	}

	void Start(){
		sprite = gameObject.GetComponent<SpriteRenderer> ();
		sprite.sortingOrder = -1;
	}

	// Update is called once per frame
	void Update () {
		if (target == null) {
			Destroy (gameObject);
			return;
		}


		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame){
			HitTarget ();
			return;
		}

		transform.Translate (dir.normalized * distanceThisFrame, Space.World);
		//transform.LookAt (Vector2.left);
		//Vector3.RotateTowards (transform.position, new Vector2(target.position.x, target.position.y + 90f), 10f, 180f);

		Quaternion rotation = Quaternion.LookRotation (target.transform.position - transform.position, transform.TransformDirection(Vector3.up));
		transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
	}

	void HitTarget(){
		if (explosionRadius > 0f) {
			Explode ();
		} else {
			Damage (target);
		}

		Destroy (gameObject);
	}

	void Explode (){
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach (Collider collider in colliders) {
			//var npc = collider.GetComponent<NPC> ();
			Damage (collider.transform);
		}
	}

	//please update this script :C

	void Damage (Transform enemy){
		var e = enemy.GetComponent<NPC> ();

		if (e != null) {
			e.DecreaseHealth (damage);
		}
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, explosionRadius);
	}
}
