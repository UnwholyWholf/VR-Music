using UnityEngine;
using System.Collections;

public class TileVisualize : MonoBehaviour {

    private Visualize visualizer;
    private Material CubeMat;

    public int bandLow;
    public int bandHigh;
    public float H;
    public float V;

    private float S;

    // Use this for initialization
    void Start()
    {
        visualizer = GameObject.Find("_GM").GetComponent<Visualize>();
        CubeMat = GetComponent<Renderer>().material;
        S = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        bool[] beats = visualizer.GetBeats();
        S = Mathf.Max(0f, S - 0.01f);
        for (int i = bandLow; i < bandHigh; i++)
        {
            if (beats[i])
            {
                S = 1f;
                break;
            }
        }

        CubeMat.SetColor("_Color", Color.HSVToRGB(H, S, V));
    }
}
