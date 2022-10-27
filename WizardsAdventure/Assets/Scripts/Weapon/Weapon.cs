using Blobcreate.ProjectileToolkit;
using JetBrains.Annotations;
using NodeCanvas.Tasks.Actions;
using UnityEngine;

namespace Wizards
{
    public class Weapon
    {
        private readonly GameObject _projectile;
        public Transform LaunchPoint { get; }
        public ItemInfo Info { get; }
        public string Type { get; }
        public Rigidbody ProjecttileRig { get; }

        public Weapon(Transform launchPoint, ItemInfo item)
        {
            LaunchPoint = launchPoint;
            Info = item;
            Type = item.AttackType.ToString();
            _projectile = item.Projectile;
            _projectile.GetComponent<ETFXProjectileScript>().Damage = item.Damage;
            ProjecttileRig = _projectile.GetComponent<Rigidbody>();
        }
    }
}