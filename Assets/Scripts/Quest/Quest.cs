using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(fileName = "Quest_")]
public class Quest : ScriptableObject
{
    [Header(("Config"))] 
    public string ID;
    public string Name;
    public int QuestGoal;

    [Header(("Description"))] [TextArea] public string QuestDescription;

    [Header("Reward")] public int GoldReward;
    public int ExpReward;
    public ItemReward ItemReward;

    [HideInInspector] public int CurrentStatus;
    [HideInInspector] public bool QuestCompleted;
    [HideInInspector] public bool QuestAccepted;

    public void AddProgress(int amount)
    {
        CurrentStatus += amount;
        if (CurrentStatus >= QuestGoal)
        {
            CurrentStatus = QuestGoal;
            QuestIsCompleted();
        }
    }

    private void QuestIsCompleted()
    {
        if (QuestCompleted) return;
        QuestCompleted = true;
    }

    public void ResetQuest()
    {
        QuestAccepted = false;
        QuestCompleted = false;
        CurrentStatus = 0;
    }
}

[Serializable]
public class ItemReward
{ 
    public InventoryItem Item;
    public int Quantity;
}