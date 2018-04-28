using UnityEngine.UI;
using UnityEngine;

public class DisplayRemainingNPC : MonoBehaviour {

	public Image allyRemaining;
	public Image neutralRemaining;
	public Image enemyRemaining;

	public float fill_ally;
	public float fill_neutral;
	public float fill_enemy;

	void Start(){
		InvokeRepeating ("UpdateRemaining", 2f, 2f);
	}

	void Update(){
		allyRemaining.fillAmount = Mathf.Lerp (allyRemaining.fillAmount, fill_ally, 5 * Time.deltaTime);
		neutralRemaining.fillAmount = Mathf.Lerp (neutralRemaining.fillAmount, fill_neutral, 5 * Time.deltaTime);
		enemyRemaining.fillAmount = Mathf.Lerp (enemyRemaining.fillAmount, fill_enemy, 5 * Time.deltaTime);
	}

	void UpdateRemaining(){
		if (NPCManager.GetTotal() > 0){
			fill_ally = (float)NPCManager.GetAlly() / (float)NPCManager.GetTotal();
			fill_neutral = (float)NPCManager.GetNeutral() / (float)NPCManager.GetTotal();
			fill_enemy = (float)NPCManager.GetEnemy() / (float)NPCManager.GetTotal();
		} else {
			fill_ally = 0;
			fill_neutral = 0;
			fill_enemy = 0;
		}
	}
}
