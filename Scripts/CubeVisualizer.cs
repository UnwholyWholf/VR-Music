using UnityEngine;
using System.Collections;

public class CubeVisualizer : MonoBehaviour
{

    public float scale = 1;
    public float baseHeight = 0.3f;
    public float maxHeight = 5f;
    public Color color = Color.cyan;

    private int bands = 8;
    private int spectrum_size = 8192;
    private float[] spectrum_left;
    private float[] spectrum_right;
    private float[] band_val;
    private GameObject[] cubes;
    private float[] ab;

    // Use this for initialization
    void Start()
    {
        spectrum_left = new float[spectrum_size];
        spectrum_right = new float[spectrum_size];
        band_val = new float[bands];
        cubes = new GameObject[bands];
        ab = Bands.GetAB(spectrum_size, bands, 16);
        for (int i=0; i< bands; i++)
        {
            cubes[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cubes[i].transform.position = this.transform.position + (new Vector3(i * this.transform.localScale.x * this.transform.right.x, 0, 0));
            cubes[i].transform.parent = this.transform;
            cubes[i].GetComponent<Renderer>().material.SetColor("_Color", color);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AudioListener.GetSpectrumData(spectrum_left, 0, FFTWindow.Rectangular);
        AudioListener.GetSpectrumData(spectrum_right, 1, FFTWindow.Rectangular);
        for(int i=0; i<spectrum_size; i++)
        {
            spectrum_left[i] += spectrum_right[i];
            spectrum_left[i] /= 2;
        }

        Bands.GetBands(spectrum_left, band_val, ab[0], ab[1]);

        for (int i = 0; i < bands; i++)
        {
            float y_music = (Mathf.Log(band_val[i] + 1) * Mathf.Pow(i + 1, 1.5f));
            float y_adjusted = (y_music * scale) + baseHeight;
            float y = Mathf.Min(maxHeight, y_adjusted);
            cubes[i].transform.localScale = new Vector3(1, y, 1);
        }
    }

}
