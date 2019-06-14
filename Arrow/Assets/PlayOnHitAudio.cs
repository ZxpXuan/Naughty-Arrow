using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnHitAudio : MonoBehaviour
{
	private void Awake()
	{
		var audios = GetComponents<AudioSource>();

		audios[Random.Range(0, audios.Length)].Play();
	}
}
