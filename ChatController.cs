using Godot;
using System;

public class ChatController : Node
{
    [Master]
    private void SubmitChatMessage(string playerName, string text)
    {
        GD.Print($"Received chat message: ({playerName}) {text}");
        Rpc("ReceiveChatMessage", playerName, text);
    }
}
