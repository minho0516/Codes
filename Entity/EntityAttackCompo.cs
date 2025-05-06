using UnityEngine;
using RPG.Entities;
using RPG.StatSystem;
using RPG.Animators;
using System;
using RPG.Combat;
using System.Collections.Generic;

namespace RPG.Entities
{
    public class EntityAttackCompo : MonoBehaviour, IEntityComponent, IAfterInitable
    {
        [SerializeField] private StatSO _attackSpeedStat, _damageStat;
        [SerializeField] private AnimParamSO _atkSpeedParam;
        [SerializeField] private DamageCaster _damageCaster;
        [SerializeField] private List<AttackDataSO> _attackDatas;

        private Dictionary<string, AttackDataSO> _atkDictionary;

        private Entity _entity;
        private EntityStat _statCompo;
        private EntityRenderer _renderer;
        private EntityMover _mover;
        private PlayerAnimatorTrigger _animTrigger;

        private bool _canJumpAttack = true;
        private AttackDataSO _currentAttackData;

        [Header("Counter settings")]
        public float counterWindoSize;
        public AnimParamSO sucessCounterParam;
        public LayerMask _whatIsCounterable;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo = entity.GetCompo<EntityStat>();
            _renderer = entity.GetCompo<EntityRenderer>();
            _mover = entity.GetCompo<EntityMover>();
            _animTrigger = entity.GetCompo<PlayerAnimatorTrigger>();

            _damageCaster.InitCaster(entity);

            _atkDictionary = new Dictionary<string, AttackDataSO>();
            _attackDatas.ForEach(data => _atkDictionary.Add(data.attackName ,data));
        }

        public void AfterInit()
        {
            _attackSpeedStat = _statCompo.GetStat(_attackSpeedStat);
            _attackSpeedStat.OnValueChange += HandleAttackSpeedChange;
            _renderer.SetParam(_atkSpeedParam, _attackSpeedStat.Value);

            _damageStat = _statCompo.GetStat(_damageStat); //������ ���� ��������

            _animTrigger.OnAttackTrigger += HandleAttackTrigger;
        }

        private void OnDestroy()
        {
            _attackSpeedStat.OnValueChange -= HandleAttackSpeedChange;
            _animTrigger.OnAttackTrigger -= HandleAttackTrigger;
        }

        private void HandleAttackTrigger()
        {
            float damage = CalculateDamage(_damageStat, _currentAttackData); //todo :: �̰� ���߿� ������ ����ü�� ó���Ұ�. (�Ϸ�)
            Vector2 knockBack = _currentAttackData.knockbackForce;
            bool isSuccess = _damageCaster.CastDamage(damage, knockBack, _currentAttackData.isPowerAttack);

            if (isSuccess)
            {
                Debug.Log("<color=red>Damaged</color>");
            }
        }
        public float CaculateDamage(AttackDataSO atkData)
        {
            return CalculateDamage(_damageStat, atkData);
        }
        private float CalculateDamage(StatSO damageStat, AttackDataSO atkData) //����� �������
        {
            //��� ���� ũ��Ƽ�� ���������͵� ���;���.
            return damageStat.Value * atkData.damageMultiflier + atkData.damageIncrease;
        }

        private void HandleAttackSpeedChange(StatSO stat, float current, float previous)
        {
            _renderer.SetParam(_atkSpeedParam, current);
        }

        public bool CanJumpAttack()
        {
            bool returnValue = _canJumpAttack;
            if (_canJumpAttack)
                _canJumpAttack = false;
            return returnValue;
        }

        private void FixedUpdate()
        {
            if (_canJumpAttack == false && _mover.IsGrounded)
                _canJumpAttack = true;
        }

        public AttackDataSO GetAttackData(string keyName)
        {
            AttackDataSO returnData = _atkDictionary.GetValueOrDefault(keyName);
            Debug.Assert(returnData != null, $"{keyName} not found");
            return returnData;
        }
        public void SetAttackData(AttackDataSO atkData)
        {
            _currentAttackData = atkData;
        }

        public ICounterable GetCounterTarget()
        {
            return _damageCaster.GetCounterableTarget(_whatIsCounterable);
        }
    }
}
