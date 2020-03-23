using UnityEngine;
using System.Collections;

public class LightVisualize : MonoBehaviour {

    private Visualize visualizer;
    private Light l;

    public int bandLow;
    public int bandHigh;

    // Use this for initialization
    void Start () {
        visualizer = GameObject.Find("_GM").GetComponent<Visualize>();
        l = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        bool[] beats = visualizer.GetBeats();
        l.intensity = Mathf.Max(0.3f, l.intensity - 0.01f);
        for(int i = bandLow; i< bandHigh; i++)
        {
            if (beats[i])
            {
                l.intensity = 2.0f;
                break;
            }

        }
	}
}
