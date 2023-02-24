using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{

    private UnityEngine.XR.InputDevice targetDevice;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics,inputDevices);

        Debug.Log("code is running.");
       
        while (inputDevices.Count == 0)
        {
            // check for devices every frame until you find one.
            yield return null;
            InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, inputDevices);
        }

        foreach (var item in inputDevices)
        {
            //   Debug.Log(item.name + item.characteristics);
            Debug.Log(string.Format("Device found with name '{0}'", item.name));

        }

        if (inputDevices.Count > 0)
        {
            targetDevice = inputDevices[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        if (primaryButtonValue)
        {
            Debug.Log("Pressing primary button");
        }

        targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if(triggerValue > 0.1f)
        {
            Debug.Log("Trigger pressed" + triggerValue);
        }

        targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue);

        if(primary2DAxisValue != Vector2.zero)
        {
            Debug.Log("Analog used!");
        }
    }
}
