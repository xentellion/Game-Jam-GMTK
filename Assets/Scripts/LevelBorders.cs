using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CamerBorders))]
public class LevelBorders : Editor
{
    CamerBorders ai;

    private void OnEnable()
    {
        ai = (CamerBorders)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }

    private void OnSceneGUI()
    {
        for (int i = 0; i < 2; i++) 
            ai.handles[i] = Handles.PositionHandle(ai.handles[i], Quaternion.identity);

        Handles.color = Color.red;

        Handles.DrawLine(ai.handles[0], new Vector3(ai.handles[0].x, ai.handles[1].y));
        Handles.DrawLine(ai.handles[1], new Vector3(ai.handles[1].x, ai.handles[0].y));

        Handles.color = Color.green;

        Handles.DrawLine(new Vector3(ai.handles[0].x, ai.handles[1].y), ai.handles[1]);
        Handles.DrawLine(new Vector3(ai.handles[1].x, ai.handles[0].y), ai.handles[0]);
    }
}
