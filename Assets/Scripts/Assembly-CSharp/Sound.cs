using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class Sound
{
	public string nameOfClip;

	public AudioClip clip;

	public AudioMixerGroup mixerGroup;

	[Range(0f, 1f)]
	public float volume;

	[Range(0.1f, 3f)]
	public float pitch;

	public bool loop;

	public bool hasLoopAfter;

	public string nameOfLoopAfter;

	public bool hasMultiple;

	[HideInInspector]
	public AudioSource source;

	[HideInInspector]
	public AudioSource loopContinuation;
}
