using UnityEngine;
using System.Collections;

public class Visualize : MonoBehaviour
{
    private const int E_SIZE = 43;
    private const int SPECTRUM_SIZE = 8192;

    private float[] spectrum_left;
    private float[] spectrum_right;
    private float[] band_vals;
    private float[,] E;
    private int E_ct;
    private bool[] beat;
    private float[] ab; 

    public float C;
    public int bands;

    // Use this for initialization
    void Start()
    {
        spectrum_left = new float[SPECTRUM_SIZE];
        spectrum_right = new float[SPECTRUM_SIZE];
        band_vals = new float[bands];
        ab = Bands.GetAB(SPECTRUM_SIZE, bands, 2);
        E = new float[E_SIZE, bands];
        beat = new bool[bands];

        E_ct = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AudioListener.GetSpectrumData(spectrum_left, 0, FFTWindow.BlackmanHarris);
        AudioListener.GetSpectrumData(spectrum_right, 1, FFTWindow.BlackmanHarris);
        for (int i = 0; i < SPECTRUM_SIZE; i++)
        {
            spectrum_left[i] += spectrum_right[i];
            spectrum_left[i] /= 2;
        }

        Bands.GetBands(spectrum_left, band_vals, ab[0], ab[1]);
        
        for(int i=0; i < bands; i++)
        {
            float Ei = getAvgEnergy(i);
            E[E_ct % E_SIZE, i] = band_vals[i];
            beat[i] = band_vals[i] > C * Ei;
        }
        E_ct++;
    }

    float getAvgEnergy(int band)
    {
        float avg = 0;
        for (int i = 0; i < E_SIZE; i ++)
        {
            avg += E[i, band];
        }
        avg /= E_SIZE;
        return avg;
    }

    public bool[] GetBeats()
    {
        return beat;
    }

}
