using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristics;
    public GameObject handModelPrefab;
   
    private InputDevice targetDevice;
    private GameObject spawnedHandModel;
    private Animator handAnimator;


    // Start is called before the first frame update
     void Start()
    {
        Debug.Log("code is running.");
        tryInitialize();
    }

    void tryInitialize()
    {
        var inputDevices = new List<InputDevice>();
       
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, inputDevices);
       

        /*foreach (var item in inputDevices)
        {
           Debug.Log(string.Format(item.name + item.characteristics));

        }*/

        if (inputDevices.Count > 0)
        {
            targetDevice = inputDevices[0];
            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();


        }
    }
    void updateHandAnimation()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (!targetDevice.isValid)
        {
            tryInitialize();
        } else
        {
            updateHandAnimation();
        }

        

    }
}
