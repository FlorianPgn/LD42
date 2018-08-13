using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jauge : MonoBehaviour
{
    public Image Icon;
    public Image Bar;
    public float Speed = 0.05f;
    public int MaxValue;
    public int TargetAmount;
    private bool _warning;

    private float _CurrentAmount;
    private float _nextUpdate;

    private void Start()
    {
        _nextUpdate = Time.time;
        StartCoroutine(UpdateJauge());
        _CurrentAmount = TargetAmount;
        StopWarning();
    }

    private IEnumerator UpdateJauge()
    {
        while (true)
        {
            if (Time.time > _nextUpdate)
            {
                _nextUpdate = Time.time + Speed;
                if (_CurrentAmount != TargetAmount)
                {
                    _CurrentAmount += (_CurrentAmount > TargetAmount ? -0.25f : 0.25f);
                    Bar.fillAmount = Mathf.Floor(_CurrentAmount) / MaxValue;
                }
            }
            yield return null;
        }
    }

    public void WarnUser(bool val)
    {
        if (val && !_warning)
        {
            _warning = val;
            StartCoroutine(WarnUser());
        }
        _warning = val;
    }

    private IEnumerator WarnUser()
    {
        Color newC = Icon.color;
        while (_warning)
        {
            float l1 = -1;
            float h1 = 1;
            float l2 = 0.2f;
            float h2 = 0.6f;
            newC.a = l2 + (Mathf.Sin(Time.time * 5) - l1) * (h2 - l2) / (h1 - l1);
            Icon.color = newC;
            yield return null;
        }
        StopWarning();
        print("Fin warning");
    }

    private void StopWarning()
    {
        Color newC = Icon.color;
        newC.a = 0;
        Icon.color = newC;
    }
}
