using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    Waypoint waypoint;
    void Awake () {
        waypoint = GetComponent<Waypoint>();
    }

    // Update is called once per frame
    void Update()
    {
        SnapToGrid();
        UpdateLabelName();
        CheckClick();
    }

    void SnapToGrid () {
        transform.position = new Vector3(
            waypoint.GetGridPos().x * waypoint.GetGridSize(),
            0,
            waypoint.GetGridPos().y * waypoint.GetGridSize()
        );
    }

    void UpdateLabelName () {
        string labelText = 
            waypoint.GetGridPos().x + 
            "," + 
            waypoint.GetGridPos().y ;
        
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        textMesh.text = labelText;
        gameObject.name = labelText;
    }

    void CheckClick () {
        if (Input.GetMouseButtonDown(0))
            print("Pressed primary button.");

        if (Input.GetMouseButtonDown(1))
            print("Pressed secondary button.");

        if (Input.GetMouseButtonDown(2))
            print("Pressed middle click.");
    }
}
