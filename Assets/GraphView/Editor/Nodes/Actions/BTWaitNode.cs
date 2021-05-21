using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;

namespace BT
{
    public class BTWaitNode : BTActionNode
    {
        private FloatField waitField;
        public float WaitFloat {
            get { return waitField.value; }
            set { waitField.value = value; }
        }

        public override string ToJson()
        {
            var list = new BTParameterList();
            list.ParameterList.Add(new BTParamter() { Name = "Wait", Value = WaitFloat.ToString() });
            return JsonUtility.ToJson(list);
        }

        public override void FromJson(string json)
        {
            var list = JsonUtility.FromJson<BTParameterList>(json);
            if (list != null)
            {
                WaitFloat = list.GetValue<float>("Wait");
            }
        }

        public BTWaitNode() : base()
        {
            NodeType = BTNodeType.Move;
            title = "BTWait";
            waitField = new FloatField();
            mainContainer.Add(waitField);
        }
    }
}
