using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTSelector : BTBase
    {
        public override BTStatus Exec(BTData data)
        {
            foreach (var node in ConnectionNodeList)
            {
                if (node.Exec(data) == BTStatus.Success)
                {
                    return BTStatus.Success;
                }
            }
            return BTStatus.Failure;
        }
    }

}