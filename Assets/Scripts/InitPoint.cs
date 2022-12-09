using UnityEngine;

public class InitPoint : MonoBehaviour
{
    [SerializeField] private ParticleSystem _hpGradeEffect;
    [SerializeField] private ParticleSystem _asGradeEffect;

    private bool _isEmpty = true;

    public bool IsEmpty
    {
        get => _isEmpty;
        set => _isEmpty = value;
    }

    public void PlayHPGrade()
    {
        if (!_isEmpty)
            _hpGradeEffect.Play();
    }
    
    public void PlayASGrade()
    {
        if (!_isEmpty)
            _asGradeEffect.Play();
    }
}