using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LaserP : MonoBehaviour
{
    public Material LaserHitMaterial;
    public Material LaserMaterial;

    //public GameObject Cam;
    public GameObject DepthCursor;
    public GameObject lineDrawer;
    public float laserWidth = 0.1f;
    public float laserMaxLength = 5f;
    //public Text debugText;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 m_LaserOffset = new Vector3(0, -0.1f, 0);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Cam.transform.position = Camera.main.transform.position;
        Vector2 ScreenPosition = new Vector2(0.5f, 0.5f);
        var xPixel = Camera.main.pixelWidth * ScreenPosition.x;
        var yPixel = Camera.main.pixelHeight * ScreenPosition.y;
        Vector3 screenPos = new Vector3(xPixel, yPixel, 0.0f);
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        startPosition = Camera.main.transform.position + m_LaserOffset;
        endPosition = DepthCursor.transform.position;
        //debugText.text = (startPosition - endPosition).ToString();
        UnityEngine.LineRenderer lineRenderer = lineDrawer.GetComponent<UnityEngine.LineRenderer>();
        RaycastHit raycastHit;
        //int layerMask = 1;


        if (Physics.Raycast(ray, out raycastHit))
        {
            endPosition = raycastHit.point;
            //debugText.text = "HIT";
            lineRenderer.material = LaserHitMaterial;
        }
        else
        {
            lineRenderer.material = LaserMaterial;
        }


        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);
    }

}

