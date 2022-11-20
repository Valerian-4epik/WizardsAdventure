using System;

namespace Enemy
{
    public interface IHealth
    {
        event Action HealthChanged;
        event Action ArmorChanged;
        event Action<int> LevelArmorInstalled;
        float CurrentHealth { get; set; }
        float MaxHealth { get; set; }
        float CurrentArmor { get; set; }
        float MaxArmor { get; set; }
        void TakeDamage(float damage);
    }
}