using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;

namespace BT
{
    public class BTNode : Node
    {
        public string Guid;
        public int Priority;
        public BTNodeType NodeType;

        protected IntegerField priorityField;

        public virtual string ToJson()
        {
            return "";
        }

        public virtual void FromJson(string json)
        {
        }

        public BTNode()
        {
            title = "BTNode";

            Guid = BTNodeFactory.NewGuid();
            priorityField = new IntegerField();
            priorityField.RegisterValueChangedCallback(v => Priority = v.newValue);
            mainContainer.Add(priorityField);
        }
    }
}