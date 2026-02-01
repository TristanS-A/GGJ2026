using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private NPCDialogueData[] mCustomers;

    private Dictionary<int, int> mPrevOrders = new();

    private int mCurrCustomerIndex = 0;
    private int mPrevCustomerIndex = 0;
    private int mPrevPrevCustomerIndex = 0;

    private GameObject mCurrCustomer;

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
        StartCoroutine(Co_HandleNextCustomer(2));
    }

    private IEnumerator Co_HandleNextCustomer(float delay)
    {
        if (mCurrCustomer != null)
        {
            mCurrCustomer.GetComponentInChildren<CharacterHead>().StartSlide(false);
        }

        yield return new WaitForSeconds(delay);

        mCurrCustomerIndex = UnityEngine.Random.Range(0, mCustomers.Length);

        while (mCurrCustomerIndex == mPrevCustomerIndex || mCurrCustomerIndex == mPrevPrevCustomerIndex)
        {
            mCurrCustomerIndex++;

            if (mCurrCustomerIndex == mCustomers.Length)
            {
                mCurrCustomerIndex = 0;
            }
        }

        mPrevPrevCustomerIndex = mPrevCustomerIndex;
        mPrevCustomerIndex = mCurrCustomerIndex;

        int orderIndex = UnityEngine.Random.Range(0, mCustomers[mCurrCustomerIndex].orders.Count);

        if (mPrevOrders.ContainsKey(mCurrCustomerIndex))
        {
            if (mPrevOrders[mCurrCustomerIndex] == orderIndex)
            {
                orderIndex++;
                if (orderIndex == mCustomers[mCurrCustomerIndex].orders.Count)
                {
                    orderIndex = 0;
                }
            }
            else
            {
                mPrevOrders.Add(mCurrCustomerIndex, orderIndex);
            }
        }

        if (mCustomers[mCurrCustomerIndex].characterOBJ)
        {
            mCurrCustomer = Instantiate(mCustomers[mCurrCustomerIndex].characterOBJ);
        }

        Debug.Log(mCurrCustomerIndex);
        EventSystem.StartNextOrder(mCustomers[mCurrCustomerIndex], orderIndex);

        IngredientManager.Instance.SetTargetScent(mCustomers[mCurrCustomerIndex].orders[orderIndex].OrderScent);
        Debug.Log(mCustomers[mCurrCustomerIndex].orders[orderIndex].OrderScent);

        EventSystem.SlideOutUI(UISlideOut.UIType.INGREDIENTS, true);
        EventSystem.SlideOutUI(UISlideOut.UIType.TRAY, true);
    }

    private void Start()
    {
        StartCoroutine(Co_HandleNextCustomer(0));
    }
}
