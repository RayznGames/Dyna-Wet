using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.VFX;

public class TimeHandles : MonoBehaviour
{
	public event Action onHourChanged;

	float deltaAngle = 15f;
	float targetAngle;
	float lastAngle;
	float elapsedTime;

	[Tooltip("Defines the angle in the x Rotation at wich the sun is pointing")]
	[SerializeField] [Range(0, 360)] float actualangle;
	
	[Space(20)]
	[Tooltip("Defines the time in a 24H clock")]
	public int hours;
	public int minutes;


	[Header("Speed Controls")]	
	[Tooltip("The seconds it takes for a minute to pass, \\ (1 is equal to a second in Real Time Scale) ")]
	[SerializeField] public float TimeSpeed;
	float timer; //is the counter for each minute;
	float calcHourTime; //removable

	public float calcTransitionDayTime;	//removable	
	[Tooltip("Overrides the scripted rotation of the sun to allow for a custom degree of choice. usefull for animations.")]
	public bool manualTinkering;

	bool activeRotatiuon;

	EnvironmentSystem envSys;

	// Start is called before the first frame update

	void Start()
	{
		activeRotatiuon = true;
		calcTransitionDayTime = CalculateDayNightTransitionTime(TimeSpeed);
		calcHourTime = CalculateHourTime(TimeSpeed);
		SetTime();									
	}

	private void FixedUpdate()
	{		
		RunClock();
		CanRotate(); 
		if (!manualTinkering)
		{
			RotateSun();					
			SetShaderVariables();
		}		
		transform.rotation = Quaternion.Euler(actualangle, transform.rotation.y, transform.rotation.z);
	}

	void SetShaderVariables()
	{
		Shader.SetGlobalVector("_SunDirection", -transform.forward);
		Shader.SetGlobalVector("_MoonDirection", transform.forward);
	}
	public void ChangeTimeSpeed(float newspeed)
	{
		TimeSpeed = newspeed;
    }
	public void SetTime()
	{
		targetAngle = (hours - 6) * 15;
		actualangle = targetAngle;
		lastAngle = actualangle - deltaAngle;
		transform.rotation = Quaternion.Euler(actualangle, transform.rotation.y, transform.rotation.z);
		SetShaderVariables();
		SetDynaWeatherSettings();
	}

	void SetDynaWeatherSettings()
	{
		envSys = GetComponent<EnvironmentSystem>();
		envSys.Bake();	
    }

	#region Rotation And Degrees
	public void RotateSun()
	{
		if (activeRotatiuon)
		{
			elapsedTime += Time.deltaTime;
			float completionpercentage = elapsedTime / CalculateHourTime(TimeSpeed);
			actualangle = Mathf.LerpAngle(lastAngle, targetAngle, completionpercentage);
		}
		else
		{
			elapsedTime = 0f;
		}
	}
	void CanRotate()
	{
		if ((int)actualangle != (int)targetAngle)
		{
			activeRotatiuon = true;
		}
		else
		{
			activeRotatiuon = false;
		}
		Check360();
	}
	void Check360()
	{
		if (actualangle >= 360f)
		{
			actualangle = 0f;
			targetAngle = 0f;
		}
	}
	#endregion

	void RunClock() 
	{	
		timer -= Time.deltaTime;
		if (timer <= 0)
		{			
			ClockCounter();			
			timer = TimeSpeed;
		}
	}
	void ClockCounter()
	{		
		minutes++;						
		if (minutes >= 60)//We've reached the 60 minutes of an hour
		{						
			hours++;//Add one hour to the clock
			minutes = 0;

			lastAngle = actualangle;
			targetAngle += deltaAngle;

			onHourChanged.Invoke();
		}
		if (hours == 24)//We've reached 24 hours a day has passed
		{
			 hours = 0;		
			
			//days ++;
		}
	}

	public float CalculateHourTime(float oneminuteSpeed) 
	{
		float result = oneminuteSpeed *60; //Should theoretiuclally last one ingame hour
		return result;
	}
	public float CalculateDayNightTransitionTime(float oneminuteSpeed) 
	{
		float result = (oneminuteSpeed *60)*4; //Should theoretiuclally last 4 ingame hours.
		return result;
	}
}
