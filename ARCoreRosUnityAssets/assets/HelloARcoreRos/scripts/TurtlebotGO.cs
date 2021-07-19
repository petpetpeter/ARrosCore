//-----------------------------------------------------------------------
// <copyright file="AvatarPathController.cs" company="Google LLC">
//
// Copyright 2020 Google LLC. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;
/// <summary>
/// Controls the demo flow of hovering Andy.
/// </summary>
public class TurtlebotGO : MonoBehaviour
{
    /// <summary>
    /// References the 3D cursor.
    /// </summary>
    public GameObject DepthCursor;

    /// <summary>
    /// References the Andy avatar.
    /// </summary>
    public GameObject alphaRobot;
    


    public GameObject basePlane;
    /// <summary>
    /// References the waypoint.
    /// </summary>
    public GameObject marker;
    private bool isSelectingAngel = false;

    /// <summary>
    /// References the placemetnIndicator.
    /// </summary>
    //public GameObject placementIndicator;
    public GameObject lineDrawer;
    public GameObject baseManipulator;
    public Text rotationText;

    private const float k_AvatarOffset = 0.0f;

    private const float k_WaypointYOffset = 0.00f; //since cursor is sink to the floor

    private GameObject m_Root;


    private bool m_FirstWaypointPlaced = false;

    /// <summary>
    /// Sets a waypoint for the avatar.
    /// </summary>

    /*void DrawLine(Vector3 start, Vector3 end, Color color)
    {
       
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        

    }*/
    public void Showline(Vector3 start, Vector3 end)
    {
        Vector3 planeStart = start;
        Vector3 planeEnd = end;
        Vector3 planeBase = basePlane.transform.position;
        planeBase.y = planeEnd.y;
        planeBase.y = planeStart.y;
        Vector3 planeDistance = planeStart - planeEnd;
        Vector3 distanceFromBasePlane = Vector3.zero;
        distanceFromBasePlane.x = Vector3.Dot(planeDistance, basePlane.transform.right.normalized);
        distanceFromBasePlane.y = Vector3.Dot(planeDistance, basePlane.transform.up.normalized);
        distanceFromBasePlane.z = Vector3.Dot(planeDistance, basePlane.transform.forward.normalized);
        //Vector3 planeOrigin = Vector3.zero;
        //planeOrigin.y = planeStart.y;
        UnityEngine.LineRenderer lineRenderer = lineDrawer.GetComponent<UnityEngine.LineRenderer>();
        lineRenderer.SetPosition(0, planeStart);
        lineRenderer.SetPosition(1, planeEnd);

        /*Quaternion quadRotation = Quaternion.FromToRotation(planeOrigin, planeDistance);
        Vector3 eulerRotation = quadRotation.eulerAngles;
        rotationText.text = eulerRotation.y.ToString();*/
        //float angle = Mathf.Atan2(planeDistance.x - planeOrigin.x, -planeDistance.z+  planeOrigin.z) * Mathf.Rad2Deg;
        
    }

    public void ShowAlpha(Vector3 start, Vector3 end)
    {
        Vector3 alphabotPos = Vector3.zero;
        alphabotPos = start;
        alphabotPos.y = start.y + 0.0f;
        alphaRobot.SetActive(true);
        alphaRobot.transform.position = alphabotPos;


        Vector3 planeStart = start;
        Vector3 planeEnd = end;
        Vector3 planeBase = basePlane.transform.position;
        planeEnd.y = planeBase.y;
        planeStart.y = planeBase.y;

        Vector3 planeDistance = planeEnd - planeStart;
        Vector3 direction = planeDistance.normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        alphaRobot.transform.rotation = lookRotation;//Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);

        
        Vector3 textCheckV = lookRotation.eulerAngles;
        rotationText.text = textCheckV.ToString();

    }

