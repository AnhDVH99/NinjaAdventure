using System;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Config")] 
    [SerializeField] private Quest[] quests;

    [Header("NPC Quest Panel")]
    [SerializeField] private QuestCardNPC npcQuestPrefab;
    [SerializeField] private Transform npcQuestContainer;

    [Header("Player Quest Panel")] 
    [SerializeField] private QuestCardPlayer questCardPlayerPrefab;
    [SerializeField] private Transform playerQuestContainer;
    
    
    private void Start()
    {
        LoadQuestIntoNpcPanel();
    }

    public void AcceptQuest(Quest quest)
    {
        QuestCardPlayer cardPlayer = Instantiate(questCardPlayerPrefab, playerQuestContainer);
        cardPlayer.ConfigQuestUI(quest);
    }

    public void AddProgress(string questID, int amount)
    {
        Quest questProgress = questExist(questID);
        if(questProgress == null) return;
        if (questProgress.QuestAccepted)
        {
            questProgress.AddProgress(amount);
        }
    }
    
    public Quest questExist(string questID)
    {
        foreach (Quest quest in quests)
        {
            if (quest.ID == questID)
            {
                return quest;
            }
        }

        return null;
    }
    public void LoadQuestIntoNpcPanel()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            QuestCard npcQuestCard = Instantiate(npcQuestPrefab, npcQuestContainer);
            npcQuestCard.ConfigQuestUI(quests[i]);
        }
    }
    
    private void OnEnable()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            quests[i].ResetQuest();
        }
    }
}