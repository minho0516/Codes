using UnityEngine;

namespace RPG.EventSystem
{
    public class PlayerEvents
    {
        public static readonly AddExpEvent AddExp = new AddExpEvent();
        public static readonly FreezePlayerEvent FreezePlayerEvent = new FreezePlayerEvent();
    }

    public class AddExpEvent : GameEvent
    {
        public int exp;
    }

    public class FreezePlayerEvent : GameEvent
    {

    }
}
