using UnityEngine;

[CreateAssetMenu(fileName = "ItemInfo", menuName = " Items/ItemInfo")]
public class ItemInfo : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private string _name;
    [SerializeField] private int _level;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackSpeed;

    public string id => _id;
    public string name => _name;
    public int level => _level;
    public Sprite icon => _icon;
    public int damage => _damage;
    public float attackSpeed => _attackSpeed;
}
