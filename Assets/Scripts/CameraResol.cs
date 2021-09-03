using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResol : MonoBehaviour
{
    private float _defaultWigth;
    private Camera _cam;
    private void Start()
    {
        _cam = Camera.main;
        _defaultWigth = _cam.orthographicSize * _cam.aspect;
    }


    private void Update()
    {
        _cam.orthographicSize = _defaultWigth / _cam.aspect;
    }

}
