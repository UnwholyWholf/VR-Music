using UnityEngine;
using System.Collections;

public class BallVisualizer : MonoBehaviour {

    private int bands;
    public int rows;
    private float[] FFT;
    private float[,] FFTh;
    private float[] band_val;
    private GameObject[,] spheres;

	// Use this for initialization
	void Start () {
        bands = 64;
        FFT = new float[8192];
        FFTh = new float[bands, rows];
        band_val = new float[bands];
        spheres = new GameObject[bands, rows];
        for(int i=0; i < bands; i++)
        {
            for(int j = 0; j < rows; j++)
            {
                spheres[i, j] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                spheres[i, j].transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                spheres[i, j].name = "Band" + i+"Row"+j;
                Material sphereMaterial = spheres[i, j].GetComponent<Renderer>().material;
                sphereMaterial.SetFloat("_Mode", 3.0f); //Set to transparent
                sphereMaterial.SetColor("_Color", GetSphereColor(i, j));
            }
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        AudioListener.GetSpectrumData(FFT, 0, FFTWindow.Rectangular);
        UpdateBands();
        UpdateHistory();
        UpdateSpheres();
    }

    void UpdateBands()
    {
        int a = 4;
        int b = -2;
        int freq = 0;
        for(int i=0; i< bands; i++)
        {
            band_val[i] = 0;
            for(int j=0; j<a*(i+1)+ b; j++)
            {
                band_val[i] = Mathf.Max(FFT[freq], band_val[i]);
                freq++;
            }
        }
    }

    void UpdateHistory()
    {
        for(int i=0; i< rows; i++)
        {
            for(int j=0; j< bands; j++)
            {
                if (i == rows - 1)
                    FFTh[j, i] = band_val[j];
                else
                    FFTh[j, i] = FFTh[j, i + 1];
            }
        }
    }

    void UpdateSpheres()
    {
        for(int i=0; i< rows; i++)
        {
            for(int j=0; j< bands; j++)
            {
                float[] coords = GenerateCoords(j, i);
                spheres[j, i].transform.position = new Vector3(coords[0], coords[1], coords[2]);
            }
        }
    }
   
    float[] GenerateCoords(int b, int r)
    {
        float[] coords = new float[3];
        float theta = (b * 6.28f) / bands + r * 0.02454f;
        coords[0] = Mathf.Cos(theta) * 10f * (((1-((float)b)/bands) * 0.2f) + 0.8f);
        coords[1] = (1 - Mathf.Cos((r * 3.14f) / rows)) * FFTh[b, r]*10 - 1;
        coords[2] = Mathf.Sin(theta) * 10f * (((1 - ((float)b) / bands) * 0.2f) + 0.8f);
        if (b == 6 && r == 63)
            Debug.Log(coords[1]);
        return coords;
    }

    Color GetSphereColor(int band, int row)
    {
        float red;
        float blue;
        if (band < bands/2)
        {
            red = 1 - ((float)band)*2 / bands;
            blue = ((float)band)*2 / bands;
        }
        else
        {
            red = 2 * (((float)band) - bands / 2) / bands;
            blue = 1 - 2 * (((float)band) - bands / 2) / bands;
        }
        return new Color(red, 0, blue);
    }
}