    public void DropWaypoint()
    {

        Vector3 pos = DepthCursor.transform.position;
        pos.y += k_WaypointYOffset;



        Vector2 ScreenPosition = new Vector2(0.5f, 0.5f);
        var xPixel = Camera.main.pixelWidth * ScreenPosition.x;
        var yPixel = Camera.main.pixelHeight * ScreenPosition.y;






        if (!m_FirstWaypointPlaced)
        {
            //
            /*robot.SetActive(true);*/
            /*TrackableHit hitBase;
            TrackableHitFlags raycastFilterBase = TrackableHitFlags.PlaneWithinPolygon;
            if (Frame.Raycast(xPixel, yPixel, raycastFilterBase, out hitBase))*/
            TrackableHit hit;
            bool foundHit = false;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                TrackableHitFlags.FeaturePointWithSurfaceNormal;
            raycastFilter |= TrackableHitFlags.Depth;
            foundHit = Frame.Raycast(xPixel, yPixel, raycastFilter, out hit);
            if (foundHit)
            {
                
                // Instantiate manipulator.
                var manipulator =
                    Instantiate(baseManipulator, hit.Pose.position, hit.Pose.rotation);

                // Make game object a child of the manipulator.
                basePlane.transform.parent = manipulator.transform;

                // Create an anchor to allow ARCore to track the hitpoint as understanding of
                // the physical world evolves.
                var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                // Make manipulator a child of the anchor.
                manipulator.transform.parent = anchor.transform;

                // Select the placed object.
                manipulator.GetComponent<GoogleARCore.Examples.ObjectManipulation.Manipulator>().Select();
                pos = anchor.transform.position;
            }
            

            basePlane.SetActive(true);
            /*pos.y = pos.y + k_AvatarOffset;*/
            basePlane.transform.position = pos;
            basePlane.transform.Rotate(0, 0, 0, Space.Self);
            marker.SetActive(true);
            marker.transform.position = pos;
            marker.transform.Rotate(0, 0, 0, Space.Self);
            //Vector3 fromBasePosition = getRelativePositionFromTo(basePlane.transform, pos);
            //placementPosition.text = "Place Position: " + fromBasePosition.ToString("F3");
            /*placementIndicator.SetActive(true);
            placementIndicator.transform.position = pos;*/
            // Instantiate manipulator.
        }
        else
        {
            /*TrackableHit hitPoint;
            TrackableHitFlags raycastFilterPoint = TrackableHitFlags.PlaneWithinPolygon;
            if (Frame.Raycast(xPixel, yPixel, raycastFilterPoint, out hitPoint))*/
            TrackableHit hit;
            bool foundHit = false;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                TrackableHitFlags.FeaturePointWithSurfaceNormal;

            raycastFilter |= TrackableHitFlags.Depth;
            foundHit = Frame.Raycast(xPixel, yPixel, raycastFilter, out hit);
            if (foundHit)
            {
                var anchor1 = hit.Trackable.CreateAnchor(hit.Pose);
                rotationText.text = hit.Pose.position.ToString();
                pos = anchor1.transform.position;
                rotationText.text = "HIT";
            }
            else
            {
                rotationText.text = "Not Hit";
            }
            
            marker.transform.position = pos;
            marker.transform.Rotate(0, 0, 0, Space.Self);
            //Vector3 fromBasePosition = getRelativePositionFromTo(basePlane.transform, pos);
            //placementPosition.text = "Place Position: " + fromBasePosition.ToString("F3");

        }


        m_FirstWaypointPlaced = true;
    }

 

    public void ChooseAngel()
    {
        isSelectingAngel = true;
        lineDrawer.SetActive(true);
    }
    public void notChooseAngel()
    {
        isSelectingAngel = false;
        lineDrawer.SetActive(false);
        //rotationText.text = "Rotation";
    }
    /// <summary>
    /// Clears all the instantiated waypoints.
    /// </summary>
    public void Clear()
    {
        if (m_Root != null)
        {
            foreach (Transform child in m_Root.transform)
            {
                Destroy(child.gameObject);
                
            }
        }
    }

    private void OnDestroy()
    {
        Destroy(m_Root);
        m_Root = null;
    }

    private void Start()
    {
        
    }

    private void Update()
    {

        
    }
    private void FixedUpdate()
    {
        if (isSelectingAngel)
        {
            //Vector3 pos = ;
            //pos.y += k_WaypointYOffset;
            ShowAlpha(marker.transform.position, DepthCursor.transform.position);
            Showline(marker.transform.position, DepthCursor.transform.position);
        }
        
    }
}
