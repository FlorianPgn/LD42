using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jauge : MonoBehaviour
{

    public Image Bar;
    public float Speed = 0.01f;
    public int MaxValue;
    public int TargetAmount;

    private float _currentAmount = 100f;
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
                if (_currentAmount != TargetAmount)
                {
                    _currentAmount += (_currentAmount > TargetAmount ? -1 : 1);
                    Bar.fillAmount = _currentAmount / MaxValue;
                }
            }
            yield return null;
        }
    }
}
