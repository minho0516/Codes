using System.Collections.Generic;
using UnityEngine;

namespace RPG.SkillSystem
{
    public abstract class SkillUpgradeDataSO : ScriptableObject
    {
        public string upgradeName;
        public Sprite upgradeIcon;

        [TextArea]
        public string description;

        public int maxUpgradeCount = 1;

        public List<SkillUpgradeDataSO> needUpgradeList = new List<SkillUpgradeDataSO>();
        public List<SkillUpgradeDataSO> dontNeedUpgradeList = new List<SkillUpgradeDataSO>();

        public abstract void UpgradeSkill(Skill skill);
        public abstract void RollbackUpgrade(Skill skill);
    }
}
