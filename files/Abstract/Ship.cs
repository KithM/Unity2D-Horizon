using UnityEngine;

public static class Ship {

	public enum Type {
		DestroyerShip, HeavyFighterShip, AdvancedFighterShip, FighterShip, PrisonShip, TraderShip, DroneShip
	}
	public enum Faction {
		Ally, Enemy, Neutral
	}

}

public class Character : MonoBehaviour {

	public Transform target { get; protected set; } // What is our target?
	public Vector3 targetPosition { get; protected set; } // Where does this NPC want to go?
	public Sprite shipBullet { get; protected set; } // What bullet do we fire?
	public float fireRate { get; protected set; } // How fast do we fire bullets?
	public float fireBurstCount { get; protected set; } // How many times do we fire every burst?
	public float fireDamage { get; protected set; } // How much damage do we deal with one bullet?
	public float fireCountdown { get; protected set; } // How much time until we start a burst?
	public float fireRange { get; protected set; } // How far away can we be to fire?
	public float MaxHealth { get; protected set; } // The maximum hitpoints this NPC can have
	public float Health { get; protected set; } // The current hitpoints this NPC has
	public float Speed { get; protected set; } // The speed of the ship, based on its type
	public int Level { get; protected set; } // What is our experience level?
	public Ship.Type shipType { get; protected set; } // The current Type of the ship
	public Ship.Faction shipFaction { get; protected set; } // The current Faction of the ship

	public virtual void SetupShip (Ship.Type npcType, Ship.Faction npcFaction, float npcHealth, float npcSpeed, float npcFireRate, float npcBurstRate, float npcFireDamage, float npcFireRange, Sprite npcBullet, bool isPlayer){
		SetShipType (npcType);
		SetShipFaction (npcFaction);
		SetMaxHealth (npcHealth);
		SetSpeed (npcSpeed);
		IncreaseLevel (1);
		HealthToMaxHealth ();
		shipBullet = npcBullet;
		fireRate = npcFireRate;
		fireBurstCount = npcBurstRate;
		fireDamage = npcFireDamage;
		fireRange = npcFireRange;

		gameObject.tag = shipFaction.ToString ();

		if (isPlayer == true) {
			return;
		}
		InvokeRepeating ("UpdateNearestEnemy", Random.Range (1.25f, 6.75f), Random.Range (1.25f, 6.75f));
	}

	// GETTERS AND SETTERS
	public void SetSpeed(float speed){
		Speed = speed;
	}
	public void SetShipType(Ship.Type newType){
		shipType = newType;
	}
	public void SetShipFaction(Ship.Faction newFaction){
		shipFaction = newFaction;
	}
	public void SetMaxHealth(float hitPoints){
		MaxHealth = hitPoints;
	}
	public void HealthToMaxHealth(){
		Health = MaxHealth;
	}
	public void SetHealth(float hitPoints){
		if (hitPoints > MaxHealth) {
			// If we try and set our health larger than our current maxHealth, 
			// just set it to maxHealth
			Health = MaxHealth;
			return;
		}
		if(hitPoints <= 0){
			// We have no health left, so we should die
			Die ();
			return;
		}

		Health = hitPoints;
	}
	public void DecreaseHealth(float hitPoints){
		if(hitPoints > Health){
			hitPoints = Health;
		}
		SetHealth (Health - hitPoints);
	}
	public void IncreaseHealth(float hitPoints){
		SetHealth (Health + hitPoints);
	}
	public void IncreaseLevel(int lvl){
		Level += lvl;
		IncreaseHealth (lvl * Level);
		fireDamage += lvl;
	}
	public void Die(){
		var deatheffect = Instantiate (GameController.sc.shipDeathEffect(), GameController.canvas.transform);
		deatheffect.transform.position = gameObject.transform.position;
		deatheffect.Play ();

		Destroy (gameObject);
	}

}
