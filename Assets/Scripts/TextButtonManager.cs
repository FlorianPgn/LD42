using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TextButtonManager : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerClickHandler
{

    private Button _btn;
    private TextMeshProUGUI _text;
    private bool _pointerOver;

    public void OnPointerClick(PointerEventData eventData)
    {
        _pointerOver = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_btn.IsInteractable())
            _pointerOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _pointerOver = false;
    }

    // Use this for initialization
    void Start()
    {
        _btn = GetComponent<Button>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_pointerOver)
        {
            _text.color = _btn.colors.highlightedColor;
        }
        else
        {
            _text.color = _btn.IsInteractable() ? _btn.colors.normalColor : _btn.colors.disabledColor;
        }
        if (!gameObject.activeInHierarchy)
        {
            _pointerOver = false;
        }
    }
}
