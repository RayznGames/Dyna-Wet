using UnityEngine;
using UnityEngine.VFX;


public class EnvironmentSystem : MonoBehaviour
{
	// Main Variables
	TimeHandles timeHandler;
	Light sunLight;	

	//Public properties
	public enum WeatherType { Sunny, LightCloudy, MediumCloudy, Cloudy, DrizzlyRain, LightRain, Rainy, HardRain, ExtremeRain }
	[Header("Weather Controls")]
	public WeatherType weather;

	[Header("Weather")]
	public Vector2 WindDir;
	public float WindSpeed;
	[Range(0f, 100f)] public float cloudsAmmount;
	[Range(0f, 100f)] public float rainAmmount;
	[Range(0f, 100f)] public float worldWetness;
	[Range(0f, 0.1f)] public float wetmultiplier;
	[Range(0f, 1f)] public float dryMultiplyer;
	public Color cloudsColor;

	[Space(15)]
	[Header("Lights")]
	[Tooltip("Time it takes to change the intensity")]
	[SerializeField] float intensitytransitionTime;
	[Tooltip("Defines the intensity of the sun at the given time")]
	[SerializeField] [Range(0f, 1f)] float sunIntensity;
	[SerializeField] [Range(0f, 1f)] float moonIntensity;

	[Space(15)]
	[Header("Skybox")]
	[Tooltip("Defines the DaynightValue of the Skybox At the given Time")]
	[SerializeField] public float daynightValue;

	[Space(15)]
	[Header("Timers")]
	[Tooltip("Defines the ammount of seconds it takes to change the weather")]
	public float WeathertransitionTime;
	float _weathertransitionTimer;

	[Tooltip("Defines the ammount of seconds it takes to change the clouds ammount")]
	public float cloudsTransitiontime;
	float _cloudsTimer;

	[Tooltip("Defines the ammount of seconds it takes to change the clouds color, (It doesent account for the Day to night clouds color change)")]
	public float cloudsColorTransitionTime;
	float _cloudsColorTimer;

	[Tooltip("The VFX Graph object of the rain within the scene")]
	[SerializeField] VisualEffect rainVFX;
	[Tooltip("The clouds Shadow material for the Texture Cookie ssigned to the sun.")]
	[SerializeField] Material cloudsShadowCookie;
	[Tooltip("Materials")]
	public ScriptableMaterials materialHauler;


	//private Properties
	WeatherType lastWeather;
	Color cloudsColorday = new Color(1f, 1f, 1f, 0f);
	Color cloudsColorNight = new Color(0.019f, 0.019f, 0.019f, 0.019f);
	Color lastcloudsColor;
	float lastCloudsAmmount;
	float lastRainAmmount;

	float intensityElapsedTime;
	float lastSunIntensity;
	float targetSunIntensity;
	float lastmoonIntensity;
	float targetmoonIntensity = 0.15f;

	float targetDayNightValue;
	float lastDayNightValue;
	float daynightElapsedTime;


	// Start is called before the first frame update
	void Start()
	{
		Bake();
	}

	public void Bake()
	{
        timeHandler = GetComponent<TimeHandles>();
		sunLight = GetComponent<Light>();
		//moonLight = transform.GetChild(0).GetComponent<Light>();
		lastWeather = weather;
		lastCloudsAmmount = cloudsAmmount;
		lastRainAmmount = rainAmmount;
		lastcloudsColor = cloudsColor;
		lastSunIntensity = sunIntensity;
		lastmoonIntensity = moonIntensity;
		timeHandler.onHourChanged += DayNightSwitch;
		SetDayNightValueAtStart();
		UpdateShaders();
    }


	// Update is called once per frame
	private void FixedUpdate()
	{
		WeatherChangeGather(); //The switch to flip
		DaynightSkyboxTransition(); //The skybox transitions
		WeatherTransition(); //The weather transitions
		IntensityControl(); //The light transitions
		ChangeWetness(); //The water transitions
		UpdateShaders();// The shaders to update.
	}

