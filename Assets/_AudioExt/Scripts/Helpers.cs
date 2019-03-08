using UnityEngine;

public class Helpers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static Color SampleToColor(float hue)
    {
        return Color.HSVToRGB(hue, 1.0f, 1.0f);
    }
}
