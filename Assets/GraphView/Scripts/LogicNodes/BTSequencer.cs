using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTSequencer : BTBase
    {
        public override BTStatus Exec(BTData data)
        {
            foreach (var node in ConnectionNodeList)
            {
                if (node.Exec(data) == BTStatus.Failure)
                {
                    return BTStatus.Failure;
                }
            }
            return BTStatus.Success;
        }
    }
}
