using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public abstract class BTBase
    {
        public BTNodeData Data;
        public List<BTBase> ConnectionNodeList;

        public abstract BTResult Exec(BTData data);
    }
}
