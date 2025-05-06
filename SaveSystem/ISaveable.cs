using UnityEngine;

namespace RPG.SaveSystem
{
    public interface ISaveable
    {
        public int SaveID { get; }
        public string GetSaveData();
        public void RestoreData(string data);
    }
}
