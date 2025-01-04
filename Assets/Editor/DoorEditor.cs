using UnityEditor;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(Door))]
public class DoorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw default inspector fields
        DrawDefaultInspector();

        Door door = (Door)target;

        // Fetch all scene names from Build Settings
        string[] scenes = GetAllScenes();

        // Display dropdown for scene selection
        int newIndex = EditorGUILayout.Popup("Target Scene", door.GetTargetSceneIndex(), scenes);

        // Update the scene index if it changes
        if (newIndex != door.GetTargetSceneIndex())
        {
            Undo.RecordObject(door, "Change Target Scene");
            door.SetTargetSceneIndex(newIndex);
            EditorUtility.SetDirty(door);
        }
    }

    private string[] GetAllScenes()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        string[] scenes = new string[sceneCount];
        for (int i = 0; i < sceneCount; i++)
        {
            scenes[i] = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
        }
        return scenes;
    }
}
