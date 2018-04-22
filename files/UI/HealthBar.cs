using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour {

	NPC ship;
	Image healthBar;
	Text healthText;

	// Use this for initialization
	void Start () {
		ship = gameObject.GetComponentInParent<NPC> ();
		healthBar = gameObject.GetComponent<Image> ();
		healthText = gameObject.GetComponentInChildren<Text> ();

		if(ship.shipFaction == NPC.Faction.Ally){
			healthBar.color = Color.green;
		} else if (ship.shipFaction == NPC.Faction.Enemy){
			healthBar.color = Color.red;
		} else {
			healthBar.color = Color.blue;
		}
	}
	
	// Update is called once per frame
	void Update () {
		healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, ship.Health / ship.MaxHealth, 4 * Time.deltaTime);
		healthText.text = Mathf.RoundToInt (ship.Health) + " \\ " + Mathf.RoundToInt(ship.MaxHealth);

		transform.rotation = Quaternion.identity;
	}
}
