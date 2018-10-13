using UnityEngine.UI;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class Configuration : MonoBehaviour {

	[Header("Sliders")]
	public Slider bloomThreshold;
	public Slider bloomIntensity;
	public Slider timeScale;

	[Header("Dropdowns")]
	public Dropdown antiAliasing;
	public Dropdown qualityLevel;

	// Use this for initialization
	void Start () {
		LoadValues ();
	}

	public void ResetValues(){
		bloomThreshold.value = 0.3f;
		bloomIntensity.value = 1f;
		timeScale.value = 1f;
		antiAliasing.value = 1;
		qualityLevel.value = 4;
	}

	public void LoadValues(){
		if (PlayerPrefs.HasKey ("bloomThreshold")) {
			bloomThreshold.value = PlayerPrefs.GetFloat ("bloomThreshold");
		} else {
			bloomThreshold.value = 0.3f;
		}
		if (PlayerPrefs.HasKey ("bloomIntensity")) {
			bloomIntensity.value = PlayerPrefs.GetInt ("bloomIntensity");
		} else {
			bloomIntensity.value = 1f;
		}

		if (PlayerPrefs.HasKey ("defaultTimeScale")) {
			timeScale.value = PlayerPrefs.GetFloat ("defaultTimeScale");
		} else {
			timeScale.value = 1f;
		}
		if (PlayerPrefs.HasKey ("antiAliasing")) {
			antiAliasing.value = PlayerPrefs.GetInt ("antiAliasing");
		} else {
			antiAliasing.value = 1;
		}
		if (PlayerPrefs.HasKey ("qualityLevel")) {
			qualityLevel.value = PlayerPrefs.GetInt ("qualityLevel");
		} else {
			qualityLevel.value = 4;
		}
	}

	public void ChangeBloomThreshold(){
		var bloom = Camera.main.GetComponent<Bloom> ();
		bloom.bloomThreshold = bloomThreshold.value;
		PlayerPrefs.SetFloat ("bloomThreshold", bloomThreshold.value);
	}
	public void ChangeBloomIntensity(){
		var bloom = Camera.main.GetComponent<Bloom> ();
		bloom.bloomIntensity = bloomIntensity.value;
		PlayerPrefs.SetFloat ("bloomIntensity", bloomIntensity.value);
	}
	public void ChangeTimescale(){
		GameController.current.defaultTimeScale = timeScale.value;
		PlayerPrefs.SetFloat ("defaultTimeScale", timeScale.value);
	}
	public void ChangeAntiAliasing(){
		QualitySettings.antiAliasing = int.Parse(antiAliasing.options[antiAliasing.value].text);
		PlayerPrefs.SetInt ("antiAliasing", antiAliasing.value);
	}
	public void ChangeGraphicsLevel(){
		QualitySettings.SetQualityLevel(qualityLevel.value, true);
		PlayerPrefs.SetInt ("qualityLevel", qualityLevel.value);
	}

	public void SaveConfiguration(){
		PlayerPrefs.Save ();
	}
}
