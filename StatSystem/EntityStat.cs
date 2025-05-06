using System.Collections.Generic;
using System.Linq;
using RPG.Entities;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RPG.StatSystem
{
    public class EntityStat : MonoBehaviour, IEntityComponent
    {
        [Header("Frequently used Stat")]
        [SerializeField] private StatSO _hpStat;
        [SerializeField] private StatSO _moveSpeed;

        [Space]
        [SerializeField] private StatOverride[] _statOverrides;
        private StatSO[] _stats; //this is real stat
        public Entity Owner { get; private set; }
        public StatSO HpStat { get; private set; }
        public StatSO MoveSpeedStat { get; private set; }
        
        public void Initialize(Entity agent)
        {
            Owner = agent;

            //스텟 오버라이드 후 자주쓰는 스탯은 뽑아서 저장해둔다.
            _stats = _statOverrides.Select(x => x.CreateStat()).ToArray();
            HpStat = _hpStat ? GetStat(_hpStat) : null;
            MoveSpeedStat = _moveSpeed ? GetStat(_moveSpeed) : null;
        }

        public StatSO GetStat(StatSO stat)
        {
            //인자가 False면 에러메시지를 보여줌
            Debug.Assert(stat != null, $"Stats::Getstat- stat은 null일 수 없습니다.");
            return _stats.FirstOrDefault(x => x.statName == stat.statName);
        }

        public bool TryGetStat(StatSO stat, out StatSO outStat)
        {
            Debug.Assert(stat != null, $"Stats::TryGetstat- stat은 null일 수 없습니다.");

            outStat = _stats.FirstOrDefault(x => x.statName == stat.statName);
            return outStat != null;
        }


        public void SetBaseValue(StatSO stat, float value)
            => GetStat(stat).BaseValue = value;

        public float GetBaseValue(StatSO stat)
            => GetStat(stat).BaseValue;

        public void IncreaseBaseValue(StatSO stat, float value)
            => GetStat(stat).BaseValue += value;

        public void AddModifier(StatSO stat, string key, float value)
            => GetStat(stat).AddModifier(key, value);
        public void RemoveModifier(StatSO stat, string key)
            => GetStat(stat).RemoveModifier(key);

        public void ClearAllStatModifier()
        {
            foreach (StatSO stat in _stats)
            {
                //stat.ClearModifier();  //다음시간에 만들어요
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Load Override Stat")]
        private void LoadStatAsOverride()
        {
            string path = "Assets/08SO/StatSystem";
            string[] assetNames = AssetDatabase.FindAssets("", new[] { path });

            List<StatSO> loadedStats = new List<StatSO>();
            foreach (string guid in assetNames)
            {
                string soPath = AssetDatabase.GUIDToAssetPath(guid);
                StatSO statSO = AssetDatabase.LoadAssetAtPath<StatSO>(soPath);
                if (statSO != null)
                {
                    loadedStats.Add(statSO);
                }
            }

            _statOverrides = loadedStats.Select(x => new StatOverride(x)).ToArray();
        }
#endif
        
    }
}
