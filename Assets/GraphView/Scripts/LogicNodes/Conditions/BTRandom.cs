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
                return BTStatus.Failure;
            }    
            int outputCount = ConnectionNodeList.Count;
            int decide = UnityEngine.Random.Range(0, outputCount);
            return ConnectionNodeList[decide].Exec(data);
        }
    }
}
