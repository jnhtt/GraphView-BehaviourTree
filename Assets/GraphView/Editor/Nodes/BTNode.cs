using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

namespace BT
{
    public class BTNode : Node
    {
        public string Guid;
        public BTNodeType NodeType;

        public BTNode(BTNodeType nodeType)
        {
            NodeType = nodeType;

            title = "BTNode";

            Guid = BTNodeFactory.NewGuid();
        }
    }
}