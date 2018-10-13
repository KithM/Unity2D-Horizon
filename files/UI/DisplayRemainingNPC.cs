using UnityEngine.UI;
using UnityEngine;

public class DisplayRemainingNPC : MonoBehaviour {

	[Header("Images")]
	public Image allyRemaining;
	public Image neutralRemaining;
	public Image enemyRemaining;

	[Header("Text Elements")]
	public Text allyWinChanceText;
	public Text neutralWinChanceText;
	public Text enemyWinChanceText;

	float fill_ally;
	float fill_neutral;
	float fill_enemy;

	void Start(){
		InvokeRepeating ("UpdateRemaining", 2f, 2f);
		InvokeRepeating ("UpdateWinChances", 5f, 5f);
	}

	void Update(){
		allyRemaining.fillAmount = Mathf.Lerp (allyRemaining.fillAmount, fill_ally, 5 * Time.deltaTime);
		neutralRemaining.fillAmount = Mathf.Lerp (neutralRemaining.fillAmount, fill_neutral, 5 * Time.deltaTime);
		enemyRemaining.fillAmount = Mathf.Lerp (enemyRemaining.fillAmount, fill_enemy, 5 * Time.deltaTime);
	}

	void UpdateRemaining(){
		if (NPCManager.current.GetTotal() > 0){
			fill_ally = (float)NPCManager.current.GetAlly() / (float)NPCManager.current.GetTotal();
			fill_neutral = (float)NPCManager.current.GetNeutral() / (float)NPCManager.current.GetTotal();
			fill_enemy = (float)NPCManager.current.GetEnemy() / (float)NPCManager.current.GetTotal();
		} else {
			fill_ally = 0;
			fill_neutral = 0;
			fill_enemy = 0;
		}
	}

	void UpdateWinChances(){
		if (NPCManager.current.GetTotal () > 0) {
			allyWinChanceText.text = 
				Mathf.RoundToInt(GetFactionTotalHealth(Ship.Faction.Ally) * 100f) + 
				" [" + GetFactionTotalLevel(Ship.Faction.Ally) + "]";
			neutralWinChanceText.text = 
				Mathf.RoundToInt(GetFactionTotalHealth(Ship.Faction.Neutral) * 100f) +
				" [" + GetFactionTotalLevel(Ship.Faction.Neutral) + "]";
			enemyWinChanceText.text = 
				Mathf.RoundToInt(GetFactionTotalHealth(Ship.Faction.Enemy) * 100f) +
				" [" + GetFactionTotalLevel(Ship.Faction.Enemy) + "]";
		} else {
			allyWinChanceText.text = "0";
			neutralWinChanceText.text = "0";
			enemyWinChanceText.text = "0";
		}
	}

	float GetFactionTotalHealth(Ship.Faction faction){
		return NPCManager.current.GetFactionTotalHealth (faction) / NPCManager.current.GetAllTotalHealth ();
	}
	float GetFactionTotalLevel(Ship.Faction faction){
		return NPCManager.current.GetFactionTotalLevel (faction);
	}
}
