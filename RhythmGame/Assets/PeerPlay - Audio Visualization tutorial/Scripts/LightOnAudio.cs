using System;

using UnityEngine;

namespace DefaultNamespace
{
	public class LightOnAudio : MonoBehaviour
	{
		[SerializeField] private int band;
		[SerializeField] private float minIntensity, maxIntensity;
		private new Light light;

		private void Start()
		{
			light = GetComponent<Light>();
		}

		private void Update()
		{
			light.intensity = (AudioPeer.audioBandBuffer[band] * (maxIntensity - minIntensity)) + minIntensity;
		}
	}
}