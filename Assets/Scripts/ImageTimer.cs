using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageTimer : MonoBehaviour
{
    public bool timerTick;
    
    [SerializeField]private float maxTime;
    
    private float _currentTime;
    private Image _imgTimer;
    
    private void Start()
    {
        _imgTimer = GetComponent<Image>();
        _currentTime = 0;
    }
    
    private void Update()
    {
        timerTick = false;
        _currentTime += Time.deltaTime;
        
        if (_currentTime >= maxTime)
        {
            timerTick = true;
            _currentTime = 0f;
        }
        
        _imgTimer.fillAmount = _currentTime / maxTime;
    }
}
