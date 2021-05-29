using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTSelector : BTBase
    {
        public override BTStatus Exec(BTData data, bool traverseRunning)
        {
            ConnectionNodeList.Sort((a, b) => b.Data.Priority - a.Data.Priority);
            Status = BTStatus.Failure;
            foreach (var node in ConnectionNodeList)
            {
                if (node.Status == BTStatus.Running)
                {
                    Status = node.Exec(data, traverseRunning);
                }
                else if (node.Exec(data, traverseRunning) == BTStatus.Success)
                {
                    Status = BTStatus.Success;
                }

                if (Status == BTStatus.Success)
                {
                    return Status;
                }
            }
            return Status;
        }
    }

}