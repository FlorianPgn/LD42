using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    public enum Machine { SMELTER, CRYSTALIZER, TRASH, DISPENSER, TRANSPORTER }

    [System.Serializable]
    public struct Tooltip
    {
        public Machine machine;
        public GameObject tip;
    }
    public Tooltip[] Tooltips;

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
    public Image MetalGlow;
    public Image CrystalGlow;
    public Button UpgradeBtn;
    public Warning EnergyWarning;
    public GameObject WinPanel;

    private const int ENERGY_MAX_VALUE = 200;
    private const int OXYGEN_MAX_VALUE = 100;

    private const int OXYGEN_COST = 20;
    private const int CRYSTAL_COST = 30;
    private const int METAL_COST = 20;

    private const float OXYGEN_DECAY_DELAY_IN_S = 1.5f;

    private int _nbMetal;
    private int _nbCrystal;
    private int _currentUpgradeGoal;
    private bool _needEnergy;

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
        if (MusicManager.instance != null)
        {
            MusicManager.instance.PlayMainTheme();
        }
       
    }

    public void Update()
    {
        EnergyJauge.WarnUser(EnergyJauge.TargetAmount < OXYGEN_COST || _needEnergy);
        OxygenJauge.WarnUser(OxygenJauge.TargetAmount < OXYGEN_MAX_VALUE * 0.25f); // Warn 25%
        EnergyWarning.gameObject.SetActive(_needEnergy);
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
        _needEnergy = false;
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
            _needEnergy = true;
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
            _needEnergy = true;
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
        MetalGlow.enabled = _nbMetal >= Upgrades[_currentUpgradeGoal].NbMetal;
        CrystalGlow.enabled = _nbCrystal >= Upgrades[_currentUpgradeGoal].NbCrystal;

        // If both materials gathered
        UpgradeBtn.interactable = _nbCrystal >= Upgrades[_currentUpgradeGoal].NbCrystal && _nbMetal >= Upgrades[_currentUpgradeGoal].NbMetal;

        MetalGoal.text = _nbMetal + " / " + Upgrades[_currentUpgradeGoal].NbMetal;
        CrystalGoal.text = _nbCrystal + " / " + Upgrades[_currentUpgradeGoal].NbCrystal;
    }

    public void UpgradeSpaceship()
    {
        _nbMetal -= Upgrades[_currentUpgradeGoal].NbMetal;
        _nbCrystal -= Upgrades[_currentUpgradeGoal].NbCrystal;
        _currentUpgradeGoal++;
        if (_currentUpgradeGoal < Upgrades.Length)
        {
            SpawnSpaceshipPart(_currentUpgradeGoal);
            UpdateUpgradeUI();
        }
        else
        {
            EndOfGame();
        }
    }

    private void SpawnSpaceshipPart(int partId)
    {
        Spaceship.GetComponent<Animator>().SetTrigger("SpawnR" + _currentUpgradeGoal);
    }

    public void ToggleTooltip(Machine machine)
    {
        foreach (Tooltip item in Tooltips)
        {
            if(item.machine == machine)
            {
                item.tip.SetActive(!item.tip.activeSelf);
            }
        }
    }

    public void NeedEnergy()
    {
        _needEnergy = true;
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER");
    }

    [ContextMenu("Test")]
    public void EndOfGame()
    {
        StartCoroutine(Win());
        FindObjectOfType<InGameMenu>().DisplayMenu = true;
    }

    private IEnumerator Win()
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime / 4f;
            float lerp =  -0.5f * (Mathf.Cos(Mathf.PI * percent) - 1.0f);
            WinPanel.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, lerp);
            yield return null;
        }
    }

}
