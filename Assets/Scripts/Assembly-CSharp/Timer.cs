using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	public AnimationManager animations;

	public AudioManager audioManager;

	private bool timerIsActive;

	[HideInInspector]
	public float currentTime;

	[SerializeField]
	private float timeLimit;

	[SerializeField]
	private Text timeToShow;

	[SerializeField]
	private int clockAnimationInterval = 20;

	public int seconds;

	public int minutes;

	private void Start()
	{
		currentTime = 0f;
	}

	private void Update()
	{
		if (timerIsActive && currentTime < timeLimit)
		{
			currentTime += Time.deltaTime;
		}
		if ((int)currentTime > 0 && (int)currentTime % clockAnimationInterval == 0)
		{
			audioManager.Play("Anim_Relogio");
			animations.playAnimacaoRelogio();
		}
		TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
		timeToShow.text = timeSpan.ToString("mm\\:ss");
		minutes = timeSpan.Minutes;
		seconds = timeSpan.Seconds;
	}

	public void startTimer()
	{
		timerIsActive = true;
	}

	public void pauseUnpauseTimer()
	{
		timerIsActive = !timerIsActive;
	}

	public void stopTimer()
	{
		currentTime = 0f;
		timerIsActive = false;
	}
}
