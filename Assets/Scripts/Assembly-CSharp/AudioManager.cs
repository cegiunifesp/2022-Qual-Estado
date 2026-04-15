using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public Sound[] sounds;

	public Sound[] mainTrack;

	public float subTrackVolume;

	public float ambienciaVolume;

	[HideInInspector]
	public Sound ambienceSoundRef;

	[SerializeField]
	private AudioMixer mainAudioMixer;

	private void Awake()
	{
		Sound[] array = sounds;
		foreach (Sound sound in array)
		{
			sound.source = base.gameObject.AddComponent<AudioSource>();
			sound.source.outputAudioMixerGroup = sound.mixerGroup;
			sound.source.clip = sound.clip;
			sound.source.volume = sound.volume;
			sound.source.pitch = sound.pitch;
			sound.source.loop = sound.loop;
		}
		array = sounds;
		foreach (Sound s in array)
		{
			if (s.hasLoopAfter)
			{
				s.loopContinuation = Array.Find(sounds, (Sound sound3) => sound3.nameOfClip == s.nameOfLoopAfter).source;
			}
		}
		array = mainTrack;
		foreach (Sound sound2 in array)
		{
			sound2.source = base.gameObject.AddComponent<AudioSource>();
			sound2.source.outputAudioMixerGroup = sound2.mixerGroup;
			sound2.source.clip = sound2.clip;
			sound2.source.volume = sound2.volume;
			sound2.source.pitch = sound2.pitch;
			sound2.source.loop = sound2.loop;
		}
		UpdateVolume();
		ambienceSoundRef = Array.Find(sounds, (Sound sound3) => sound3.nameOfClip == "Ambiencia");
	}

	public void FadeMenuSound()
	{
		StartCoroutine(Fade(2f));
	}

	public IEnumerator Fade(float timeToFade)
	{
		float vInicial = 0f;
		while (vInicial != -80f)
		{
			mainAudioMixer.SetFloat("volumePrincipal", vInicial - 2f);
			mainAudioMixer.GetFloat("volumePrincipal", out vInicial);
			yield return new WaitForSeconds(0.05f);
		}
		Debug.Log("Saiu do loop");
	}

	public string RandomSong()
	{
		return UnityEngine.Random.Range(0, 3).ToString();
	}

	public void PlayMainTrack()
	{
		Sound sound = Array.Find(sounds, (Sound sound2) => sound2.nameOfClip == "MainTrack");
		mainTrack[0].source.volume = 0f;
		mainTrack[1].source.volume = 0f;
		mainTrack[2].source.volume = 0f;
		mainTrack[3].source.volume = 0f;
		mainTrack[4].source.volume = 0f;
		mainTrack[0].source.Play();
		mainTrack[1].source.Play();
		mainTrack[2].source.Play();
		mainTrack[3].source.Play();
		mainTrack[4].source.Play();
		sound.source.Play();
	}

	public void PlaySubTrack(string name)
	{
		Sound sound = Array.Find(mainTrack, (Sound sound2) => sound2.nameOfClip == name);
		if (PlayerPrefs.GetString("audio") == "on")
		{
			sound.source.volume = subTrackVolume;
		}
	}

	public void StopSubTrack(string name)
	{
		Array.Find(mainTrack, (Sound sound) => sound.nameOfClip == name).source.volume = 0f;
	}

	public void EnableAmbiencia()
	{
		mainAudioMixer.SetFloat("frequence", 300f);
		if (PlayerPrefs.GetString("audio") == "on")
		{
			ambienceSoundRef.source.volume = ambienciaVolume;
		}
	}

	public void DisableAmbiencia()
	{
		mainAudioMixer.SetFloat("frequence", 22000f);
		ambienceSoundRef.source.volume = 0f;
	}

	public void Play(string name)
	{
		Sound sound = Array.Find(sounds, (Sound sound2) => sound2.nameOfClip == name);
		string id;
		if (sound.hasMultiple)
		{
			id = RandomSong();
			if (id == "0")
			{
				id = "";
			}
			sound = Array.Find(sounds, (Sound sound2) => sound2.nameOfClip == name + id);
		}
		else
		{
			id = "";
		}
		sound.source.Play();
		if (sound.hasLoopAfter)
		{
			sound.loopContinuation.PlayDelayed(sound.source.clip.length);
		}
	}

	public void Stop(string name)
	{
		Sound sound = Array.Find(sounds, (Sound sound2) => sound2.nameOfClip == name);
		sound.source.Stop();
		sound.loopContinuation.Stop();
	}

	public void UpdateVolume()
	{
		Sound[] array = sounds;
		foreach (Sound sound in array)
		{
			if (PlayerPrefs.GetString("audio") == "on")
			{
				sound.source.volume = sound.volume;
			}
			else
			{
				sound.source.volume = 0f;
			}
		}
	}
}
