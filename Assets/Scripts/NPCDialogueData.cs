using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OrderQuality { Bad, Mid, Good, Peak }

[System.Serializable]
public class OrderDialogue
{
    [SerializeField] IngredientManager.Scent orderScent;

    [SerializeField] public string requestOrder;
    [SerializeField] public List<string> ramblings;
    [SerializeField] public string rateOrderBad;
    [SerializeField] public string rateOrderMid;
    [SerializeField] public string rateOrderGood;
    [SerializeField] public string rateOrderPeak;
}

[CreateAssetMenu(menuName = "ScripableObjects/NPC")]
public class NPCDialogueData : ScriptableObject
{
    [SerializeField] public List<OrderDialogue> orders;
    [SerializeField] public List<string> goodShot;
    [SerializeField] public List<string> badShot;
}
