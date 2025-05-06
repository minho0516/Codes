using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace RPG.SkillSystem
{
    [CreateAssetMenu(fileName = "SkillUpgradeDataSO", menuName = "SO/Skill/SkillUpgradeDataSO")]
    public class SkillPerkUpgradeSO : SkillUpgradeDataSO
    {
        public enum UpgradeFieldType
        {
            Boolean, Integer, Float
        }

        [HideInInspector] public string targetSkill;
        public List<FieldInfo> boolFields = new List<FieldInfo>();
        public List<FieldInfo> intFields = new List<FieldInfo>();
        public List<FieldInfo> floatFields = new List<FieldInfo>();
        public List<FieldInfo> stringFields = new List<FieldInfo>();

        [HideInInspector] public UpgradeFieldType fieldType;
        [HideInInspector] public string selectFieldName; //모든 직렬화는 문자로
        [HideInInspector] public int intValue;
        [HideInInspector] public float floatValue;

        private Type _skillType;
        private FieldInfo _selectedField;

        private void OnEnable()
        {
            GetFieldsFromTargetSkill();
            SetSelectedField();
        }

        public void GetFieldsFromTargetSkill()
        {
            if(string.IsNullOrEmpty(targetSkill))
            {
                Debug.LogWarning("No target skill selected");
                return;
            }

            _skillType = Type.GetType($"RPG.SkillSystem.{targetSkill}");
            if(_skillType== null)
            {
                Debug.Log("No target skill found");
                return;
            }

            FieldInfo[] fields = _skillType.GetFields(BindingFlags.Instance | BindingFlags.Public);

            foreach(FieldInfo field in fields)
            {
                if(field.FieldType == (typeof(bool)))
                {
                    boolFields.Add(field);
                }
                else if (field.FieldType == (typeof(int)))
                {
                    intFields.Add(field);
                }
                else if (field.FieldType == (typeof(float)))
                {
                    floatFields.Add(field);
                }
            }
        }

        private void SetSelectedField()
        {
            _selectedField = _skillType.GetField(selectFieldName);
            Debug.Assert(_selectedField != null, $"selected field is null {selectFieldName}");
        }

        public override void RollbackUpgrade(Skill skill)
        {
            switch (fieldType)
            {
                case UpgradeFieldType.Boolean:
                    _selectedField.SetValue(skill, false);
                    break;
                case UpgradeFieldType.Integer:
                {
                    int oldValue = (int)_selectedField.GetValue(skill);
                    _selectedField.SetValue(skill, oldValue - intValue);
                    break;
                }
                case UpgradeFieldType.Float:
                {
                    float oldValue = (float)_selectedField.GetValue(skill);
                    _selectedField.SetValue(skill, oldValue - floatValue);
                    break;
                }
            }
        }

        public override void UpgradeSkill(Skill skill)
        {
            switch (fieldType)
            {
                case UpgradeFieldType.Boolean:
                    _selectedField.SetValue(skill, false);
                    break;
                case UpgradeFieldType.Integer:
                {
                    int oldValue = (int)_selectedField.GetValue(skill);
                    _selectedField.SetValue(skill, oldValue - intValue);
                    break;
                }
                case UpgradeFieldType.Float:
                {
                    float oldValue = (float)_selectedField.GetValue(skill);
                    _selectedField.SetValue(skill, oldValue - floatValue);
                    break;
                }
            }
        }
    }
}
