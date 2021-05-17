using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

public class BTGraphEditor : GraphView
{

    public BTGraphEditor(EditorWindow editorWindow)
    {
        this.StretchToParentSize();

        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var menuWindowProvider = ScriptableObject.CreateInstance<SearchMenuWindowProvider>();
        menuWindowProvider.Initialize(this, editorWindow);
        nodeCreationRequest += context =>
        {
            SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), menuWindowProvider);

        };
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatibbleParts = new List<Port>();
        compatibbleParts.AddRange(ports.ToList().Where(port =>
        {
            if (startPort.node == port.node)
            {
                return false;
            }
            if (port.direction == startPort.direction)
            {
                return false;
            }
            if (port.portType != startPort.portType)
            {
                return false;
            }
            return true;
        }));

        return compatibbleParts;
    }

    public void Load()
    {
        BT.BTGraphSaveUtility.Load("test001", this);
    }

    public void Save()
    {
        BT.BTGraphSaveUtility.Save("test001", this);
    }
}
