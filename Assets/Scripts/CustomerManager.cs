using System;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private NPCDialogueData[] mCustomers;

    private int mCurrCustomerIndex = 0;
    private int mPrevCustomerIndex = 0;

    private void OnEnable()
    {
        EventSystem.OnTriggerNextCustomer += StartNextOrder;
    }

    private void OnDisable()
    {
        EventSystem.OnTriggerNextCustomer -= StartNextOrder;
    }

    private void StartNextOrder()
    {
        int customerIndex = UnityEngine.Random.Range(0, mCustomers.Length);

        if (mCurrCustomerIndex == mPrevCustomerIndex)
        {
            mCurrCustomerIndex++;

            if (mCurrCustomerIndex == mCustomers.Length)
            {
                mCurrCustomerIndex = 0;
            }
        }

        int orderIndex = UnityEngine.Random.Range(0, mCustomers[mCurrCustomerIndex].orders.Count);
        EventSystem.StartNextOrder(mCustomers[mCurrCustomerIndex], orderIndex);

        EventSystem.SlideOutUI(UISlideOut.UIType.INGREDIENTS, true);
        EventSystem.SlideOutUI(UISlideOut.UIType.TRAY, true);
    }

    private void Start()
    {
        StartNextOrder();
    }
}
