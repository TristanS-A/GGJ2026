using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeManager : MonoBehaviour
{
    [SerializeField] DialogueBox dialogueBox;
    //[SerializeField] List<NPCDialogueData> dialogueDatas;

    NPCDialogueData currentData;
    int currentOrder;

    [SerializeField] float timeBetweenRamblings;
    int rambleIndex;

    private void OnEnable()
    {
        EventSystem.OnStartNextOrder += SetNewOrder;
        EventSystem.OnRateOrder += RateOrder;
        EventSystem.OnRateShot += RateShot;

        //dialogueBox.WriteLine(dialogueDatas[2].orders[0].requestOrder, dialogueDatas[2].voiceClip);
    }

    private void OnDisable()
    {
        EventSystem.OnStartNextOrder -= SetNewOrder;
        EventSystem.OnRateOrder -= RateOrder;
        EventSystem.OnRateShot -= RateShot;
    }

    private void Start()
    {
        //dialogueBox.WriteLine(dialogueDatas[2].orders[0].requestOrder);
    }

    public void SetNewOrder(NPCDialogueData currCustomer, int orderIndex)
    {
        currentData = currCustomer;
        currentOrder = orderIndex;

        dialogueBox.WriteLine(currentData.orders[currentOrder].requestOrder, currentData.voiceClip);

        rambleIndex = 0;
        Ramble();
    }

    public void RateOrder(OrderQuality quality)
    {
        string text = "";
        switch (quality)
        {
            case OrderQuality.Bad:
                dialogueBox.WriteLine(currentData.orders[currentOrder].rateOrderBad);
                break;
            case OrderQuality.Mid:
                dialogueBox.WriteLine(currentData.orders[currentOrder].rateOrderMid);
                break;
            case OrderQuality.Good:
                dialogueBox.WriteLine(currentData.orders[currentOrder].rateOrderGood);
                break;
            case OrderQuality.Peak:
                dialogueBox.WriteLine(currentData.orders[currentOrder].rateOrderPeak);
                break;
        }
        //dialogueBox.WriteLine(text, currentData.voiceClip);
    }

    IEnumerator Ramble()
    {
        yield return new WaitForSeconds(timeBetweenRamblings);

        dialogueBox.WriteLine(currentData.orders[currentOrder].ramblings[rambleIndex]);

        rambleIndex++;
        if (rambleIndex < currentData.orders[currentOrder].ramblings.Count)
        {
            Ramble();
        }
        else
        {
            Debug.Log("OUT OF RAMBLES. not feeling chatty :(");
        }

    }

    public void RateShot(bool goodShot)
    {
        if (goodShot)
        {
            dialogueBox.WriteLine(currentData.goodShot[Random.Range(0, currentData.goodShot.Count)]);
        }
        else
        {
            dialogueBox.WriteLine(currentData.badShot[Random.Range(0, currentData.badShot.Count)]);
        }
    }
}
