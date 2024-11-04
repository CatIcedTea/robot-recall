using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Health _health;

    [SerializeField]
    private RectTransform _barRect;

    [SerializeField]
    private RectMask2D _mask;

    [SerializeField]
    private TMP_Text _hpIndicator;

    private float _maxRightMask;
    private float _initialRightMask;
    private void Start()
    {
        _maxRightMask = _barRect.rect.width - _mask.padding.x - _mask.padding.z;
        _hpIndicator.SetText(sourceText: $"{_health.Hp}/{_health.MaxHp}");
        _initialRightMask = _mask.padding.z;
    }
    public void SetValue(int newValue)
    {
        var targetWidth = newValue * _maxRightMask / _health.MaxHp;
        var newRightMask = _maxRightMask + _initialRightMask - targetWidth;
        var padding = _mask.padding;
        padding.z = newRightMask * 7;
        _mask.padding = padding;
        _hpIndicator.SetText($"{newValue}/{_health.MaxHp}");
    }

}
