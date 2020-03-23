using UnityEngine;
using System.Collections;

public class BeatVisualize : MonoBehaviour
{
    private const int E_SIZE = 1000;
    private const int SPECTRUM_SIZE = 8192;

    private float[] spectrum_left;
    private float[] spectrum_right;
    private float[] band_vals;
    private float[] E;
    private int E_ct;
    private bool beat;
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
        E = new float[E_SIZE];
        beat = false;

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
        
	float energy = 0.0f;
	float avg_energy = getAvgEnergy();
        for(int i=0; i < bands; i++)
        {
            energy += Mathf.Pow(band_vals[i], 2);
        }
	E[E_ct % E_SIZE] = energy;
        E_ct++;
	beat = energy > C * avg_energy; 
    }

    float getAvgEnergy()
    {
        float avg = 0;
        for (int i = 0; i < E_SIZE; i ++)
        {
            avg += E[i];
        }
        avg /= E_SIZE;
        return avg;
    }

    public bool GetBeat()
    {
        return beat;
    }

}
