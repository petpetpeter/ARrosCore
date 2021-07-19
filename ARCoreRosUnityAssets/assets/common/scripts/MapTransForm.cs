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
    public Vector3 offSet;
   
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
        robotFromWorldTransform.position += offSet;
       
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
    }


    // Update is called once per frame
    void Update()
    {
        GetPositionFromWorld();
        GetRotationFromWorld();
    }
}
