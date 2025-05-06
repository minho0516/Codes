using UnityEngine;

namespace RPG.EventSystem
{
    public class UIEvents
    {
        public static readonly FadeEvent FadeEvent = new FadeEvent();
        public static readonly FadeCompleteEvent FadeCompleteEvent = new FadeCompleteEvent();
    }

    public class FadeEvent : GameEvent
    {
        public bool isFadeIn;
        public float fadeTime;
    }

    public class FadeCompleteEvent : GameEvent
    {

    }
}
