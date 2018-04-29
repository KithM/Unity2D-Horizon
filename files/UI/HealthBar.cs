using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour {

	Character ship;
	Image healthBar;
	Text healthText;
	string levelColor;

	// Use this for initialization
	void Start () {
		ship = gameObject.GetComponentInParent<Character> ();
		healthBar = gameObject.GetComponent<Image> ();
		healthText = gameObject.GetComponentInChildren<Text> ();

		if(ship.shipFaction == Ship.Faction.Ally){
			healthBar.color = Color.green;
		} else if (ship.shipFaction == Ship.Faction.Enemy){
			healthBar.color = Color.red;
		} else {
			healthBar.color = Color.blue;
		}

		levelColor = ColorUtility.ToHtmlStringRGBA(healthBar.color);
	}
	
	// Update is called once per frame
	void Update () {
		SetRotation ();
		SetFill ();
		SetText ();
	}

	void SetText(){
		if (ship.GetType ().ToString () == "Player") {
			healthText.text = "[P1] " + ship.shipClass + "\n" + Mathf.RoundToInt (ship.Health) + " \\ " + Mathf.RoundToInt (ship.MaxHealth) + " [<color=#" + levelColor + ">" + ship.Level + "</color>]";
			return;
		}
		healthText.text = ship.shipClass + "\n" + Mathf.RoundToInt (ship.Health) + " \\ " + Mathf.RoundToInt (ship.MaxHealth) + " [<color=#" + levelColor + ">" + ship.Level + "</color>]";
	}
	void SetFill(){
		healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, ship.Health / ship.MaxHealth, 4 * Time.deltaTime);
	}
	void SetRotation(){
		transform.rotation = Quaternion.identity;
	}
}
