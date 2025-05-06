using RPG.SkillSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.Editors
{
    [CustomEditor(typeof(SkillPerkUpgradeSO))]
    public class CustomSkillPerkSO : Editor
    {
        [SerializeField] private VisualTreeAsset _customTreeAsset = default;

        private SkillPerkUpgradeSO _skillPerkUpgradeSO;
        private Assembly _skillAssembly;
        private VisualElement _root;
        private DropdownField _fieldList;

        public override VisualElement CreateInspectorGUI()
        {
            _skillPerkUpgradeSO = (SkillPerkUpgradeSO)target;

            _root = new VisualElement();

            //��Ŷ���� �ٷ��� �ʾҴ� �༮���� �⺻���� �׷��ֶ�.
            InspectorElement.FillDefaultInspector(_root, serializedObject, this);

            _customTreeAsset.CloneTree(_root); // Ŀ���� ������ �� �ؿ� �׷��ش�.

            _fieldList = _root.Q<DropdownField>("UpgradeFieldList");
            _fieldList.RegisterValueChangedCallback(HandleFieldChange);

            EnumField fieldType = _root.Q<EnumField>("UpgradeFieldType");
            fieldType.RegisterValueChangedCallback(HandleFieldTypeChange);

            MakeSkillDropdown();

            UpdateFieldList();

            return _root;
        }

        private void MakeSkillDropdown()
        {
            DropdownField typeDropdown = _root.Q<DropdownField>("TypeSelector");

            _skillAssembly = Assembly.GetAssembly(typeof(Skill)); //Skill �̶�� Ŭ������ ��ġ���ִ� ������� ������
            List<Type> derivedTypes = _skillAssembly.GetTypes().Where(type => type.IsClass == false && type.IsSubclassOf(typeof(Skill))).ToList();

            derivedTypes.ForEach(type => typeDropdown.choices.Add(type.Name));

            typeDropdown.RegisterValueChangedCallback(HandleTypeDropdownChange);
            typeDropdown.SetValueWithoutNotify(_skillPerkUpgradeSO.targetSkill);
        }

        private void HandleTypeDropdownChange(ChangeEvent<string> evt)
        {
            _skillPerkUpgradeSO.targetSkill = evt.newValue;
            _skillPerkUpgradeSO.GetFieldsFromTargetSkill();

            UpdateFieldList(); //Ŭ������ �ٲ�鼭�ٽ� �ش� Ŭ�����κ��� ���÷����� ���� �ʵ带 �ҷ��;���
        }

        private void HandleFieldTypeChange(ChangeEvent<Enum> evt)
        {
            UpdateFieldList();

            IntegerField intField = _root.Q<IntegerField>("InteagerValue");
            FloatField floatField = _root.Q<FloatField>("FloatValue");

            switch(evt.newValue)
            {
                case SkillPerkUpgradeSO.UpgradeFieldType.Boolean:
                    intField.style.display = DisplayStyle.None;
                    floatField.style.display = DisplayStyle.None;
                    break;
                case SkillPerkUpgradeSO.UpgradeFieldType.Integer:
                    intField.style.display = DisplayStyle.Flex;
                    floatField.style.display = DisplayStyle.None;
                    break;
                case SkillPerkUpgradeSO.UpgradeFieldType.Float:
                    intField.style.display = DisplayStyle.None;
                    floatField.style.display = DisplayStyle.Flex;
                    break;
            }
        }

        private void HandleFieldChange(ChangeEvent<string> evt)
        {
            _skillPerkUpgradeSO.selectFieldName = evt.newValue;
        }

        private void UpdateFieldList()
        {
            _fieldList.choices = _skillPerkUpgradeSO.fieldType switch
            {
                SkillPerkUpgradeSO.UpgradeFieldType.Boolean => _skillPerkUpgradeSO.boolFields.Select(fInfo => fInfo.Name).ToList(),
                SkillPerkUpgradeSO.UpgradeFieldType.Integer => _skillPerkUpgradeSO.intFields.Select(fInfo => fInfo.Name).ToList(),
                SkillPerkUpgradeSO.UpgradeFieldType.Float => _skillPerkUpgradeSO.floatFields.Select(fInfo => fInfo.Name).ToList(),
                _ => _fieldList.choices //_ �� ����Ʈ�� ��
            };

            if (_fieldList.choices.Count > 0 && _fieldList.choices.Contains(_skillPerkUpgradeSO.selectFieldName) == false)
                _fieldList.value = _fieldList.choices.First();
        }
    }
}
