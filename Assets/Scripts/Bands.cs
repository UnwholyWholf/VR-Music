using UnityEngine;
using System.Collections;

public class Bands : MonoBehaviour {

    public static float[] GetAB(int FFT, int bands, int base_width)
    {
        float[] ab = new float[2];
        ab[0] = (2 * ((float)FFT) - 2 * base_width * bands) / (Mathf.Pow(bands, 2) - bands);
        ab[1] = base_width - ab[0];
        return ab;

    }

	public static void GetBands(float[] FFT, float[] bands, float a, float b)
    {
        int freq = 0;
        for (int i = 0; i < bands.Length; i++)
        {
            bands[i] = 0;
            for (int j = 0; j < a * (i + 1) + b; j++)
            {
                bands[i] = Mathf.Max(FFT[freq], bands[i]);
                freq++;
            }
        }
    }
}
