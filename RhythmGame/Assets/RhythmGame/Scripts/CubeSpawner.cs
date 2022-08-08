using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private int band;
    [SerializeField] private int scaler;
    [SerializeField] private int threshold;

    void Update()
    {
        for(int i = 0; i < 8; i++)
        {
            if(AudioPeer.audioBandBuffer[i] * scaler > threshold )
            {
                GameObject newCube = Instantiate(cubePrefab,new Vector3(Random.Range(1,10),Random.Range(1,10),0), Quaternion.identity);
                Destroy(newCube,0.05f);
            }
        }
        
    }
}
