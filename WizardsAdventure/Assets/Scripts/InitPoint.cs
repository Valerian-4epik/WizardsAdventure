using UnityEngine;

public class InitPoint : MonoBehaviour
{
    private bool _isEmpty = true;

    public bool IsEmpty
    {
        get => _isEmpty;
        set => _isEmpty = value;
    }
}
