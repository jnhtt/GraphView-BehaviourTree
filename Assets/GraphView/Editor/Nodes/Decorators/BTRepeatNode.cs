using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;

namespace BT
{
    public class BTRepeatNode : BTDecoratorNode
    {
        private IntegerField repeatField;

        public BTRepeat data;

        public BTRepeatNode() : base()
        {
            data = new BTRepeat();

            NodeType = BTNodeType.Repeat;
            title = "BTRepeat";

            repeatField = new IntegerField();
            repeatField.RegisterValueChangedCallback(v => data.repeat = v.newValue);
            mainContainer.Add(repeatField);
        }

        public override string ToJson()
        {
            return data.ToJson();
        }

        public override void FromJson(string json)
        {
            data.FromJson(json);
            repeatField.value = data.repeat;
        }
    }
}
