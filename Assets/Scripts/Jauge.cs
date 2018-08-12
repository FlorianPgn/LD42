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
    public bool Warning = true;

    private float _CurrentAmount;
    private float _nextUpdate;

    private void Start()
    {
        _nextUpdate = Time.time;
        StartCoroutine(UpdateJauge());
        _CurrentAmount = TargetAmount;
        StartCoroutine(WarnUser());
    }

    private void Update()
    {
        //_targetAmount = Test;
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
    private IEnumerator WarnUser()
    {
        while (_CurrentAmount > 0)
        {
            Color newC = Icon.color;
            float l1 = -1;
            float h1 = 1;
            float l2 = 0.2f;
            float h2 = 1;
            newC.a = l2 + (Mathf.Sin(Time.time * 5) - l1) * (h2 - l2) / (h1 - l1);
            Icon.color = newC;
            yield return null;
        }
        print("Fin warning");
    }
}
