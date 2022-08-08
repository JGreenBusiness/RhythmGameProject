using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour
{
    [SerializeField]private int band;
    [SerializeField]private float startScale, maxScale;
    [SerializeField] private bool useBuffer;

    private Material material;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().materials[0];
    }

    void Update()
    {
        if(useBuffer)
        {
            if(((AudioPeer.audioBandBuffer[band] * maxScale) + startScale) > 1)
            {
                transform.localScale = new Vector3(transform.localScale.x, (AudioPeer.audioBandBuffer[band] * maxScale) + startScale, transform.localScale.z);
                Color color = new Color(AudioPeer.audioBandBuffer[band], AudioPeer.audioBandBuffer[band], AudioPeer.audioBandBuffer[band]);
                material.SetColor("_EmissionColor",color);
            }
        }
        else
        {
            if(((AudioPeer.audioBand[band] * maxScale) + startScale) > 1)
            {
                transform.localScale = new Vector3(transform.localScale.x, (AudioPeer.audioBand[band] * maxScale) + startScale, transform.localScale.z);
                Color color = new Color(AudioPeer.audioBand[band], AudioPeer.audioBand[band], AudioPeer.audioBand[band]);
                material.SetColor("_EmissionColor",color);
            }
        }
    }
}