	void UpdateShaders()
	{
		//VFX
		rainVFX.SetFloat("RainAmmount", rainAmmount);
		rainVFX.SetVector2("WindDIr", -WindDir);
		rainVFX.SetFloat("WindSpeed", WindSpeed);

		//MAterials
		UpdateWeatherableMaterials();
		cloudsShadowCookie.SetFloat("_CloudsAmmount", cloudsAmmount);
		cloudsShadowCookie.SetVector("_WindDir", WindDir);	
		cloudsShadowCookie.SetFloat("_WindSpeed", WindSpeed);

		//Skybox
		Shader.SetGlobalFloat("_DayNight", daynightValue);
		Shader.SetGlobalFloat("_CloudsAmmount", cloudsAmmount);
		Shader.SetGlobalVector("_WIndDir", WindDir);
		Shader.SetGlobalFloat("_WindSpeed", WindSpeed);
		Shader.SetGlobalColor("_CloudsColor", cloudsColor);
		Shader.SetGlobalColor("_HighCloudsColor", cloudsColor);

		//lIGHTS
		sunLight.intensity = sunIntensity;
		//moonLight.intensity = moonIntensity;
	}
	void UpdateWeatherableMaterials() 
	{
		for (int i = 0; i < materialHauler.weatherablematerials.Length; i++)
		{
			materialHauler.weatherablematerials[i].SetFloat("_RainAmmount", rainAmmount);
			materialHauler.weatherablematerials[i].SetFloat("_Wetness", worldWetness);
		}
	}
	void Wetnesslimitter()
	{
		if (worldWetness < 0f)
		{
			worldWetness = 0f;
		}
		if (worldWetness > 100f)
		{
			worldWetness = 100f;
		}
	}
	void WeatherTransition()
	{
		switch (weather)
		{
			case WeatherType.Sunny:
				float targetCloudAmmount = 5f;
				float targetRainAmmount = 0;
				ChangeRain(lastRainAmmount, targetRainAmmount);
				ChangeCloudsColor(cloudsColorday);
				if (rainAmmount == 0f)
				{
					ChangeCloudsAmmount(lastCloudsAmmount, targetCloudAmmount);
				}
				break;
			case WeatherType.LightCloudy:
				targetCloudAmmount = 15;
				targetRainAmmount = 0;
				ChangeRain(lastRainAmmount, targetRainAmmount);
				ChangeCloudsColor(cloudsColorday);
				if (rainAmmount == 0f)
				{
					ChangeCloudsAmmount(lastCloudsAmmount, targetCloudAmmount);
				}
				break;
			case WeatherType.MediumCloudy:
				targetCloudAmmount = 35;
				targetRainAmmount = 0;
				ChangeRain(lastRainAmmount, targetRainAmmount);
				ChangeCloudsColor(cloudsColorday);
				if (rainAmmount == 0)
				{
					ChangeCloudsAmmount(lastCloudsAmmount, targetCloudAmmount);
				}
				break;
			case WeatherType.Cloudy:
				targetCloudAmmount = 70;
				targetRainAmmount = 0;
				ChangeRain(lastRainAmmount, targetRainAmmount);
				ChangeCloudsColor(cloudsColorday);
				if (rainAmmount == 0f)
				{
					ChangeCloudsAmmount(lastCloudsAmmount, targetCloudAmmount);
				}
				break;
			case WeatherType.DrizzlyRain:
				targetCloudAmmount = 50;
				targetRainAmmount = 5;
				ChangeCloudsAmmount(lastCloudsAmmount, targetCloudAmmount);
				ChangeCloudsColor(cloudsColorday);
				if (cloudsAmmount >= targetCloudAmmount)
				{
					ChangeRain(lastRainAmmount, targetRainAmmount);
				}
				break;
			case WeatherType.LightRain:
				targetCloudAmmount = 90;
				targetRainAmmount = 15;
				ChangeCloudsAmmount(lastCloudsAmmount, targetCloudAmmount);
				ChangeCloudsColor(new Color(0.8f, 0.8f, 0.8f, 0f));
				if (cloudsAmmount >= targetCloudAmmount)
				{
					ChangeRain(lastRainAmmount, targetRainAmmount);
				}
				break;
			case WeatherType.Rainy:
				targetCloudAmmount = 100;
				targetRainAmmount = 40;
				ChangeCloudsAmmount(lastCloudsAmmount, targetCloudAmmount);
				ChangeCloudsColor(new Color(0.5f, 0.5f, 0.5f, 0f));
				if (cloudsAmmount >= targetCloudAmmount)
				{
					ChangeRain(lastRainAmmount, targetRainAmmount);
				}
				break;
			case WeatherType.HardRain:
				targetCloudAmmount = 100;
				targetRainAmmount = 75;
				ChangeCloudsAmmount(lastCloudsAmmount, targetCloudAmmount);
				ChangeCloudsColor(new Color(0.3f, 0.3f, 0.3f, 0f));
				if (cloudsAmmount >= targetCloudAmmount)
				{
					ChangeRain(lastRainAmmount, targetRainAmmount);
				}
				break;
			case WeatherType.ExtremeRain:
				targetCloudAmmount = 100;
				targetRainAmmount = 100;
				ChangeCloudsAmmount(lastCloudsAmmount, targetCloudAmmount);
				ChangeCloudsColor(cloudsColorNight);
				if (cloudsAmmount >= targetCloudAmmount)
				{
					ChangeRain(lastRainAmmount, targetRainAmmount);
				}
				break;
		}
	}

