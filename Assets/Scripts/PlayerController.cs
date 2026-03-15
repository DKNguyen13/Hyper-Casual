using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] private float _limitX;
    [SerializeField] private float _runningSpeed;
    [SerializeField] private float _xSpeed;
    private float _currentRunningSpeed;

    [SerializeField] private GameObject _ridingCylinderPrefab;
    public List<RidingCylinder> cylinders;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

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
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("AddCylinder"))
        {
            collision.enabled = false;collision.enabled = false;
            IncrementCylinderVolume(0.1f);
            Destroy(collision.gameObject);
        }
    }

    public void IncrementCylinderVolume(float value)
    {

        if (cylinders.Count == 0)
        {
            if (value > 0)
            {
                CreateCylinder(value);
            }
            else
            {
                // Gameover
            }
        }
        else
        {
            cylinders[cylinders.Count - 1].IncrementCylinderVolume(value);
        }
    }

    public void CreateCylinder(float value)
    {
        RidingCylinder createdCylinder = Instantiate(_ridingCylinderPrefab, transform).GetComponent<RidingCylinder>();
        cylinders.Add(createdCylinder);
        createdCylinder.IncrementCylinderVolume(value);
    }

    public void DestroyCylinder(RidingCylinder cylinder)
    {
        cylinders.Remove(cylinder);
        Destroy(cylinder.gameObject);
    }
}
