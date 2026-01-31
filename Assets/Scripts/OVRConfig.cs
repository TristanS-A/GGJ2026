using OVR.API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRConfig : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        ConnectionManager.instance.OnNewDeviceFound.AddListener(CheckNewDevice);

        ConnectionManager.instance.Init(DeviceState.BLE_STATE, BLEConnectionStyle.SCAN);
        var devices = ConnectionManager.instance.GetDiscoveredDevices();
    }

    private void CheckNewDevice(CrossPlatformDevice device)
    {
        ConnectionManager.instance.ConnectToDevice(device.nativeDeviceRef);
    }
}
