using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTSequencer : BTBase
    {
        public override BTResult Exec(BTData data)
        {
            foreach (var node in ConnectionNodeList)
            {
                if (node.Exec(data) == BTResult.Failure)
                {
                    return BTResult.Failure;
                }
            }
            return BTResult.Success;
        }
    }
}
