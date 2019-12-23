using UnityEngine;

public class UnitFacade : MonoBehaviour
{
    [SerializeField]
    private Transform _headPlaceholder;
    
    public Transform HeadPlaceholder => _headPlaceholder;
}
