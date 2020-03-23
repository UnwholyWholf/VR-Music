using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxLight : MonoBehaviour
{
    private BeatVisualize beat;
    
    public float decayRate;
    public float maxIntensity;

    private float intensity;
    // Start is called before the first frame update
    void Start()
    {
        beat = this.GetComponent<BeatVisualize>();
	intensity = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        intensity = Mathf.Max(1.0f, intensity - decayRate);
	if(beat.GetBeat())
	{
	    intensity = maxIntensity;
	}
	RenderSettings.skybox.SetFloat("_Exposure",  intensity);
    }
}
