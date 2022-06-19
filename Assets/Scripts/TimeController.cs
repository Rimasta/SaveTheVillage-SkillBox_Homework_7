using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite playSprite;
    [SerializeField] private Sprite x1TimeScale;
    [SerializeField] private Sprite x2TimeScale;
    [SerializeField] private Text timeScaleText;
    [SerializeField] private GameObject timeScaleButton;
    
    private bool isPause;
    
    public void GamePause()
    {
        if (!isPause)
        {
            isPause = true;
            Time.timeScale = 0;
            GetComponent<Image>().sprite = playSprite;
            timeScaleButton.GetComponent<Image>().sprite = x1TimeScale;
            timeScaleText.text = "X1";
            pausePanel.SetActive(true);
        }
        else
        {
            isPause = false;
            Time.timeScale = 1;
            GetComponent<Image>().sprite = pauseSprite;
            pausePanel.SetActive(false);
        }
    }

    public void ChangeTimeScale()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 2;
            GetComponent<Image>().sprite = x2TimeScale;
            timeScaleText.text = "X2";
        }
        else if (Time.timeScale == 2f)
        {
            Time.timeScale = 1;
            GetComponent<Image>().sprite = x1TimeScale;
            timeScaleText.text = "X1";
        }
    }
}
