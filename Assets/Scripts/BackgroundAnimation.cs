using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundAnimation : MonoBehaviour
{
    private static int lastpos;

    public float speed = 0.04f;

    private int _pos0;

    private int _pos1;

    private float _timer;


    private Image back; 

    float _t;

    private readonly Color[] _colors = { Color.red / 1.5f, Color.yellow / 1.5f, Color.green / 1.5f, Color.blue / 1.5f, Color.magenta / 1.5f };

    private void Start()
    {
        back = GetComponent<Image>();
        _pos1 = (int)Mathf.Repeat(lastpos++, _colors.Length - 1);
        SelectNextColor();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        _t = _timer / speed;
        back.color = Color.Lerp(_colors[_pos0], _colors[_pos1], _t);
        if (_t >= 1f)
        {
            SelectNextColor();
        }
    }

    private void SelectNextColor()
    {
        _pos0 = _pos1;
        _pos1 = _pos1 < _colors.Length - 1 ? _pos1 + 1 : 0;
        _timer = 0f;

    }
}
