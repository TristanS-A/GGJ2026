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

    public IngredientManager.Scent OrderScent { get { return orderScent; } }
}

[CreateAssetMenu(menuName = "ScripableObjects/NPC")]
public class NPCDialogueData : ScriptableObject
{
    [SerializeField] public List<OrderDialogue> orders;
    [SerializeField] public List<string> greatShot;
    [SerializeField] public List<string> goodShot;
    [SerializeField] public List<string> midShot;
    [SerializeField] public List<string> badShot;
    [SerializeField] public AudioClip voiceClip;
}
