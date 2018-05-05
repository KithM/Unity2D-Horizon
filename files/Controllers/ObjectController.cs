using UnityEngine;

public class ObjectController : MonoBehaviour {

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
	public GameObject npcBulletPrefab;
	public GameObject playerBulletPrefab;

	[Header("Ship Misc Sprites")]
	public Sprite traderShipBullet;
	public Sprite prisonShipBullet;
	public Sprite fighterShipBullet;
	public Sprite advancedFighterShipBullet;
	public Sprite heavyFighterShipBullet;
	public Sprite destroyerShipBullet;
	public Sprite droneShipBullet;

	[Header("Ship Rank Sprites")]
	public Sprite recruit;
	public Sprite fighter;
	public Sprite squadronFighter;
	public Sprite squadronCaptain;
	public Sprite secondCaptain;
	public Sprite captain;
	public Sprite secondGeneral;
	public Sprite general;
	public Sprite commander;
	public Sprite fleetCommander;

	[Header("Particle Prefabs")]
	public ParticleSystem[] shipdeathEffects;

	[Header("Panels")]
	public GameObject pausePanel;

	public ParticleSystem shipDeathEffect(){
		int r = Random.Range (0, shipdeathEffects.Length);
		return shipdeathEffects [r];
	}
	public Sprite GetRankImage(Ship.Rank rank){
		switch (rank) {
		case Ship.Rank.FleetCommander:
			return fleetCommander;
			break;
		case Ship.Rank.Commander:
			return commander;
			break;
		case Ship.Rank.General:
			return general;
			break;
		case Ship.Rank.SecondGeneral:
			return secondGeneral;
			break;
		case Ship.Rank.Captain:
			return captain;
			break;
		case Ship.Rank.SecondCaptain:
			return secondCaptain;
			break;
		case Ship.Rank.SquadronCaptain:
			return squadronCaptain;
			break;
		case Ship.Rank.SquadronFighter:
			return squadronFighter;
			break;
		case Ship.Rank.Fighter:
			return fighter;
			break;
		default:
			break;
		}
		return recruit;
	}

	public void FlipX(GameObject go){
		// Flips the object on the x-axis
		go.transform.Rotate (new Vector2(180f, 0f));
	}
}
