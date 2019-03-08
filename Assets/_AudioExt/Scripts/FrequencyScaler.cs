using UnityEngine;

public class FrequencyScaler : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField]
    private FrequencySamples _samples;

    [SerializeField]
    private float _minSize = 0.2f;
    
    [SerializeField]
    private float _maxSize = 0.5f;

    [SerializeField]
    private float _factor = 1.0f;
    #endregion

    #region Unity Workflow
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_samples != null)
        {
            float scale = Mathf.Clamp(_minSize + (_samples.getLevel() * _factor), _minSize, _maxSize);
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }
    #endregion
}
