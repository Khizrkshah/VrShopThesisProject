using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{

    private InputDevice rightHandDevice;
    private InputDevice leftHandDevice;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        var inputDevices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        //InputDeviceCharacteristics leftControlllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;

        Debug.Log("code is running.");
       
        while (inputDevices.Count == 0)
        {
            // check for devices every frame until you find one.
            yield return null;
            InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, inputDevices);
            //InputDevices.GetDevicesWithCharacteristics(leftControlllerCharacteristics, inputDevices);
        }

        foreach (var item in inputDevices)
        {
            //   Debug.Log(item.name + item.characteristics);
            Debug.Log(string.Format(item.name + item.characteristics));

        }

        if (inputDevices.Count > 0)
        {
            rightHandDevice = inputDevices[0];
            //leftHandDevice = inputDevices[1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (rightHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
        {
            Debug.Log("Pressing Right primary button");
        }

        
        if(rightHandDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
        {
            Debug.Log("Right Trigger pressed" + triggerValue);
        }

        

        if(rightHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) && primary2DAxisValue != Vector2.zero)
        {
            Debug.Log("Right Analog used!");
        }

        if (leftHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool leftprimaryButtonValue) && leftprimaryButtonValue)
        {
            Debug.Log("Left Primary button clicked!");
        }

    }
}
