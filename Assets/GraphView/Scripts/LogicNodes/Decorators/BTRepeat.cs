using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTRepeat : BTDecorator
    {
        public int repeat = 0;
        private int count = 0;

        public override BTStatus Exec(BTData data, bool traverseRunning)
        {
            if (ConnectionNodeList == null || ConnectionNodeList.Count <= 0)
            {
                return BTStatus.Failure;
            }

            var st = count < repeat ? BTStatus.Running : BTStatus.Success;
            count++;
            return st;
        }

        public override string ToJson()
        {
            var list = new BTParameterList();
            list.ParameterList.Add(new BTParamter() { Name = "Repeat", Value = repeat.ToString() });
            return JsonUtility.ToJson(list);
        }

        public override void FromJson(string json)
        {
            var list = JsonUtility.FromJson<BTParameterList>(json);
            if (list != null)
            {
                repeat = list.GetValue<int>("Repeat");
            }
        }
    }
}
