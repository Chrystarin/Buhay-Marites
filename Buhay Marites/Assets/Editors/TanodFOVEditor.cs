using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TanodFOV))]
public class TanodFOVEditor : Editor
{
    private void OnSceneGUI()
    {
        TanodFOV fov = (TanodFOV)target;

        Handles.color = Color.white;
        Handles.DrawWireDisc(fov.transform.position, Vector3.forward, fov.radius);

        Vector3 angle1 = DirectionFromAngle(-fov.transform.eulerAngles.z, -fov.viewAngle / 2);
        Vector3 angle2 = DirectionFromAngle(-fov.transform.eulerAngles.z, fov.viewAngle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + angle1 * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + angle2 * fov.radius);

        if (fov.playerVisible)
        {
            Handles.color = Color.red;
            Handles.DrawLine(fov.transform.position, fov.player.transform.position);
        }
    }

    private Vector2 DirectionFromAngle(float eY, float angleDeg)
    {
        angleDeg += eY;
        return new Vector2(Mathf.Sin(angleDeg * Mathf.Deg2Rad), Mathf.Cos(angleDeg * Mathf.Deg2Rad));
    }
}
