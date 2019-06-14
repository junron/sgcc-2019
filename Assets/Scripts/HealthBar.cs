using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar :MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text text;

    private int _value;
    public int barValue
    {
      get => _value;
      set
      {
        if (value > slider.maxValue || value < slider.minValue)
        {
          throw new ArgumentOutOfRangeException($"{value} is out of range for slider.");
        }

        _value = value;
        text.text = value.ToString();
        slider.value = value;
      }
    }
}