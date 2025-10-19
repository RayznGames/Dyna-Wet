using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour
{
	// UI to update
	public TMP_Text weatherText;
	public Slider rainfallingSlider;
	public TMP_Text rainText;
	public Slider rainaccumSlider;
	public TMP_Text rainAccumText;
	public Slider CloudsSider;
	public TMP_Text CloudsAmmountText;
	//wind
	public TMP_Text windSpeed;
	public TMP_Text winddirX;
	public TMP_Text winddirY;
	public TMP_Text overallWindDIr;

	//Time
	public TMP_Text TimeText;

	public TMP_Dropdown weatherDropdown;
	public TMP_Dropdown TimeDropdown;
	public TMP_InputField TimeSpeedInputField;

	//Environment System Reference.
	public EnvironmentSystem enviroSys;
	public TimeHandles timeSys;
	// Start is called before the first frame update
	void Start()
	{
		weatherText.text = enviroSys.weather.ToString();
		rainfallingSlider.value = enviroSys.rainAmmount;
		rainaccumSlider.value = enviroSys.worldWetness;
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void FixedUpdate()
	{
		UpdateUI();
	}

	void UpdateUI() 
	{
		weatherText.text = enviroSys.weather.ToString();

		rainfallingSlider.value = enviroSys.rainAmmount;
		rainText.text = enviroSys.rainAmmount.ToString() + " %";
		rainaccumSlider.value = enviroSys.worldWetness;
		rainAccumText.text = enviroSys.worldWetness.ToString() + " %";
		CloudsSider.value = enviroSys.cloudsAmmount;
		CloudsAmmountText.text = enviroSys.cloudsAmmount.ToString() + " %";
		winddirX.text = enviroSys.WindDir.x.ToString();
		winddirY.text = enviroSys.WindDir.y.ToString();
		windSpeed.text = enviroSys.WindSpeed.ToString() + " Ku/h";		
		if (timeSys.minutes.ToString().Length < 2)
		{
			TimeText.text = timeSys.hours.ToString() + " : 0" + timeSys.minutes.ToString();
		}
		else
		{
			TimeText.text = timeSys.hours.ToString() + " : " + timeSys.minutes.ToString();
		}
		WindDir();
	}
	public void SetWeather()
	{
		string weatherSelValue;
		weatherSelValue = weatherDropdown.captionText.text;
		switch (weatherSelValue)
		{
			case "Sunny":
				enviroSys.weather = EnvironmentSystem.WeatherType.Sunny;
				break;
			case "LightCloudy":
				enviroSys.weather = EnvironmentSystem.WeatherType.LightCloudy;
				break;
			case "MediumCloudy":
				enviroSys.weather = EnvironmentSystem.WeatherType.MediumCloudy;
				break;
			case "Cloudy":
				enviroSys.weather = EnvironmentSystem.WeatherType.Cloudy;
				break;
			case "DrizzlyRain":
				enviroSys.weather = EnvironmentSystem.WeatherType.DrizzlyRain;
				break;
			case "LightRain":
				enviroSys.weather = EnvironmentSystem.WeatherType.LightRain;
				break;
			case "Rainy":
				enviroSys.weather = EnvironmentSystem.WeatherType.Rainy;
				break;
			case "HardRain":
				enviroSys.weather = EnvironmentSystem.WeatherType.HardRain;
				break;
			case "ExtremeRain":
				enviroSys.weather = EnvironmentSystem.WeatherType.ExtremeRain;
				break;
		}
		
	}
	public void SetTime()
	{
		timeSys.hours = int.Parse(TimeDropdown.captionText.text);
		timeSys.TimeSpeed = float.Parse(TimeSpeedInputField.text);		
		timeSys.SetTime();
		enviroSys.SetDayNightValueAtStart();		
	}
	
	void WindDir() 
	{
		overallWindDIr.text = ""; 
		if (enviroSys.WindDir.y > 0.1f)
		{
			overallWindDIr.text += "North";
		}
		if (enviroSys.WindDir.y < -0.1f)
		{
			overallWindDIr.text += "South";
		}		
		if (enviroSys.WindDir.x < -0.1f)
		{
			overallWindDIr.text += "East";
		}
		if (enviroSys.WindDir.x > 0.1f)
		{
			overallWindDIr.text += "West";
		}
		if (enviroSys.WindDir.y < 0.1 && enviroSys.WindDir.y > -0.1f && enviroSys.WindDir.x < 0.1f && enviroSys.WindDir.x > -0.1f)
		{		
			overallWindDIr.text = "Still";
		}
	}
}
