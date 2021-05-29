using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTStart : BTBase
    {
        public override BTStatus Exec(BTData data)
        {
            if (ConnectionNodeList.Count == 1)
            {
                Debug.Log("BTStart");
                return ConnectionNodeList[0].Exec(data);
            }
            return BTStatus.Failure;
        }
    }
}