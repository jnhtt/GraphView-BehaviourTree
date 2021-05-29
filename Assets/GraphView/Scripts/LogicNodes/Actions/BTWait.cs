using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTWait : BTAction
    {
        public float waitTime;
        private float elapsedTime;

        public override void Reset(bool forceInit = false)
        {
            base.Reset(forceInit);
        }

        public override void OnUpdate(BTData data)
        {
            elapsedTime += Time.deltaTime;
        }

        public override BTStatus Exec(BTData data)
        {
            Debug.LogError("BTWait");
            data.runningAction = this;
            var ret = elapsedTime >= waitTime ? BTStatus.Success : BTStatus.Running;
            if (ret == BTStatus.Success)
            {
                elapsedTime = 0f;
            }
            return ret;
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