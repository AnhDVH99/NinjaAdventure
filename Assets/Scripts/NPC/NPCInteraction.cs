using System;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private NPC_Dialog dialogueToShow;
    [SerializeField] private GameObject interactionBox;

    public NPC_Dialog DialogToShow => dialogueToShow;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueManager.Instance.NpcSelected = this;
            interactionBox.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueManager.Instance.NpcSelected = null;
            interactionBox.SetActive(false);
        }
    }
}