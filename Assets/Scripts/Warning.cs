using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Warning : MonoBehaviour {
    
    public float Min = 0.2f;
    public float Max = 0.6f;
    public float Speed = 1f;

    private Color _newColor;
    private TextMeshProUGUI _text;

    // Use this for initialization
    void Start () {
        _text = GetComponent<TextMeshProUGUI>();
        _newColor = _text.color;
    }

    // Update is called once per frame
    void Update() {
        _newColor.a = Min + (Mathf.Sin(Time.time * Speed) +1) * (Max - Min) / (2);
        _text.color = _newColor;
    }
}

