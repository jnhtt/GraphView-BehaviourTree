using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTBase
    {
        public BTNodeData Data;
        public List<BTBase> ConnectionNodeList;

        public virtual BTResult Exec()
        {
            return BTResult.Failure;
        }
    }
}
