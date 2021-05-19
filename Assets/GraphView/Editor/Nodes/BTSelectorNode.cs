using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

namespace BT
{
    public class BTSelectorNode : BTNode
    {
        public BTSelectorNode() : base(BTNodeType.Selector)
        {
            title = "Selector";

            var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(float));
            inputPort.portName = "In";
            inputContainer.Add(inputPort);

            var ouputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            ouputPort.portName = "Out";
            outputContainer.Add(ouputPort);
        }
    }
}