using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public Jauge EnergyJauge;
    public Jauge OxygenJauge;

    private const int ENERGY_MAX_VALUE = 200;
    private const int OXYGEN_MAX_VALUE = 100;

    private void Start()
    {
        EnergyJauge.MaxValue = ENERGY_MAX_VALUE;
        EnergyJauge.TargetAmount = 0;
        OxygenJauge.MaxValue = OXYGEN_MAX_VALUE;
        OxygenJauge.TargetAmount = OXYGEN_MAX_VALUE;
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

    public void AddOxygen(int quantity)
    {
        if (OxygenJauge.TargetAmount + quantity > OXYGEN_MAX_VALUE)
        {
            OxygenJauge.TargetAmount = OXYGEN_MAX_VALUE;
        }
        else
        {
            OxygenJauge.TargetAmount += quantity;
        }
    }
}
