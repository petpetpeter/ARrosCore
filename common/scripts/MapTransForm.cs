using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapTransForm : MonoBehaviour
{
    
    [SerializeField]
    private GameObject baseFromWorld;
    [SerializeField]
    private GameObject robotFromBase;
    public Transform robotFromWorldTransform;
    public bool isStatic;
    public Vector3 offPosition;
    public Vector3 offRotation;

    private void GetPositionFromWorld()
    {
        Vector3 robotFromBasePosition = robotFromBase.transform.position;
        if (isStatic)
        {
            robotFromBasePosition = Vector3.zero;

        }
        //localToWorld Matrix : Transforms Vector from Local Frame to World(AR) Frame
        Matrix4x4 baseFromWorldMatrix = baseFromWorld.transform.localToWorldMatrix;
        robotFromWorldTransform.position = baseFromWorldMatrix.MultiplyPoint(robotFromBasePosition);
        
        //offset
        robotFromWorldTransform.position += offPosition;
       
    }

    private void GetRotationFromWorld()
    {
        Quaternion robotFromBaseRotation = robotFromBase.transform.rotation;
        if (isStatic)
        {
            robotFromBaseRotation = Quaternion.identity;

        }

        Quaternion robotFromWorldRotation = robotFromBaseRotation * baseFromWorld.transform.rotation;
        robotFromWorldTransform.rotation = robotFromWorldRotation;

        //offset
        Quaternion add_rotation = Quaternion.Euler(offRotation);
        robotFromWorldTransform.rotation *= add_rotation;


    }


    // Update is called once per frame
    void Update()
    {
        GetPositionFromWorld();
        GetRotationFromWorld();
    }
}
