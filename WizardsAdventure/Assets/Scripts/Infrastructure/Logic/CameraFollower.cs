using UI;
using UnityEngine;
using DG.Tweening;

public class CameraFollower : MonoBehaviour
{
    private const int DURATION = 9;
    
    [SerializeField] private Vector3 _fightTimePosition;
    [SerializeField] private Quaternion _fightTimeRotation;
    
    private Camera _cameraMain;
    private Transform _cameraPosition;
    private UIInventory _shopInterface;
    
    private void OnEnable()
    {
        if(Camera.main != null)
            _cameraMain = Camera.main;
    }

    public void SetShopInterface(UIInventory shopInterface)
    {
        _shopInterface = shopInterface;
        _shopInterface.Fight += ChangePosition;
    }

    public void ChangePosition()
    {
        _cameraMain.gameObject.transform.DOMove(_fightTimePosition, DURATION);
        _cameraMain.gameObject.transform.DORotateQuaternion(_fightTimeRotation, DURATION);
    }
}
