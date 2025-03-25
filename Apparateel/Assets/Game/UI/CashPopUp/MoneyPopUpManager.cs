using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPopUpManager : MonoBehaviour
{
    public static MoneyPopUpManager Instance;

    [SerializeField]
    private MoneyPopUpAnimation _popUpPrefab;

    private void Awake() {
        Instance = this;
    }

    public void CreatePopUp(float value) {
        Vector3 spawnLocation = Vector3.Lerp(InputManager.Instance._pointingAtObject.transform.position, Camera.main.transform.position, 0.2f);
        MoneyPopUpAnimation popUp = Instantiate(_popUpPrefab, spawnLocation , Quaternion.identity);
        popUp.SetText(value);
    }

    public void CreatePopUp(float value, Vector3 spawnLocation) {
        //Vector3 spawnLocation = Vector3.Lerp(InputManager.Instance._pointingAtObject.transform.position, Camera.main.transform.position, 0.2f);
        MoneyPopUpAnimation popUp = Instantiate(_popUpPrefab, spawnLocation , Quaternion.identity);
        popUp.SetText(value);
    }
}
