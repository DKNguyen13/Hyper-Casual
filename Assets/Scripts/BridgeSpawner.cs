using UnityEngine;

public class BridgeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _startReference, _endReference;
    [SerializeField] private BoxCollider _hiddenPlatform;

    void Start()
    {
        Vector3 direction = _endReference.transform.position - _startReference.transform.position;
        float distance = direction.magnitude;
        direction = direction.normalized;
        _hiddenPlatform.transform.forward = direction;
        _hiddenPlatform.size = new Vector3(_hiddenPlatform.size.x, _hiddenPlatform.size.y, distance);
        _hiddenPlatform.transform.position = _startReference.transform.position + (direction * distance / 2) + (new Vector3(0, -direction.z, direction.y) * _hiddenPlatform.size.y / 2);

    }

    void Update()
    {
        
    }

    public GameObject StartReference => _startReference;
    public GameObject EndReference => _endReference;
}
