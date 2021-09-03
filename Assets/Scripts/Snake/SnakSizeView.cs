using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Snake))]
public class SnakSizeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _view;

    private Snake _snake;

    private void Awake()
    {
        _snake = GetComponent<Snake>();
    }

    private void OnEnable()
    {
        _snake.SizeUpdated += OnSizeUpdated;
    }

    private void OnDisable()
    {
        _snake.SizeUpdated += OnSizeUpdated;
    }

    private void OnSizeUpdated(int size)
    {
        _view.text = size.ToString();
        if (size <= 0)
        {
            _view.text = null;
        }
    }
}
