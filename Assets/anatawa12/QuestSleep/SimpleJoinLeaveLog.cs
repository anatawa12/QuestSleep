using System;
using TMPro;
using UdonSharp;
using VRC.SDKBase;

namespace anatawa12.QuestSleep
{
    public class SimpleJoinLeaveLog : UdonSharpBehaviour
    {
        public TextMeshProUGUI text;

        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            if (text != null)
                text.text += $"[{DateTime.Now:T}] [<color=#0f0>Join</color>] {player.displayName}\n";
        }

        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            if (text != null)
                text.text += $"[{DateTime.Now:T}] [<color=#0f0>Leave</color>] {player.displayName}\n";
        }
    }
}
