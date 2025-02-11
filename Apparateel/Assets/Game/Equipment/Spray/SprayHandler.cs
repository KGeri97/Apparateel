using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apparateel.Equipment;
using Apparateel.Crop;

public static class SprayHandler
{
    //public static void Spray(SOSprayEquipment usedEquipment, SOSpray sprayData, InputManager.OnClickEventArgs e) {
    //    //No crop was selected
    //    if ((usedEquipment.SprayType == SprayType.Singular || usedEquipment.SprayType == SprayType.Row)
    //        && e.ClickedType != ClickableType.Crop)
    //        return;

    //    Crop crop = e.ClickedObject.GetComponentInParent<Crop>();

    //    switch (usedEquipment.SprayType) {
    //        case SprayType.Singular:
    //            SpraySingular(crop, sprayData);
    //            break;
    //        case SprayType.Row:
    //            SprayRow(crop, sprayData);
    //            break;
    //    }
    //}

    //private static void SpraySingular(Crop crop, SOSpray sprayData) {
    //    if (!MoneyManager.Instance.ItemPurchased(sprayData.CostPerPlant))
    //        return;

    //    crop.CropInfection.Spray(sprayData);
    //}

    //private static void SprayRow(Crop crop, SOSpray sprayData) {
    //    //List<DirtMound> dirtMoundsToSpray = new();
    //    //foreach (List<DirtMound> row in Parcel.Instance.DirtMounds) {
    //    //    if (!row.Contains(crop.Mound)) {
    //    //        continue;
    //    //    }

    //    //    dirtMoundsToSpray = row;
    //    //}

    //    //if (dirtMoundsToSpray.Count == 0) {
    //    //    Debug.LogError("The selected crop's dirtmound exists outside the parcel. Aborting spraying.", crop);
    //    //    return;
    //    //}

    //    //foreach (DirtMound dirtMound in dirtMoundsToSpray) {
    //    //    if (!dirtMound.OccupyingCrop)
    //    //        continue;

    //    //    SpraySingular(dirtMound.OccupyingCrop, sprayData);
    //    //}



    //}

    public static void Spray(SOSpray sprayData) {
        foreach (Crop crop in SprayManager.Instance.HighlightedCrops) {
            if (!MoneyManager.Instance.ItemPurchased(sprayData.CostPerPlant))
                return;

            crop.CropInfection.Spray(sprayData);
        }
    }
}
