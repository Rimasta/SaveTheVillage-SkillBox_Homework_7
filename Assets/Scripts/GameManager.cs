using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ImageTimer harvestTimer;
    public ImageTimer lunchBreakTimer;

    [SerializeField] private Button peasantButton;
    [SerializeField] private Button warriorButton;

    [SerializeField] private Image peasantTimerImg;
    [SerializeField] private Image warriorTimerImg;
    [SerializeField] private Image invasionTimerImg;
    
    [SerializeField] private Text foodSuppliesText;
    [SerializeField] private Text foodProductionText;
    [SerializeField] private Text foodConsumptionText;
    [SerializeField] private Text peasantPopulationText;
    [SerializeField] private Text warriorPopulationText;
    [SerializeField] private Text invasionEnemies;
    [SerializeField] private Text invasionWaveNumber;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text gameVictoryText;

    [SerializeField] private GameObject gameVictoryPanel;
    [SerializeField] private GameObject gameOverPanel;
    
    private int _foodCounter;
    private int _peasantCounter;
    private int _warriorCounter;

    private int _peasantCost = 45;
    private int _warriorCost = 60;

    private int _foodPerPeasant = 25;
    private int _foodToWarrior = 15;

    private float _peasantCreateTime = 10f;
    private float _warriorCreateTime = 2.5f;
    private float _invasionMaxTime = 25f;
    
    private float _peasantTimer = -2f;
    private float _warriorTimer = -2f;
    private float _invasionTimer = -2f;

    private int _nextInvasionScale = 0;
    private int _nextInvasionIncrease = 1;
    private int _invasionCounter = 1;

    public void CreatePeasantOnButtonClick()
    {
        if (_foodCounter >= _peasantCost)
        {
            _foodCounter -= _peasantCost;
            _peasantTimer = _peasantCreateTime;
            peasantButton.interactable = false;
        }
    }

    public void CreateWarriorOnButtonClick()
    {
        if (_foodCounter >= _warriorCost)
        {
            _foodCounter -= _warriorCost;
            _warriorTimer = _warriorCreateTime;
            warriorButton.interactable = false;
        }
    }
    
    private void Start()
    {
        _foodCounter = 150;
        _peasantCounter = 0;
        _warriorCounter = 0;
        _invasionTimer = _invasionMaxTime;
    }
    
    private void Update()
    {
        UpdateText();
        MiningAndEatingFood();
        AddPeasantTimer();
        AddWarriorTimer();
        TimeToInvasion();
        CheckGameState();
    }

    private void AddPeasantTimer()
    {
        if (_peasantTimer > 0)
        {
            _peasantTimer -= Time.deltaTime;
            peasantTimerImg.fillAmount = _peasantTimer / _peasantCreateTime;
        } else if (_peasantTimer > -1)
        {
            peasantTimerImg.fillAmount = 1f;
            peasantButton.interactable = true;
            _peasantCounter++;
            _peasantTimer = -2f;
        }
    }

    private void AddWarriorTimer()
    {
        if (_warriorTimer > 0)
        {
            _warriorTimer -= Time.deltaTime;
            warriorTimerImg.fillAmount = _warriorTimer / _warriorCreateTime;
        } else if (_warriorTimer > -1)
        {
            warriorTimerImg.fillAmount = 1f;
            warriorButton.interactable = true;
            _warriorCounter++;
            _warriorTimer = -2f;
        }
    }

    private void TimeToInvasion()
    {
        _invasionTimer -= Time.deltaTime;
        invasionTimerImg.fillAmount = _invasionTimer / _invasionMaxTime;
        
        if (_invasionTimer <= 0)
        {
            _invasionCounter++;
            _invasionTimer = _invasionMaxTime;
            _warriorCounter -= _nextInvasionScale;

            if (_invasionCounter > 2)
            {
                _nextInvasionScale += _nextInvasionIncrease;
                _nextInvasionIncrease++;
            }
        }
    }

    private void CheckGameState()
    {
        if (_warriorCounter >= 10 && _peasantCounter >= 15 && _foodCounter >= 1500)
        {
            Time.timeScale = 0;
            gameVictoryPanel.SetActive(true);
            gameVictoryText.text = $"Warriors:{_warriorCounter}\n Peasants: {_peasantCounter}\n Wheat: {_foodCounter}";
        }
        
        if (_warriorCounter < 0)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            gameOverText.text = $"Survived {_invasionCounter-1} waves";
        };
    }

    private void UpdateText()
    {
        foodSuppliesText.text = $"{_foodCounter}";
        foodProductionText.text = _peasantCounter * _foodPerPeasant + "/cycle";
        foodConsumptionText.text =_warriorCounter * _foodToWarrior + "/cycle";
        peasantPopulationText.text =$"{_peasantCounter}";
        warriorPopulationText.text =$"{_warriorCounter}";
        invasionEnemies.text = $"{_nextInvasionScale}";
        invasionWaveNumber.text = $"{_invasionCounter}";
    }
    
    private void MiningAndEatingFood()
    {
        if (harvestTimer.timerTick)
            _foodCounter += _peasantCounter * _foodPerPeasant;
        if (lunchBreakTimer.timerTick)
            _foodCounter -= _warriorCounter * _foodToWarrior;
    }
}
