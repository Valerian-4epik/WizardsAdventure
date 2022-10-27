using Infrastructure;
using UnityEngine;
using Wizards;

[CreateAssetMenu(fileName = "ItemInfo", menuName = " Items/ItemInfo")]
public class ItemInfo : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private string _name;
    [SerializeField] private int _level;
    [SerializeField] private Sprite _icon;
    [SerializeField] private AttackType _attackType;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _attackRange;
    [SerializeField] private GameObject _projectile;

    public string ID => _id;
    public new string Name => _name;
    public int Level => _level;
    public Sprite Icon => _icon;
    public AttackType AttackType => _attackType;
    public float Damage => _damage;
    public float AttackSpeed => _attackSpeed;
    public float AttackRange => _attackRange;
    public GameObject Projectile => _projectile;

}
