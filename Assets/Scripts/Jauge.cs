using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jauge : MonoBehaviour
{

    public Image Bar;
    public float Speed = 0.05f;
    public int MaxValue;
    public int TargetAmount;

    private float _CurrentAmount = 100f;
    private float _nextUpdate;


    private void Start()
    {
        _nextUpdate = Time.time;
        StartCoroutine(UpdateJauge());
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
}
