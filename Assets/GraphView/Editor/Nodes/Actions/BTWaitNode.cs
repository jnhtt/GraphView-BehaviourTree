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
        public BTWait data;

        public float WaitFloat {
            get { return data.waitTime; }
            set { data.waitTime = value; }
        }

        public override string ToJson()
        {
#if true
            return data.ToJson();
#else
            var list = new BTParameterList();
            list.ParameterList.Add(new BTParamter() { Name = "Wait", Value = data.waitTime.ToString() });
            return JsonUtility.ToJson(list);
#endif
        }

        public override void FromJson(string json)
        {
#if true
            data.FromJson(json);
            waitField.value = data.waitTime;
#else
            var list = JsonUtility.FromJson<BTParameterList>(json);
            if (list != null)
            {
                data.waitTime = list.GetValue<float>("Wait");
                waitField.value = data.waitTime;
            }
#endif
        }

        public BTWaitNode() : base()
        {
            data = new BTWait();

            NodeType = BTNodeType.Wait;
            title = "BTWait";
            waitField = new FloatField();
            waitField.RegisterValueChangedCallback(v => data.waitTime = v.newValue);
            mainContainer.Add(waitField);
        }
    }
}
