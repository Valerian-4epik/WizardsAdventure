using Infrastructure;
using UnityEngine;
using UnityEngine.Serialization;
using Wizards;

[CreateAssetMenu(fileName = "ItemInfo", menuName = " Items/ItemInfo")]
public class ItemInfo : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private string _name;
    [SerializeField] private int _level;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private AttackType _attackType;
    [SerializeField] private ItemType _itemType;
    [SerializeField] private TypeOfObject _typeOfObject;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _attackRange;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _armor;

    public TypeOfObject TypeOfObject => _typeOfObject;
    public string ID => _id;
    public string Name => _name;
    public int Level => _level;
    public int Price => _price;
    public Sprite Icon => _icon;
    public AttackType AttackType => _attackType;
    public ItemType ItemType => _itemType;
    public float Damage => _damage;
    public float AttackSpeed => _attackSpeed;
    public float AttackRange => _attackRange;
    public GameObject Projectile => _projectile;
    public GameObject Prefab => _prefab;
    public float Armor => _armor;

}