using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSoundController : MonoBehaviour
{

    [SerializeField]
    private FrequencySamples _frequencySamples;

    [SerializeField]
    private float _levelFactor = 10.0f;

    private ParticleSystem _particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_frequencySamples != null)
        {
            ParticleSystem.MainModule psMain = _particleSystem.main;

            var newParticles = Mathf.RoundToInt(_frequencySamples.getLevel() * _levelFactor);

            for(int i=0; i < newParticles; i++)
            {
                psMain.startColor = Color.HSVToRGB(_frequencySamples.getRandomValue(), 1.0f, 1.0f);
                _particleSystem.Emit(1);
            }
            
        }
    }
}
