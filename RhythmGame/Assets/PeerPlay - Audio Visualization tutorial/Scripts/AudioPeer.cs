using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{
    private AudioSource audioSource;
    private static int bandCount = 8;  
    private float[] freqBandHeighest = new float[bandCount];
    
    private readonly float[] bufferDecrease = new float[bandCount];
    
    public static readonly float[] audioBand = new float[bandCount];
    public static readonly float[] audioBandBuffer = new float[bandCount];

    public static readonly float[] samples = new float[512];
    public static readonly float[] freqBand = new float[bandCount];
    public static readonly float[] bandBuffer = new float[bandCount];
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
    }

    private void CreateAudioBands()
    {
        for(int i = 0; i < bandCount; i++)
        {
            if(freqBand[i] > freqBandHeighest[i])
                freqBandHeighest[i] = freqBand[i];

            audioBand[i] = (freqBand[i] / freqBandHeighest[i]);
            audioBandBuffer[i] = (bandBuffer[i] / freqBandHeighest[i]);
        }
    }

    private void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samples,0,FFTWindow.Blackman);
    }

    private void MakeFrequencyBands()
    {
        /*
         * Current song (still woozy - that's life) has Hz of 24000
         * Hz of audio source can be retrieved using "AudioSettings.outputSampleRate)/2f"
         * 512 is amount of bands we have available 
         * 24000 / 512 = 46.875hertz per sample
         *
         * Known Frequency Bands
            20 - 60 hertz - Sub Bass
            60 - 250 hertz - Bass
            250 - 500 hertz - Low Midrange
            500 - 2000 hertz - Midrange
            2000 - 4000 hertz - Upper Midrange
            4000 - 6000 hertz - Presence
            6000 - 20000 hertz - Brilliance
         * 
         * Need to find how many samples need to be divided over these frequency bands
         * 0 - 2 = 93.7hertz
         * 1 - 4 = 187.5hertz   -> 94.7 - 281.2
         * 2 - 8 = 375hertz     -> 282.2 - 656.2
         * 3 - 16 = 750hertz    -> 657.2 - 1406.2
         * 4 - 32 = 1500hertz   -> 1407.2 - 2906.2
         * 5 - 64 = 3000hertz   -> 2907.2 - 5906.2
         * 6 - 128 = 6000hertz  -> 5907.2 - 11906.2
         * 7 - 256 = 12000hertz -> 11907.2 - 23906.2
         * 510
         */
        int count = 0;
        
        // Loops through Bands
        for(int i = 0; i < bandCount; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) *2;

            if(i == 7)
                sampleCount += 2;
            
            // Send samples to frequency bands
            for(int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= count;

            freqBand[i] = average * 10;
        }


    }

    private void BandBuffer()
    {
        for(int g = 0; g < bandCount; g++)
        {
            if(freqBand[g] > bandBuffer[g])
            {
                bandBuffer[g] = freqBand[g];
                bufferDecrease[g] = 0.005f;
            }
            if(freqBand[g] < bandBuffer[g])
            {
                bandBuffer[g] -= bufferDecrease[g];
                bufferDecrease[g] *= 1.2f;
            }
        }
    }
}
