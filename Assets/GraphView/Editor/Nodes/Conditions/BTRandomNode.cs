using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTRandomNode : BTConditionNode
    {
        public BTRandomNode() : base()
        {
            NodeType = BTNodeType.Random;
            title = "BTRandom";
        }
    }
}
