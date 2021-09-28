using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.Common;
public class PlaneSelector : MonoBehaviour
{
    public GameObject basePlane;
    public GameObject planeGenerator;

    private List<DetectedPlane> _newPlanes = new List<DetectedPlane>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropMarker()
    {

        Vector2 ScreenPosition = new Vector2(0.5f, 0.5f);
        var xPixel = Camera.main.pixelWidth * ScreenPosition.x;
        var yPixel = Camera.main.pixelHeight * ScreenPosition.y;

        TrackableHit hit;
        bool foundHit = false;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon;
        foundHit = Frame.Raycast(xPixel, yPixel, raycastFilter, out hit);
        if (foundHit)
        {
            //selectingPlane = hit;
            var anchor = hit.Trackable.CreateAnchor(hit.Pose);
            basePlane.transform.position = anchor.transform.position;
            basePlane.transform.Rotate(0, 0, 0, Space.Self);
            basePlane.transform.parent = anchor.transform;
        }
    }

    public void DestoryPlaneVisualizer()
    {
        planeGenerator.GetComponent<DetectedPlaneGenerator>().enabled = false;
        var planeObjects = GameObject.FindGameObjectsWithTag("Plane");
        var planeCount = planeObjects.Length;
        foreach (var plane in planeObjects)
        {
            Destroy(plane);
        }

    }
}
