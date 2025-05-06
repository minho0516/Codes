using RPG.EventSystem;
using RPG.SaveSystem;
using System;
using UnityEngine;

namespace RPG.Players
{
    public class PlayerData : MonoBehaviour, ISaveable
    {
        [SerializeField] private GameEventChannelSO _playerChannel;

        public int currentExp;
        public int level;
        public int skillpoints;
        public int gold;

        public int SaveID => SaveIDRepository.PLAYER_DATA_ID;
        [Serializable]
        public struct PlayerSaveData
        {
            public int currentExp;
            public int level;
            public int skillPoints;
            public int gold;
        }

        private void Awake()
        {
            _playerChannel.AddListner<AddExpEvent>(HandleAddExp);
        }

        private void OnDestroy()
        {
            _playerChannel.RemoveListner<AddExpEvent>(HandleAddExp);
        }

        public void HandleAddExp(AddExpEvent evt) => AddExp(evt.exp);

        public void AddExp(int amount)
        {
            //레벨업 등의 로직은 나중에 여기에서 해줘야돼
            currentExp += amount;
        }

        public string GetSaveData()
        {
            PlayerSaveData saveData = new PlayerSaveData()
            {
                currentExp = currentExp,
                level = level,
                skillPoints = skillpoints,
                gold = gold
            };
            return JsonUtility.ToJson(saveData);
        }

        public void RestoreData(string data)
        {
            PlayerSaveData lodedData = JsonUtility.FromJson<PlayerSaveData>(data);
            currentExp = lodedData.currentExp;
            level = lodedData.level;
            skillpoints = lodedData.skillPoints;
            gold = lodedData.gold;
        }
    }
}
