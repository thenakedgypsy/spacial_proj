using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;

public partial class DialogueManager : Node
{

    public static DialogueManager Instance;
    private Dictionary<string, NPCDialogue> _npcDialogues;
    private Dictionary<string, int> _dialogueStates; // Add this to store dialogue states

    public override void _Ready()
    {
        _npcDialogues = new Dictionary<string, NPCDialogue>();
        _dialogueStates = new Dictionary<string, int>(); // Initialize the dialogue states
        Instance = this;
    }

    public void AddNPCDialogue(string npcId, NPCDialogue dialogue)
    {
        _npcDialogues[npcId] = dialogue;
    }

    public NPCDialogue GetNPCDialogue(string npcID)
    {
        if (_npcDialogues.TryGetValue(npcID, out var dialogue))
        {
            // Restore the dialogue state if it exists
            if (_dialogueStates.TryGetValue(npcID, out int lineIndex))
            {
                dialogue.CurrentLineIndex = lineIndex;
            }
            return dialogue;
        }
        return null;
    }

    // Add this method to save dialogue states
    public void SaveDialogueStates()
    {
        foreach (var kvp in _npcDialogues)
        {
            _dialogueStates[kvp.Key] = kvp.Value.CurrentLineIndex;
        }
    }

    // Add this method to restore dialogue states
    public void RestoreDialogueStates()
    {
        foreach (var kvp in _dialogueStates)
        {
            if (_npcDialogues.TryGetValue(kvp.Key, out var dialogue))
            {
                dialogue.CurrentLineIndex = kvp.Value;
            }
        }
    }


}
