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
        public BTMove data;

        public override string ToJson()
        {
#if true
            return data.ToJson();
#else
            var list = new BTParameterList();
            list.ParameterList.Add(new BTParamter() { Name = "Target", Value = TargetText });
            return JsonUtility.ToJson(list);
#endif
        }

        public override void FromJson(string json)
        {
#if true
            data.FromJson(json);
            targetField.value = data.targetName;
#else
            var list = JsonUtility.FromJson<BTParameterList>(json);
            if (list != null)
            {
                TargetText = list.GetValue<string>("Target");
            }
#endif
        }

        public BTMoveNode() : base()
        {
            data = new BTMove();

            NodeType = BTNodeType.Move;
            title = "BTMove";
            targetField = new TextField();
            targetField.RegisterValueChangedCallback(v => data.targetName = v.newValue);
            mainContainer.Add(targetField);
        }
    }
}
