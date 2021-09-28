﻿//-----------------------------------------------------------------------
// <copyright file="ObjectCollisionAwarePlacementManager.cs" company="Google LLC">
//
// Copyright 2020 Google LLC
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

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the demo flow of hovering Andy.
/// </summary>
public class DepthCursorWithUI : MonoBehaviour
{
    /// <summary>
    /// References the 3D cursor.
    /// </summary>
    public GameObject DepthCursor;

    /// <summary>
    /// References the object to place.
    /// </summary>
    public GameObject[] ObjectPrefabs;

    private const float _avatarOffsetMeters = 0.015f;
    private ObjectViewerInteractionController _interactionController;
    private int _currentPrefabId = 0;
    private GameObject _root;


    ///peter
    public Text _debuggerText;
    
    public float _upSlider;
    public void OnValueChanged(float newValue)
    {
        _upSlider = newValue;
        _debuggerText.text = _upSlider.ToString();
    }

    /// <summary>
    /// Places the object in the current position.
    /// </summary>
    public void PlaceModel()
    {
        // Instantiates and clones the transform.
        var currentTransform = GetCurrentModel().transform;
        var newPosition = currentTransform.position;
        var newModel = Instantiate(GetCurrentModel(), newPosition, currentTransform.rotation,
            _root.transform);

        if (_interactionController != null)
        {
            _interactionController.SetManipulatedObject(newModel);
        }

        newModel.GetComponent<ObjectCollisionController>().enabled = false;
        newModel.GetComponent<ObjectCollisionEvent>().enabled = false;

        // Clones the material for each submesh.
        var oldRenderers = GetCurrentModel().GetComponentsInChildren<Renderer>();
        var newRenderers = newModel.GetComponentsInChildren<Renderer>();

        for (int i = 0; i < oldRenderers.Length; ++i)
        {
            newRenderers[i].material = new Material(oldRenderers[i].material);
            newRenderers[i].gameObject.AddComponent(typeof(DepthTarget));
        }

        SetPrefabVisibility(_currentPrefabId, false);
    }

    /// <summary>
    /// Rotates the object clockwise by degrees.
    /// </summary>
    /// <param name="degrees">Rotation degrees.</param>
    public void RotateModel(float degrees)
    {
        // Rotates the object clockwise by degrees around its up vector.
        GetCurrentModel().transform.rotation *= Quaternion.Euler(0, degrees, 0);
    }

    /// <summary>
    /// Switches to the next model.
    /// </summary>
    public void SwitchToNextModel()
    {
        _currentPrefabId = (_currentPrefabId + 1) % ObjectPrefabs.Length;
        HideInactiveModels();

        var placeText = GameObject.Find("PlaceButton").gameObject.GetComponentInChildren<Text>();
        placeText.text = "Place Object";
    }

    /// <summary>
    /// Clears all placed models.
    /// </summary>
    public void ClearModels()
    {
        if (_root != null)
        {
            foreach (Transform child in _root.transform)
            {
                Destroy(child.gameObject);
            }
        }

        SetPrefabVisibility(_currentPrefabId, true);
    }

    private void Start()
    {
        
        _root = new GameObject("Colliders");
        _interactionController = GetComponent<ObjectViewerInteractionController>();

        HideInactiveModels();
    }

    private void HideInactiveModels()
    {
        for (int i = 0; i < ObjectPrefabs.Length; ++i)
        {
            SetPrefabVisibility(i, i == _currentPrefabId);
        }

        // Updates the vertices list for faster collision checking.
        GetCurrentModel().GetComponent<ObjectCollisionController>().UpdateVerticesList();
    }

    private void SetPrefabVisibility(int prefabId, bool isActive)
    {
        var renderers = ObjectPrefabs[prefabId].GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            renderer.enabled = isActive;
        }

        ObjectPrefabs[prefabId].GetComponent<ObjectCollisionController>().enabled = isActive;
        ObjectPrefabs[prefabId].GetComponent<ObjectCollisionEvent>().enabled = isActive;
    }

    private GameObject GetCurrentModel()
    {
        return ObjectPrefabs[_currentPrefabId];
    }

    private void Update()
    {
        Vector3 toCamera = Camera.main.transform.position - DepthCursor.transform.position;
        toCamera.Normalize();

        Vector3 upVector = Vector3.up;

        GetCurrentModel().transform.position = DepthCursor.transform.position +
            (toCamera * _avatarOffsetMeters) + (upVector * _upSlider);
    }

    private void OnDestroy()
    {
        if (_root != null)
        {
            Destroy(_root);
        }

        _root = null;
    }
}
