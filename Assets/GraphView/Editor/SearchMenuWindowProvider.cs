using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using BT;

public class SearchMenuWindowProvider : ScriptableObject, ISearchWindowProvider
{
    private BTGraphEditor graphView;
    private EditorWindow editorWindow;

    public void Initialize(BTGraphEditor graphView, EditorWindow editorWindow)
    {
        this.graphView = graphView;
        this.editorWindow = editorWindow;
    }

    List<SearchTreeEntry> ISearchWindowProvider.CreateSearchTree(SearchWindowContext context)
    {
        var entries = new List<SearchTreeEntry>();
        entries.Add(new SearchTreeGroupEntry(new GUIContent("Create Node")));
        entries.Add(new SearchTreeGroupEntry(new GUIContent("BT")) { level = 1 });
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(BTNode))) { level = 2, userData = typeof(BTNode) });
        //entries.Add(new SearchTreeEntry(new GUIContent(nameof(ValueNode))) { level = 2, userData = typeof(ValueNode) });
        //entries.Add(new SearchTreeEntry(new GUIContent(nameof(AddNode))) { level = 2, userData = typeof(AddNode) });
        //entries.Add(new SearchTreeEntry(new GUIContent(nameof(OutputNode))) { level = 2, userData = typeof(OutputNode) });
        return entries;
    }

    bool ISearchWindowProvider.OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
    {
        var type = searchTreeEntry.userData as Type;
        var node = Activator.CreateInstance(type) as Node;

        var worldMousePos = editorWindow.rootVisualElement.ChangeCoordinatesTo(editorWindow.rootVisualElement.parent, context.screenMousePosition - editorWindow.position.position);
        var localMousePos = graphView.contentViewContainer.WorldToLocal(worldMousePos);
        node.SetPosition(new Rect(localMousePos, new Vector2(100, 100)));

        graphView.AddElement(node);
        return true;
    }
}