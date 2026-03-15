using UnityEngine;

public class RidingCylinder : MonoBehaviour
{
    private bool _filled;
    private float _value;

    public void IncrementCylinderVolume(float value)
    {
        _value += value;

        if (_value >= 1f)
        {
            float leftValue = _value - 1f;

            if (leftValue < 0.001f)
                leftValue = 0f;

            _value = 1f;

            int cylinderCount = PlayerController.Instance.cylinders.Count;

            transform.localPosition = new Vector3(
                transform.localPosition.x,
                -0.5f * (cylinderCount - 1) - 0.25f,
                transform.localPosition.z
            );

            transform.localScale = new Vector3(0.5f, transform.localScale.y, 0.5f);

            if (leftValue > 0)
                PlayerController.Instance.CreateCylinder(leftValue);
        }
        else if (_value < 0)
        {
            PlayerController.Instance.DestroyCylinder(this);
        }
        else
        {
            int cylinderCount = PlayerController.Instance.cylinders.Count;
            transform.localPosition = new Vector3(transform.localPosition.x, -0.5f * (cylinderCount - 1) - 0.25f * _value, transform.localPosition.z);
            transform.localScale = new Vector3(0.5f * _value, transform.localScale.y, 0.5f * _value);
        }
    }
}