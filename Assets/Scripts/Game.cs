using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    public enum TOOLTIP { SMELTER, CRYSTALIZER, TRASH}

    [System.Serializable]
    public struct Upgrade
    {
        public int NbMetal;
        public int NbCrystal;
    }
    public Upgrade[] Upgrades;

    public GameObject Spaceship;

    public Jauge EnergyJauge;
    public Jauge OxygenJauge;
    public TextMeshProUGUI MetalGoal;
    public TextMeshProUGUI CrystalGoal;
    public Button UpgradeBtn;

    private const int ENERGY_MAX_VALUE = 200;
    private const int OXYGEN_MAX_VALUE = 100;

    private const int OXYGEN_COST = 20;
    private const int CRYSTAL_COST = 30;
    private const int METAL_COST = 20;

    private const float OXYGEN_DECAY_DELAY_IN_S = 1f;

    private int _nbMetal;
    private int _nbCrystal;
    private int _currentUpgradeGoal;

    private void Start()
    {
        EnergyJauge.MaxValue = ENERGY_MAX_VALUE;
        EnergyJauge.TargetAmount = 0;
        OxygenJauge.MaxValue = OXYGEN_MAX_VALUE;
        OxygenJauge.TargetAmount = OXYGEN_MAX_VALUE;
        UpgradeBtn.interactable = false;
        UpgradeBtn.onClick.AddListener(UpgradeSpaceship);
        _currentUpgradeGoal = 0;
        UpdateUpgradeUI();
        StartCoroutine(OxygenDecay());
        MusicManager.instance.PlayMainTheme();
    }

    public void Update()
    {
        EnergyJauge.WarnUser(EnergyJauge.TargetAmount < OXYGEN_COST);
        OxygenJauge.WarnUser(OxygenJauge.TargetAmount < OXYGEN_MAX_VALUE * 0.2f); // Warn 20%
    }

    private IEnumerator OxygenDecay()
    {
        WaitForSeconds delay = new WaitForSeconds(OXYGEN_DECAY_DELAY_IN_S);
        while (OxygenJauge.TargetAmount > 0)
        {
            OxygenJauge.TargetAmount--;
            yield return delay;
        }
        GameOver();
    }

    public void AddEnergy(int quantity)
    {
        if (EnergyJauge.TargetAmount + quantity > ENERGY_MAX_VALUE)
        {
            EnergyJauge.TargetAmount = ENERGY_MAX_VALUE;
        }
        else
        {
            EnergyJauge.TargetAmount += quantity;
        }
    }

    public bool AddOxygen(int quantity)
    {
        if (OXYGEN_COST > EnergyJauge.TargetAmount)
        {
            return false;
        }
        else
        {
            EnergyJauge.TargetAmount -= OXYGEN_COST;
            if (OxygenJauge.TargetAmount + quantity > OXYGEN_MAX_VALUE)
            {
                OxygenJauge.TargetAmount = OXYGEN_MAX_VALUE;
            }
            else
            {
                OxygenJauge.TargetAmount += quantity;
            }
            return true;
        }

    }

    public bool AddMetal()
    {
        if (METAL_COST > EnergyJauge.TargetAmount)
        {
            return false;
        }
        else
        {
            EnergyJauge.TargetAmount -= METAL_COST;
            _nbMetal += 1;
            UpdateUpgradeUI();
            return true;
        }
    }

    public bool AddCrystal()
    {
        if (CRYSTAL_COST > EnergyJauge.TargetAmount)
        {
            return false;
        }
        else
        {
            EnergyJauge.TargetAmount -= CRYSTAL_COST;
            _nbCrystal += 1;
            UpdateUpgradeUI();
            return true;
        }
    }

    public void UpdateUpgradeUI()
    {

        //MetalGoal.color = _nbMetal >= Upgrades[_currentUpgradeGoal].NbMetal ? Color.green : Color.black;
        //CrystalGoal.color = _nbCrystal >= Upgrades[_currentUpgradeGoal].NbCrystal ? Color.green : Color.black;

        // If both materials gathered
        UpgradeBtn.interactable = _nbCrystal >= Upgrades[_currentUpgradeGoal].NbCrystal && _nbMetal >= Upgrades[_currentUpgradeGoal].NbMetal;

        MetalGoal.text =_nbMetal + " / " + Upgrades[_currentUpgradeGoal].NbMetal;
        CrystalGoal.text = _nbCrystal + " / " + Upgrades[_currentUpgradeGoal].NbCrystal;
    }

    public void UpgradeSpaceship()
    {
        _nbMetal -= Upgrades[_currentUpgradeGoal].NbMetal;
        _nbCrystal -= Upgrades[_currentUpgradeGoal].NbCrystal;
        _currentUpgradeGoal++;
        SpawnSpaceshipPart(_currentUpgradeGoal);
        UpdateUpgradeUI();
    }

    private void SpawnSpaceshipPart(int partId)
    {
        Spaceship.GetComponent<Animator>().SetTrigger("SpawnR"+_currentUpgradeGoal);
    }

    public void DisplayNeedEnergy()
    {
        Debug.Log("NEED MORE ENERGY");
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER");
    }

}
