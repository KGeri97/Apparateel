using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpraySO", menuName = "Scriptable Objects/SpraySO")]
public class SOSpray : ScriptableObject
{
    public string Name;

    public float CostPerPlant;

    public float MaxProtection;
    public float ProtectionDecline; //The amount the protection declines each growth advancement

    public float CropSellPriceReduction;
}
