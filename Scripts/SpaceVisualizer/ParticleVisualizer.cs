using UnityEngine;
using System.Collections;

public class ParticleVisualizer : MonoBehaviour
{
    public int rows = 64;
    public float outerRadius = 10f;
    public float innerRadius = 2f;
    public float yOffset = -2f;
    public bool showInverse = false;
    public float rotateRate = 0.05f;

    private int bands;
    private float[] FFT_left;
    private float[] FFT_right;
    private float[,] FFTh;
    private float[] band_val;
    private float[,] x_vals;
    private float[,] z_vals;
    private Color[] colors;
    private float[,] y_corrections;
    private ParticleSystem particleGen;
    private Transform trans;
    private float[] ab;

    // Use this for initialization
    void Start()
    {
        bands = 64;
        FFT_left = new float[8192];
        FFT_right = new float[8192];
        FFTh = new float[bands, rows];
        band_val = new float[bands];
        CalculateCoords();
        CalculateColors();
        particleGen = GetComponent<ParticleSystem>();
        trans = GetComponent<Transform>();
        ab = Bands.GetAB(8192, bands, 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        trans.Rotate(Vector3.up, rotateRate);
        AudioListener.GetSpectrumData(FFT_left, 0, FFTWindow.Rectangular);
        AudioListener.GetSpectrumData(FFT_right, 1, FFTWindow.Rectangular);
        for(int i=0; i<8192; i++)
        {
            FFT_left[i] += FFT_right[i];
            FFT_left[i] /= 2;
        }
        Bands.GetBands(FFT_left, band_val, ab[0], ab[1]);
        UpdateHistory();
        GenerateParticles();
    }

    void UpdateHistory()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < bands; j++)
            {
                if (i == rows - 1)
                    FFTh[j, i] = band_val[j];
                else
                    FFTh[j, i] = FFTh[j, i + 1];
            }
        }
    }

    void GenerateParticles()
    {
        ParticleSystem.EmitParams particle = new ParticleSystem.EmitParams();
        for(int i=0; i< bands; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                particle.position = GenerateCoords(i, j);
                particle.startColor = colors[i];
                particleGen.Emit(particle, 1);
                if (showInverse)
                {
                    particle.position = new Vector3(particle.position.x, -particle.position.y + (2*yOffset), particle.position.z);
                    particleGen.Emit(particle, 1);
                }
            }
        }
    }

    

    Vector3 GenerateCoords(int b, int r)
    {
        float y =  (y_corrections[b,r] * Mathf.Log(FFTh[b, r]+1)) + yOffset;
        return new Vector3(x_vals[b, r], y, z_vals[b, r]);
    }

    void CalculateColors()
    {
        colors = new Color[bands];
        for(int i=0; i< bands; i++)
            colors[i] = GetColor(i);
    }

    Color GetColor(int band)
    {
        float r;
        float g;
        float b;
        float thirdBandRange = ((float)bands) / 3f;
        // Blue to Green
        if (band < thirdBandRange)
        {
            g = band * 3f/bands;
            b = 1 - g;
            r = 0;
        }
        //Green to Red
        else if(band < 2f * thirdBandRange)
        {
            r = (band - thirdBandRange) *3f/bands;
            g = 1 - r;
            b = 0;
        }
        //Red to Blue
        else
        {
            b = (band - 2f * thirdBandRange) * 3f / bands;
            r = 1 - b;
            g = 0;
        }

        return new Color(r, g, b);
    }

    void CalculateCoords()
    {
        x_vals = new float[bands, rows];
        z_vals = new float[bands, rows];
        y_corrections = new float[bands, rows];
        for (int b=0; b< bands; b++)
        {
            for(int r=0; r< rows; r++)
            {
                float theta = (b * 6.28f) / bands + r * 0.02454f;
                float radiusRange = outerRadius - innerRadius;
                float radius = radiusRange * ((float) r / rows) + innerRadius;
                x_vals[b, r] = Mathf.Cos(theta) * radius;
                z_vals[b, r] = Mathf.Sin(theta) * radius;
                y_corrections[b, r] = (1 - Mathf.Cos((r * Mathf.PI) / rows)) * 2 * Mathf.Pow(b + 1, 1.5f);
            }
        }
    }
}
