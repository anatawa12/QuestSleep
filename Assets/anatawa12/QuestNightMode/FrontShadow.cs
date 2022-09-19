
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class FrontShadow : UdonSharpBehaviour
{
    private void Update()
    {
        if (Networking.LocalPlayer.IsValid())
        {
            VRCPlayerApi.TrackingData data = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head);
            var t = gameObject.transform;
            t.position = data.position;
            t.rotation = data.rotation;
        }
    }
}
