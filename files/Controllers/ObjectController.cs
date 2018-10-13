using UnityEngine;

public class ObjectController : MonoBehaviour {

	// Instance
	public static ObjectController current;

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

	[Header("Misc Prefabs")]
	public ParticleSystem[] shipDeathEffects;

	[Header("Panels")]
	public GameObject pausePanel;

	void Awake(){
		current = this;
	}
	public ParticleSystem shipDeathEffect(){
		int r = Random.Range (0, shipDeathEffects.Length);
		return shipDeathEffects [r];
	}
	public Sprite GetRankImage(Ship.Rank rank){
		switch (rank) {
		case Ship.Rank.FleetCommander:
			return fleetCommander;
		case Ship.Rank.Commander:
			return commander;
		case Ship.Rank.General:
			return general;
		case Ship.Rank.SecondGeneral:
			return secondGeneral;
		case Ship.Rank.Captain:
			return captain;
		case Ship.Rank.SecondCaptain:
			return secondCaptain;
		case Ship.Rank.SquadronCaptain:
			return squadronCaptain;
		case Ship.Rank.SquadronFighter:
			return squadronFighter;
		case Ship.Rank.Fighter:
			return fighter;
		}
		return recruit;
	}

	public void FlipX(GameObject go){
		// Flips the object on the x-axis
		go.transform.Rotate (new Vector2(180f, 0f));
	}
	public void FlipY(GameObject go){
		// Flips the object on the y-axis
		go.transform.Rotate (new Vector2(0f, 180f));
	}
}
