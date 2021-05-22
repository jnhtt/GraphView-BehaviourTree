using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTWait : BTAction
    {
        public float waitTime;

        public override BTResult Exec(BTData data)
        {
            return BTResult.Success;
        }

        public override string ToJson()
        {
            var list = new BTParameterList();
            list.ParameterList.Add(new BTParamter() { Name = "Wait", Value = waitTime.ToString() });
            return JsonUtility.ToJson(list);
        }

        public override void FromJson(string json)
        {
            var list = JsonUtility.FromJson<BTParameterList>(json);
            if (list != null)
            {
                waitTime = list.GetValue<float>("Wait");
            }
        }
    }

}