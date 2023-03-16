using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.XR;

public class UserManagerSystem : MonoBehaviour
{
    [SerializeField] GameObject[] rayInteractors = new GameObject[2];
    private InputDevice targetDevice;
    public InputDeviceCharacteristics controllerCharacteristics;

    // Start is called before the first frame update
    void Start()
    {
        tryInitialize();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetDevice.isValid)
        {
            tryInitialize();
        }
        else
        {
            targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool activated);
            if (activated)
            {
                disableRays();
            }
            
            
        }
    }

    public void disableRays()
    {
        for (int i = 0; i < rayInteractors.Length; i++)
        {
            if (rayInteractors[i].activeInHierarchy)
            {
                rayInteractors[i].SetActive(false);
            }
            else
            {
                rayInteractors[i].SetActive(true);
            }
        }
    }

    void tryInitialize()
    {
        var inputDevices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, inputDevices);


        foreach (var item in inputDevices)
        {
            Debug.Log(string.Format("usermanager" + item.name + item.characteristics));

        }

        if (inputDevices.Count > 0)
        {
            targetDevice = inputDevices[0];
          


        }
    }

}
