using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider Slider;

    // Start is called before the first frame update
    private void Start()
    {
        Slider.value = 0.0f;
    }

    public void SetValue(float value)
    {
        Slider.value = value;
    }

    public void SetMinValue(float minValue)
    {
        Slider.minValue = minValue;
    }

    public void SetMaxValue(float maxValue)
    {
        Slider.maxValue = maxValue;
    }

    public void Hide()
    {
        Slider.gameObject.SetActive(false);
    }

    public void Show()
    {
        Slider.gameObject.SetActive(true);
    }
}