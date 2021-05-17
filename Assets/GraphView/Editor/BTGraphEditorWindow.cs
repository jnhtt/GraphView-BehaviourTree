using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class BTGraphEditorWindow : EditorWindow
{
    [MenuItem("Window/BTGraphEditorWindow")]
    public static void Open()
    {
        GetWindow<BTGraphEditorWindow>(ObjectNames.NicifyVariableName(nameof(BTGraphEditorWindow)));
    }

    private void OnEnable()
    {
        var graphView = new BTGraphEditor(this);
        rootVisualElement.Add(graphView);

        rootVisualElement.Add(new Button(graphView.Load) { text = "Load" });
        rootVisualElement.Add(new Button(graphView.Save) { text = "Save" });
    }
}
