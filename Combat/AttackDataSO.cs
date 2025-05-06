using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(fileName = "AttackDataSO", menuName = "SO/Combat/AttackDataSO")]
    public class AttackDataSO : ScriptableObject
    {
        public string attackName;
        public Vector2 movement;
        public Vector2 knockbackForce;
        public float damageMultiflier;
        public float damageIncrease;
        public bool isPowerAttack;

        public float cameraShakePower;
        public float cameraShakeDuration;

        private void OnValidate()
        {
            attackName = this.name;
        }
    }
}
