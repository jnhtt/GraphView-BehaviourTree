using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTRandom : BTCondition
    {
        public override BTStatus Exec(BTData data)
        {
            if (ConnectionNodeList == null || ConnectionNodeList.Count <= 0)
            {
                Debug.Log("BTRandom : Failure");
                return BTStatus.Failure;
            }    
            int outputCount = ConnectionNodeList.Count;
            int decide = UnityEngine.Random.Range(0, outputCount);
            Debug.Log("BTRandom : decide = " + decide);
            return ConnectionNodeList[decide].Exec(data);
        }

        public override string ToJson()
        {
            var list = new BTParameterList();
            return JsonUtility.ToJson(list);
        }

        public override void FromJson(string json)
        {
            var list = JsonUtility.FromJson<BTParameterList>(json);
            if (list != null)
            {
            }
        }
    }
}
