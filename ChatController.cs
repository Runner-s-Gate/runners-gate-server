using Godot;
using System;

public class ChatController : Node
{
    [Remote]
    private void SubmitChatMessage(string playerName, string text)
    {
        GD.Print($"Received chat message: ({playerName}) {text}");
        Rpc("ReceiveChatMessage", playerName, text);
    }
}
