using UnityEngine;

namespace RPG.EventSystem
{
    public class SystemEvents
    {
        public static readonly SaveGameEvent SaveGame = new SaveGameEvent();
        public static readonly LoadGameEvent LoadGame = new LoadGameEvent();
    }

    public class SaveGameEvent : GameEvent
    {
        public bool isSaveToFile;
    }

    public class LoadGameEvent : GameEvent
    {
        public bool isLoadFromFile;
    }
}
