using RPG.EventSystem;
using Unity.Cinemachine;
using UnityEngine;

namespace RPG
{
    public static class CameraEvents
    {
        public static PanEvent panEvent = new PanEvent();
        public static SwapCameraEvnet SwapCameraEvent = new SwapCameraEvnet();
    }
    public class PanEvent : GameEvent
    {
        public float distance;
        public float penTime;
        public PanDirection direction;
        public bool isRewindToStart;
    }
    public class SwapCameraEvnet : GameEvent
    {
        public CinemachineCamera leftCamera;
        public CinemachineCamera rightCamera;
        public Vector2 direction;
    }
}
