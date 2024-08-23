using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;

public partial class NPCDialogue : Node
{
    private List<DialogueLine> _dialogueLines = new List<DialogueLine>();
    private int _currentLineIndex = 0;

    // Add this property to get and set the current line index
    public int CurrentLineIndex
    {
        get { return _currentLineIndex; }
        set { _currentLineIndex = value; }
    }

    public void AddLine(string text, string triggerEvent = null, string requiredTrigger = null)
    {
        _dialogueLines.Add(new DialogueLine(text, triggerEvent, requiredTrigger));
        if (!string.IsNullOrEmpty(triggerEvent))
        {
            TriggerManager.Instance.AddTrigger(triggerEvent);
        }
        if (!string.IsNullOrEmpty(requiredTrigger))
        {
            TriggerManager.Instance.AddTrigger(requiredTrigger);
        }
    }

    public DialogueLine GetNextLine()
    {
        if (_dialogueLines.Count == 0)
        {
            return null;
        }

        int initialIndex = _currentLineIndex;
        do
        {
            if (_currentLineIndex >= _dialogueLines.Count)
            {
                UpdateDialogue();
                if (_currentLineIndex == initialIndex)
                {
                    return null;
                }
            }

            var line = _dialogueLines[_currentLineIndex];
            _currentLineIndex++;

            if (string.IsNullOrEmpty(line.RequiredTrigger) || 
                TriggerManager.Instance.IsTriggered(line.RequiredTrigger))
            {
                return line;
            }
        } while (_currentLineIndex != initialIndex);

        return null;
    }

    public void UpdateDialogue()
    {
        string mostRecentTrigger = null;
        int startIndex = 0;

        for (int i = _dialogueLines.Count - 1; i >= 0; i--)
        {
            if (!string.IsNullOrEmpty(_dialogueLines[i].RequiredTrigger) &&
                TriggerManager.Instance.IsTriggered(_dialogueLines[i].RequiredTrigger))
            {
                mostRecentTrigger = _dialogueLines[i].RequiredTrigger;
                break;
            }
        }

        for (int i = 0; i < _dialogueLines.Count; i++)
        {
            if (_dialogueLines[i].RequiredTrigger == mostRecentTrigger)
            {
                startIndex = i;
                break;
            }
        }

        _currentLineIndex = startIndex;
    }

    public void Reset()
    {
        _currentLineIndex = 0;
    }
}


