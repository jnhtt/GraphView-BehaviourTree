using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace BT
{
    public class BTMoveNode : BTActionNode
    {
        private TextField targetField;
        public string TargetText {
            get { return targetField.value; }
            set { targetField.value = value; }
        }

        public override string ToJson()
        {
            var list = new BTParameterList();
            list.ParameterList.Add(new BTParamter() { Name = "Target", Value = TargetText });
            return JsonUtility.ToJson(list);
        }

        public override void FromJson(string json)
        {
            var list = JsonUtility.FromJson<BTParameterList>(json);
            if (list != null)
            {
                TargetText = list.GetValue<string>("Target");
            }
        }

        public BTMoveNode() : base()
        {
            NodeType = BTNodeType.Move;
            title = "BTMove";
            targetField = new TextField();
            mainContainer.Add(targetField);
        }
    }
}
