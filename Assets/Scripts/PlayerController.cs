using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _limitX;
    [SerializeField] private float _runningSpeed;
    [SerializeField] private float _xSpeed;
    private float _currentRunningSpeed;
    
    void Start()
    {
        _currentRunningSpeed = _runningSpeed;    
    }

    void Update()
    {
        float newX = 0;
        float touchXDelta = 0;
        
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            touchXDelta = Input.GetTouch(0).deltaPosition.x / Screen.width;
        }
        else
        {
            touchXDelta = Input.GetAxis("Mouse X");
        }

        newX = transform.position.x + _xSpeed * touchXDelta * Time.deltaTime;
        newX = Mathf.Clamp(newX, -_limitX, _limitX);
        Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z + _currentRunningSpeed * Time.deltaTime);
        transform.position = newPosition;

    }
}
