using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class JoyMover : MonoBehaviour
{
    public float speedScale = 0.8f;
    public float rotateScale = 0.8f;
    public VariableJoystick xyJoystick;
    public VariableJoystick zJoystick;
    public VariableJoystick yRotJoystick;
    public VariableJoystick xzRotJoystick;

    public GameObject movingObject;
    public GameObject depthCursor;
    private GameObject baseAtDepthPos;

    private Vector3 velocity;
    private Vector3 currentPosition;
    private Vector3 rot_velocity;
    public Text debugText;

    public bool isX; public bool isY; public bool isZ; public bool isXr; public bool isYr; public bool isZr;
    public bool isFromDepthCursor;

    public void FixedUpdate() // dt = 0.02 sec
    {
        /*currentPosition = movingObject.transform.position;
        currentPosition.x += xyJoystick.Direction[0] * 0.02f * speedScale;
        currentPosition.z += xyJoystick.Direction[1] * 0.02f * speedScale;

        movingObject.transform.position = currentPosition;*/
        velocity.x = xyJoystick.Direction[0] * speedScale * System.Convert.ToSingle(isX);
        velocity.z = xyJoystick.Direction[1] * speedScale * System.Convert.ToSingle(isY);
        velocity.y = zJoystick.Direction[1] * speedScale * System.Convert.ToSingle(isZ);

        rot_velocity.x = xzRotJoystick.Direction[1] * rotateScale * System.Convert.ToSingle(isXr);
        rot_velocity.y = yRotJoystick.Direction[0] * rotateScale * System.Convert.ToSingle(isYr);
        rot_velocity.z = xzRotJoystick.Direction[0] * rotateScale * System.Convert.ToSingle(isZr);
        if (Mathf.Abs(xyJoystick.Direction[0]) < 0.1f)
        {
            velocity.x = 0.0f;
        }
        if (Mathf.Abs(xyJoystick.Direction[1]) < 0.1f)
        {
            velocity.z = 0.0f;
        }

        if (isFromDepthCursor)
        {
            Vector3 depthCursorPos = depthCursor.transform.position;

            Vector3 movingPosWorld = GetPositionFromWorld(velocity * Time.deltaTime);
            movingObject.transform.position = depthCursorPos + movingPosWorld;
        }
        else
        {
            movingObject.transform.Translate(velocity * Time.deltaTime);
            movingObject.transform.Rotate(rot_velocity);
        }
        

        debugText.text = velocity.ToString();
    }

    private Vector3 GetPositionFromWorld(Vector3 pos)
    {
        Vector3 robotFromBasePosition = pos;
        //localToWorld Matrix : Transforms Vector from Local Frame to World(AR) Frame
        Matrix4x4 baseFromWorldMatrix = depthCursor.transform.localToWorldMatrix;
        Vector3 robotFromWorldPosition = baseFromWorldMatrix.MultiplyPoint(robotFromBasePosition);
        return robotFromWorldPosition;
    }
}