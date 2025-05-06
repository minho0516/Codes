using UnityEngine;

namespace RPG
{
    public enum SkillType
    {
        Default, Passive, Active
    }
    [CreateAssetMenu(fileName = "SkillDataSO", menuName = "SO/Skill/SkillDataSO")]
    public class SkillDataSO : ScriptableObject
    {
        public SkillType skillType = SkillType.Default;
        public Sprite skillIcon;
    }
}
