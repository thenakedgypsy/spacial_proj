using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;

public partial class DialogueLine : Node
{
    public string Text { get; }
    public string TriggerEvent { get; }
    public string RequiredTrigger { get; }

    public DialogueLine(string text, string triggerEvent = null, string requiredTrigger = null)
    {
        Text = text;
        TriggerEvent = triggerEvent;
        RequiredTrigger = requiredTrigger;
    }
}
