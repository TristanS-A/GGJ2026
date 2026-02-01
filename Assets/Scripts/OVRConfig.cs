using OVR.API;
using OVR.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRConfig : MonoBehaviour
{
    [SerializeField] private List<ScentToScentData> mScentToOdorant = new();
    //[SerializeField] private Odorant mOderantSpray;
    private Dictionary<IngredientManager.Scent, Odorant> mScentToOdorantMap = new();
    Coroutine mCurrCoroutine = null;
    private IngredientManager.Scent mCurrScent = IngredientManager.Scent.Machina;

    [Serializable]
    struct ScentToScentData
    {
        public IngredientManager.Scent scent;
        public Odorant oderant;
    }

    void Awake()
    {
        ConnectionManager.instance.OnNewDeviceFound.AddListener(CheckNewDevice);
        //ConnectionManager.instance.OnDeviceConnected.AddListener(ConnectedToOmara);

        ConnectionManager.instance.Init(DeviceState.BLE_STATE, BLEConnectionStyle.SCAN);
        var devices = ConnectionManager.instance.GetDiscoveredDevices();

        BuildMap();
    }

    private void BuildMap()
    {
        for (int i = 0; i < mScentToOdorant.Count; i++) 
        {
            mScentToOdorantMap.Add(mScentToOdorant[i].scent, mScentToOdorant[i].oderant);
        }
    }

    private void CheckNewDevice(CrossPlatformDevice device)
    {
        ConnectionManager.instance.ConnectToDevice(device.nativeDeviceRef);
    }

    private void ConnectedToOmara()
    {
        EventSystem.SlideOutUI(UISlideOut.UIType.OMARA_POP_UP, true);

        if (mCurrCoroutine != null)
        {
            StopCoroutine(mCurrCoroutine);
        }

        mCurrCoroutine = StartCoroutine(Co_SlidePopUpBack());
    }

    private IEnumerator Co_SlidePopUpBack()
    {
        yield return new WaitForSeconds(3);
        EventSystem.SlideOutUI(UISlideOut.UIType.OMARA_POP_UP, false);
    }

    public void StartScent(IngredientManager.Scent newScent)
    {
        if (mScentToOdorantMap[mCurrScent].enabled)
        {
            mScentToOdorantMap[mCurrScent].enabled = false;
        }

        mScentToOdorantMap[newScent].enabled = true;
        mCurrScent = newScent;
    }

    public void StopScent()
    {
        mScentToOdorantMap[mCurrScent].enabled = false;
    }
}
