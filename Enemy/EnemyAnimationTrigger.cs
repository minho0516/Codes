using RPG.Entities;
using System;
using UnityEngine;

namespace RPG.Enemies
{
    public class EnemyAnimationTrigger : EntityAnimationTrigger
    {
        public event Action<bool> CounterStatusChange;

        private void OpenCouner() => CounterStatusChange?.Invoke(true);
        private void CloseCounter() => CounterStatusChange?.Invoke(false);
    }
}