	#region WeatherChangers

	void ChangeWetness()
	{
		if (rainAmmount > 0)
		{
			worldWetness += rainAmmount * wetmultiplier * Time.deltaTime;
		}
		else
		{
			worldWetness -= sunIntensity * dryMultiplyer + 0.01f * Time.deltaTime;
		}
		Wetnesslimitter();
	}
	void ChangeRain(float from, float to)
	{
		if (rainAmmount != to)
		{
			_weathertransitionTimer += Time.deltaTime;

			float percentage = Transition(_weathertransitionTimer, WeathertransitionTime);
			rainAmmount = Mathf.Lerp(from, to, percentage);
		}
		else
		{
			_weathertransitionTimer = 0f;
			lastRainAmmount = rainAmmount;
		}
	}
	void ChangeCloudsAmmount(float from, float to)
	{
		if (cloudsAmmount != to)
		{
			_cloudsTimer += Time.deltaTime;
			float percentage = Transition(_cloudsTimer, cloudsTransitiontime);
			cloudsAmmount = Mathf.Lerp(from, to, percentage);
		}
		else
		{
			_cloudsTimer = 0f;
			lastCloudsAmmount = cloudsAmmount;
		}
	}
	void UpdateCloudsColor(Color from, Color to, float transitionTime)
	{
		if (cloudsColor != to)
		{
			_cloudsColorTimer += Time.deltaTime;
			float percentage = Transition(_cloudsColorTimer, transitionTime);
			cloudsColor = Color.Lerp(from, to, percentage);
		}
		else
		{
			_cloudsColorTimer = 0f;
			lastcloudsColor = cloudsColor;
		}
	}
	void ChangeCloudsColor(Color to)
	{
		if (timeHandler.hours > 16 || timeHandler.hours < 5)
		{			
				UpdateCloudsColor(lastcloudsColor, cloudsColorNight, timeHandler.CalculateDayNightTransitionTime(timeHandler.TimeSpeed));				
		}
		else
		{			
				UpdateCloudsColor(lastcloudsColor, to, cloudsColorTransitionTime);					
		}
	}
	void DaynightSkyboxTransition()
	{
		if (daynightValue != targetDayNightValue)
		{
			daynightElapsedTime += Time.deltaTime;
			float percentage = Transition(daynightElapsedTime, timeHandler.CalculateDayNightTransitionTime(timeHandler.TimeSpeed));
			daynightValue = Mathf.Lerp(lastDayNightValue, targetDayNightValue, percentage);
		}
		else
		{
			daynightElapsedTime = 0f;
			lastDayNightValue = daynightValue;
		}
	}

	#endregion

