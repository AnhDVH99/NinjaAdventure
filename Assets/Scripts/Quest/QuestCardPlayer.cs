using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestCardPlayer : QuestCard
{
    [Header("Config")] [SerializeField] private TextMeshProUGUI statusTMP;
    [SerializeField] private TextMeshProUGUI goldRewardTMP;
    [SerializeField] private TextMeshProUGUI expReward;

    [Header("Item")] [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemQuantityTMP;

    [Header("Quest Completed")] 
    [SerializeField] private GameObject claimButton;
    [SerializeField] private GameObject rewardPanel;
    


    private void Update()
    {
        if (QuestToComplete == null) return;
        if (QuestToComplete.QuestAccepted)
        {
            statusTMP.text = $"Status\n{QuestToComplete.CurrentStatus}/{QuestToComplete.QuestGoal}";
        }
        QuestCompletedCheck();
    }

    public override void ConfigQuestUI(Quest quest)
    {
        base.ConfigQuestUI(quest);
        statusTMP.text = $"Status\n{quest.CurrentStatus}/{quest.QuestGoal}";
        goldRewardTMP.text = quest.GoldReward.ToString();
        expReward.text = quest.ExpReward.ToString();

        itemIcon.sprite = quest.ItemReward.Item.Icon;
        itemQuantityTMP.text = quest.ItemReward.Quantity.ToString();
    }

    public void ClaimQuest()
    {
        GameManager.Instance.AddPlayerExp(QuestToComplete.ExpReward);
        Inventory.Instance.AddItem(QuestToComplete.ItemReward.Item, QuestToComplete.ItemReward.Quantity);
        CoinManager.Instance.AddCoins(QuestToComplete.GoldReward);
        gameObject.SetActive(false);
    }
    private void QuestCompletedCheck()
    {
        if (QuestToComplete.QuestCompleted)
        {
            claimButton.SetActive(true);
            rewardPanel.SetActive(false);
        }
    }
    
}