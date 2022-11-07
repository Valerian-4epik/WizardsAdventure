using UI;
using UnityEngine;
using DG.Tweening;

public class CameraFollower : MonoBehaviour
{
    private const int DURATION = 2;
    
    [SerializeField] private Vector3 _shopTimePosition;
    [SerializeField] private Vector3 _fightTimePosition;
    
    private Camera _cameraMain;
    private Transform _cameraPosition;
    private UIInventory _shopInterface;
    
    private void OnEnable()
    {
        if(Camera.main != null)
            _cameraMain = Camera.main;
        // _cameraPosition = _cameraMain.transform;
        // _cameraPosition.position = _shopTimePosition;
    }

    public void SetShopInterface(UIInventory shopInterface)
    {
        _shopInterface = shopInterface;
        _shopInterface.Fight += ChangePosition;
    }

    public void ChangePosition() => 
        _cameraPosition.DOMove(_fightTimePosition, DURATION);
}
