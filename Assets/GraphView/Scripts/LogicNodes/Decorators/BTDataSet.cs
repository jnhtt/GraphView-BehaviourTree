using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTDataSet : BTDecorator
    {
        public string key = "";
        public string value = "";

        public override BTStatus Exec(BTData data)
        {
            if (ConnectionNodeList == null || ConnectionNodeList.Count <= 0)
            {
                return BTStatus.Failure;
            }

            data.SetValue(key, value);
            return ConnectionNodeList[0].Exec(data);
        }

        public override string ToJson()
        {
            var list = new BTParameterList();
            list.ParameterList.Add(new BTParamter() { Name = "Key", Value = key });
            list.ParameterList.Add(new BTParamter() { Name = "Value", Value = value });
            return JsonUtility.ToJson(list);
        }

        public override void FromJson(string json)
        {
            var list = JsonUtility.FromJson<BTParameterList>(json);
            if (list != null)
            {
                key = list.GetValue<string>("Key");
                value = list.GetValue<string>("Value");
            }
        }
    }
}
