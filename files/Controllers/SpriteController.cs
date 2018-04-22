using UnityEngine;

public class SpriteController : MonoBehaviour {

	[Header("Ship Sprites")]
	public Sprite traderShip;
	public Sprite prisonShip;
	public Sprite fighterShip;
	public Sprite advancedFighterShip;
	public Sprite heavyFighterShip;
	public Sprite destroyerShip;

	[Header("Ship Part Prefabs")]
	public GameObject shipPrefab;
	public GameObject enginePrefab;
	public GameObject healthBarPrefab;

	[Header("Ship Misc Prefabs")]
	public GameObject bulletPrefab;

	[Header("Particle Prefabs")]
	public ParticleSystem shipDeathEffect;
}
