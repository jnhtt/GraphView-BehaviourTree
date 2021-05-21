using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTRandom : BTCondition
    {
        public override BTResult Exec(BTData data)
        {
            if (ConnectionNodeList == null || ConnectionNodeList.Count <= 0)
            {
                return BTResult.Failure;
            }    
            int outputCount = ConnectionNodeList.Count;
            int decide = UnityEngine.Random.Range(0, outputCount);
            return ConnectionNodeList[decide].Exec(data);
        }
    }
}
