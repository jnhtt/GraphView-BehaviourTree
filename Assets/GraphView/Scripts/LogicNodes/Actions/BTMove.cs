using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTMove : BTAction
    {
        public string targetName;
        public override BTStatus Exec(BTData data)
        {
            return BTStatus.Success;
        }

        public override string ToJson()
        {
            var list = new BTParameterList();
            list.ParameterList.Add(new BTParamter() { Name = "Target", Value = targetName });
            return JsonUtility.ToJson(list);
        }

        public override void FromJson(string json)
        {
            var list = JsonUtility.FromJson<BTParameterList>(json);
            if (list != null)
            {
                targetName = list.GetValue<string>("Target");
            }
        }
    }

}