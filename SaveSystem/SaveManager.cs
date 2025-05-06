using RPG.EventSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace RPG.SaveSystem
{
    [Serializable]
    public struct DataCollection
    {
        public List<SaveData> dataCollection;
    }

    [Serializable]
    public struct SaveData
    {
        public int ID;
        public string Data;
    }

    public class SaveManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO _systemEventChannel;
        [SerializeField] private string _saveDataKey = "savedData";
        private List<SaveData> _unUsedData = new List<SaveData>();

        private void Awake()
        {
            _systemEventChannel.AddListner<SaveGameEvent>(HandleSaveGameEvent);
            _systemEventChannel.AddListner<LoadGameEvent>(HandleLoadGameEvent);
        }

        private void OnDestroy()
        {
            _systemEventChannel.RemoveListner<SaveGameEvent>(HandleSaveGameEvent);
            _systemEventChannel.RemoveListner<LoadGameEvent>(HandleLoadGameEvent);
        }

        #region Load section

        private void HandleLoadGameEvent(LoadGameEvent evt)
        {
            if (evt.isLoadFromFile == false)
                LoadFromPrefs();
        }

        private void LoadFromPrefs()
        {
            string loadJson = PlayerPrefs.GetString(_saveDataKey);
            RestoreData(loadJson);
        }


        private void RestoreData(string loadJson)
        {
            IEnumerable<ISaveable> saveables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<ISaveable>();

            DataCollection loadData = string.IsNullOrEmpty(loadJson)
                ? new DataCollection() : JsonUtility.FromJson<DataCollection>(loadJson);

            _unUsedData.Clear();

            if(loadData.dataCollection != null)
            {
                foreach(SaveData saveData in loadData.dataCollection)
                {
                    ISaveable target = saveables.FirstOrDefault(saveable => saveable.SaveID == saveData.ID);

                    if (target != null)
                        target.RestoreData(saveData.Data);
                    else
                        _unUsedData.Add(saveData); //현재 씬에 없다면 버리지말고 넣어둔다 
                }
            }
        }
        #endregion

        #region save section

        private void HandleSaveGameEvent(SaveGameEvent evt)
        {
            if (evt.isSaveToFile == false)
                SaveGameToPrefs();
            //파일저장은 구현안한다.
        }

        private void SaveGameToPrefs()
        {
            string dataJson = GetDataToSave();
            PlayerPrefs.SetString(_saveDataKey, dataJson);
            Debug.Log(dataJson);
        }

        private string GetDataToSave()
        {
            IEnumerable<ISaveable> savables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<ISaveable>();

            List<SaveData> saveDataList = new List<SaveData>();
            foreach (ISaveable savable in savables)
            {
                saveDataList.Add(new SaveData { ID = savable.SaveID, Data = savable.GetSaveData() });
            }

            saveDataList.AddRange(_unUsedData);
            DataCollection dataCollection = new DataCollection { dataCollection = saveDataList };

            return JsonUtility.ToJson(dataCollection);
        }

        #endregion

        #region DebugRegion
        [ContextMenu("Clear prefs data")]
        private void ClearPrefs()
        {
            PlayerPrefs.DeleteKey(_saveDataKey);
        }
        #endregion
    }
}
