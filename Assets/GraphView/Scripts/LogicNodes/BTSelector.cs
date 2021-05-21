using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTSelector : BTBase
    {
        public override BTResult Exec(BTData data)
        {
            foreach (var node in ConnectionNodeList)
            {
                if (node.Exec(data) == BTResult.Success)
                {
                    return BTResult.Success;
                }
            }
            return BTResult.Failure;
        }
    }

}