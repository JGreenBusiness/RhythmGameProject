using System;

using UnityEngine;

namespace DefaultNamespace
{
	public class Instantiate512Cubes : MonoBehaviour
	{
		public GameObject sampleCubePrefab;
		private GameObject[] sampleCube = new GameObject[512];
		public float maxScale;

		private void Start()
		{
			for(int i = 0; i < 512; i++)
			{
				GameObject instanceSampleCube = (GameObject) Instantiate(sampleCubePrefab);
				instanceSampleCube.transform.position = this.transform.position;
				instanceSampleCube.transform.parent = this.transform;
				instanceSampleCube.name = $"SampleCube ({i})";
				this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0);
				instanceSampleCube.transform.position = Vector3.forward * 100;
				sampleCube[i] = instanceSampleCube;
			}
		}

		private void Update()
		{
			for(int i = 0; i < 512; i++)
			{
				if(sampleCube != null)
				{
					sampleCube[i].transform.localScale = new Vector3(10, (AudioPeer.samples[i] * maxScale) +2, 10);
				}
			}
		}
	}
}