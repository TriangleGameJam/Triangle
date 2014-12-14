using UnityEngine;
using System.Collections;

public static class Utilities
{
    public static float ClampAngle(float aAngle)
    {
        if (aAngle < -360.0f)
        {
            aAngle = 0.0f;
        }
        if (aAngle > 360.0f)
        {
            aAngle = 0.0f;
        }
        return aAngle;
    }

    public static float ClampAngle(float aAngle, float aMin, float aMax)
    {
        if(aAngle < -360.0f)
        {
            aAngle = 0.0f;
        }
        if(aAngle > 360.0f)
        {
            aAngle = 0.0f;
        }
        return Mathf.Clamp(aAngle, aMin, aMax);
    }

    public static void SetX(this Transform transform, float aValue)
    {
        transform.position = new Vector3(aValue, transform.position.y, transform.position.z);
    }
    public static void SetY(this Transform transform, float aValue)
    {
        transform.position = new Vector3(transform.position.x, aValue, transform.position.z);
    }
    public static void SetZ(this Transform transform, float aValue)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, aValue);
    }
}
