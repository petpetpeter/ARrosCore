using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class BaseCalibrator : MonoBehaviour
{
    /// <summary>
    /// References the 3D cursor.
    /// </summary>
    public GameObject DepthCursor;
    public GameObject basePlane;

    void start()
    {
        
    }

    public void DropBase()
    {

        Vector3 pos = DepthCursor.transform.position;
        Vector2 ScreenPosition = new Vector2(0.5f, 0.5f);
        var xPixel = Camera.main.pixelWidth * ScreenPosition.x;
        var yPixel = Camera.main.pixelHeight * ScreenPosition.y;

        TrackableHit hit;
        bool foundHit = false;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
            TrackableHitFlags.FeaturePointWithSurfaceNormal;
        raycastFilter |= TrackableHitFlags.Depth;
        foundHit = Frame.Raycast(xPixel, yPixel, raycastFilter, out hit);
        if (foundHit)
        {
            // Create an anchor to allow ARCore to track the hitpoint as understanding of
            // the physical world evolves.
            var anchor = hit.Trackable.CreateAnchor(hit.Pose);
            // Make manipulator a child of the anchor.
            basePlane.transform.position = anchor.transform.position;
            basePlane.transform.rotation = Quaternion.identity;
            basePlane.transform.Rotate(0, 180, 0, Space.Self);

            basePlane.transform.parent = anchor.transform;
        }

        



    }

    void Update()
    {

    }
}
