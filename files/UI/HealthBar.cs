using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour {

	NPC ship;
	Image healthBar;
	Text healthText;
	string levelColor;

	// Use this for initialization
	void Start () {
		ship = gameObject.GetComponentInParent<NPC> ();
		healthBar = gameObject.GetComponent<Image> ();
		healthText = gameObject.GetComponentInChildren<Text> ();

		if(ship.shipFaction == Ship.Faction.Ally){
			healthBar.color = Color.green;
		} else if (ship.shipFaction == Ship.Faction.Enemy){
			healthBar.color = Color.red;
		} else {
			healthBar.color = Color.blue;
		}
	}
	
	// Update is called once per frame
	void Update () {
		healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, ship.Health / ship.MaxHealth, 4 * Time.deltaTime);

		if (ship.GetType ().ToString () == "Player") {
			healthText.text = "Player 1\n" + Mathf.RoundToInt (ship.Health) + " \\ " + Mathf.RoundToInt (ship.MaxHealth) + " [<color=" + levelColor + ">" + ship.Level + "</color>]";
		} else {
			healthText.text = "NPC\n" + Mathf.RoundToInt (ship.Health) + " \\ " + Mathf.RoundToInt (ship.MaxHealth) + " [<color=" + levelColor + ">" + ship.Level + "</color>]";
		}

		transform.rotation = Quaternion.identity;

		if (ship.Level >= 50) {
			levelColor = "#b36300";
		} else if (ship.Level >= 40) {
			levelColor = "#cc7100";
		} else if (ship.Level >= 30) {
			levelColor = "#e67f00";
		} else if (ship.Level >= 20) {
			levelColor = "#ff8d00";
		} else if (ship.Level >= 10) {
			levelColor = "#ff981a";
		} else if (ship.Level >= 5) {
			levelColor = "#ffa433";
		} else if (ship.Level < 5) {
			levelColor = "#ffaf4d";
		}
	}
}
