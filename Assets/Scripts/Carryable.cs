using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Carryable : MonoBehaviour {

    public enum PlantType { ENERGY, FOOD, OXYGEN, METAL, CRYSTAL }

    [System.Serializable]
    public struct SeedType
    {
        public PlantType plant;
        public Material material;
    }

    public SeedType Type;
    public bool IsSeed;
}
