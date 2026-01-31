using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeManager : MonoBehaviour
{
    [SerializeField] DialogueBox dialogueBox;
    [SerializeField] List<NPCDialogueData> dialogueDatas;

    NPCDialogueData currentData;
    int currentOrder;

    [SerializeField] float timeBetweenRamblings;
    int rambleIndex;

    private void Start()
    {
        dialogueBox.WriteLine(dialogueDatas[2].orders[0].requestOrder);
    }

    public void SetNewOrder(int NPCIndex, int orderIndex)
    {
        currentData = dialogueDatas[NPCIndex];
        currentOrder = orderIndex;

        dialogueBox.WriteLine(currentData.orders[currentOrder].requestOrder);

        rambleIndex = 0;
        Ramble();
    }

    public void RateOrder(OrderQuality quality)
    {
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

    public void GoodShot()
    {
        dialogueBox.WriteLine(currentData.goodShot[Random.Range(0, currentData.goodShot.Count)]);
    }

    public void BadShot()
    {
        dialogueBox.WriteLine(currentData.badShot[Random.Range(0, currentData.badShot.Count)]);
    }
}
