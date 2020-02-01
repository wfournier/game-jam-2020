using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider Slider;

    // Start is called before the first frame update
    void Start()
    {
        this.Slider.value = 0.0f;
    }

    public void SetValue(float value)
    {
        this.Slider.value = value;
    }

    public void SetMinValue(float minValue)
    {
        this.Slider.minValue = minValue;
    }

    public void SetMaxValue(float maxValue)
    {
        this.Slider.maxValue = maxValue;
    }

    public void Hide()
    {
        this.Slider.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.Slider.gameObject.SetActive(true);
    }
}
