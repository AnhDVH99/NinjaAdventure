using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    public static event Action<InteractionType> OnExtraInteractionType;
    
    [Header("Config")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Image npcIcon;
    [SerializeField] private TextMeshProUGUI npcNameTMP;
    [SerializeField] private TextMeshProUGUI npcDialogueTMP;

    public NPCInteraction NpcSelected { get; set; }

    private bool dialogueStarted;
    private PlayerActions actions;
    private Queue<string> dialogueQueue = new Queue<string>();

    protected override void Awake()
    {
        base.Awake();
        actions = new PlayerActions();
    }

    private void Start()
    {
        actions.Dialogue.Interact.performed += ctx => ShowDialogue();
        actions.Dialogue.Interact.performed += ctx => ContinueDialogue();
    }

    private void LoadDiaLogueFromNPC()
    {
        if (NpcSelected.DialogToShow.Dialogues.Length <= 0) return;
        foreach (string sentence in NpcSelected.DialogToShow.Dialogues)
        {
            dialogueQueue.Enqueue(sentence);
        }
    }
    private void ShowDialogue()
    {
        if(NpcSelected == null || dialogueStarted ) return;
        dialoguePanel.SetActive(true);
        LoadDiaLogueFromNPC();
        npcIcon.sprite = NpcSelected.DialogToShow.Icon;
        npcNameTMP.text = NpcSelected.DialogToShow.Name;
        npcDialogueTMP.text = NpcSelected.DialogToShow.Greeting;
        dialogueStarted = true;
    }

    private void ContinueDialogue()
    {
        if (NpcSelected == null)
        {
            dialogueQueue.Clear();
            return;
        }

        if (dialogueQueue.Count <= 0)
        {
            dialoguePanel.SetActive(false);
            dialogueStarted = false;
            if (NpcSelected.DialogToShow.HasInteraction)
            {
                OnExtraInteractionType?.Invoke(NpcSelected.DialogToShow.InteractionType);
            }
            return;
        }

        npcDialogueTMP.text = dialogueQueue.Dequeue();
    }
    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }
}