	private void IntensityControl()
	{
		intensityElapsedTime += Time.deltaTime;
		if (sunIntensity != targetSunIntensity)
		{
			float transition = intensityElapsedTime / timeHandler.CalculateDayNightTransitionTime(timeHandler.TimeSpeed);
			sunIntensity = Mathf.Lerp(lastSunIntensity, targetSunIntensity, transition);
			moonIntensity = Mathf.Lerp(lastmoonIntensity, targetmoonIntensity, transition);
		}
		else
		{
			intensityElapsedTime = 0f;
			lastSunIntensity = sunIntensity;
			lastmoonIntensity = moonIntensity;
		}
	}

	#region Switchers

	void WeatherChangeGather()
	{
		//We gather a change in the weather by comparing to the last weather we selected       
		if (weather != lastWeather)
		{
			//And close it right away by setting last weather to the newly selected weather
			lastWeather = weather;
			//Setting the last rain ammount to the rain ammount at the moment of the change
			_weathertransitionTimer = 0f;
			lastRainAmmount = rainAmmount; 

			_cloudsTimer = 0f;
			lastCloudsAmmount = cloudsAmmount;

			_cloudsColorTimer = 0f;
			lastcloudsColor = cloudsColor;
		}
	}
	void DayNightSwitch()
	{
		if (timeHandler.hours > 16 || timeHandler.hours < 5) //Night
		{
			targetDayNightValue = 0f; //Skybox DaynightValue
			targetSunIntensity = 0f;
			targetmoonIntensity = 0.25f;//Moon Intensity			
		}
		else//Day
		{
			targetDayNightValue = 1f; //Skybox DaynightValue
			targetSunIntensity = 1f;
			targetmoonIntensity = 0f;//Moon Intensity 			
		}
	}

	public void SetDayNightValueAtStart()
	{
		if (timeHandler.hours >= 8 || timeHandler.hours <= 16)
		{
			//lIGHTS
			sunIntensity = 1F;
			targetSunIntensity = 1F;
			moonIntensity = 0f;
			targetmoonIntensity = 0f;
			//Skybox
			daynightValue = 1f;
			targetDayNightValue = 1f;
		}
		if (timeHandler.hours >= 20 || timeHandler.hours <= 4)
		{
			sunIntensity = 0F;
			targetSunIntensity = 0F;
			moonIntensity = 0.1f;
			targetmoonIntensity = 0.1f;
			//Skybox
			daynightValue = 0f;
			targetDayNightValue = 0f;
		}
		if (timeHandler.hours == 5)
		{
			sunIntensity = 0.25F;
			targetSunIntensity = 1F;
			moonIntensity = 0f;
			targetmoonIntensity = 0f;
			//Skybox
			daynightValue = 0.25f;
			targetDayNightValue = 1f;
		}
		if (timeHandler.hours == 6)
		{
			sunIntensity = 0.5F;
			targetSunIntensity = 1F;
			moonIntensity = 0f;
			targetmoonIntensity = 0f;
			//Skybox
			daynightValue = 0.5f;
			targetDayNightValue = 1f;
		}
		if (timeHandler.hours == 7)
		{
			sunIntensity = 0.75F;
			targetSunIntensity = 1F;
			moonIntensity = 0f;
			targetmoonIntensity = 0f;
			//Skybox
			daynightValue = 0.75f;
			targetDayNightValue = 1f;
		}
		if (timeHandler.hours == 17)
		{
			sunIntensity = 0.75F;
			targetSunIntensity = 0F;
			moonIntensity = 0f;
			targetmoonIntensity = 0.1f;
			//Skybox
			daynightValue = 0.75f;
			targetDayNightValue = 0f;
		}
		if (timeHandler.hours == 18)
		{
			sunIntensity = 0.5F;
			targetSunIntensity = 0F;
			moonIntensity = 0f;
			targetmoonIntensity = 0.1f;
			//Skybox
			daynightValue = 0.5f;
			targetDayNightValue = 0f;
		}
		if (timeHandler.hours == 19)
		{
			sunIntensity = 0.25F;
			targetSunIntensity = 0F;
			moonIntensity = 0f;
			targetmoonIntensity = 0.1f;
			//Skybox
			daynightValue = 0.25f;
			targetDayNightValue = 0f;
		}
		lastDayNightValue = daynightValue;
	}

	#endregion

	public float Transition(float timeCount, float transitionTime)
	{
		var percentage = timeCount / transitionTime;
		return percentage;
	}
}
