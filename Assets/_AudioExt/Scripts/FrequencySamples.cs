using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FrequencySamples : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField]
    [Range(4, 20)]
    [Tooltip("Highest frequency in kHz for the random value")]
    private float _maxFrequency = 10.0f; 
    #endregion

    #region FrequencySamples Fields
    private const int _frequencySampleCount = 128;
    private AudioSource _audioSource;
    private float[] _spektrum;
    private float[] _cumulativeDistribution;
    private int _highestBand;
    #endregion

    #region Unity Workflow
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _spektrum = new float[_frequencySampleCount];

        // To create a cumulative probability function, the sprectrum is limited to the maximum frequency
        _highestBand = Mathf.RoundToInt(_frequencySampleCount * ((_maxFrequency * 1000) / AudioSettings.outputSampleRate));
        _cumulativeDistribution = new float[_frequencySampleCount];
    }
    
    void Update()
    {
        _audioSource.GetSpectrumData(_spektrum, 0, FFTWindow.Rectangular);
        
        _cumulativeDistribution[0] = _spektrum[0];

        for (int i = 1; i < _frequencySampleCount; i++)
        {
            _cumulativeDistribution[i] = _cumulativeDistribution[i - 1] + _spektrum[i];
        }
    }
    #endregion

    #region FrequencySamples Methods

    /// <summary>
    /// Returns a value with respect to the cumulative distribution function of current frequencies
    /// </summary>
    /// <returns>Float between 0.0 and 1.0</returns>
    public float getRandomValue()
    {
        float uniformRandom = Random.value * _cumulativeDistribution[_highestBand - 1];
        int index = 0;

        for(; index < _highestBand; index++) 
        {
            if(_cumulativeDistribution[index] > uniformRandom)
            {
                break;
            }
        }

        return index / (float)_highestBand;
    }
    
    /// <summary>
    /// Returs the highest value of the CDF.
    /// </summary>
    /// <returns>Float</returns>
    public float getLevel()
    {
        return _cumulativeDistribution[_frequencySampleCount - 1];
    }
    #endregion
}
