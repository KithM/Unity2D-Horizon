using UnityEngine;

public class SpriteController : MonoBehaviour {

	[Header("Ship Sprites")]
	public Sprite traderShip;
	public Sprite prisonShip;
	public Sprite fighterShip;
	public Sprite advancedFighterShip;
	public Sprite heavyFighterShip;
	public Sprite destroyerShip;
	public Sprite droneShip;

	[Header("Ship Part Prefabs")]
	public GameObject shipPrefab;
	public GameObject enginePrefab;
	public GameObject healthBarPrefab;
	public GameObject bulletPrefab;

	[Header("Ship Misc Sprites")]
	public Sprite traderShipBullet;
	public Sprite prisonShipBullet;
	public Sprite fighterShipBullet;
	public Sprite advancedFighterShipBullet;
	public Sprite heavyFighterShipBullet;
	public Sprite destroyerShipBullet;
	public Sprite droneShipBullet;

	[Header("Particle Prefabs")]
	public ParticleSystem shipDeathEffect;
}
