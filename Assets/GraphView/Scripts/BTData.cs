using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace BT
{
    [Serializable]
    public class BTNodeData
    {
        public string Guid;
        public BTNodeType NodeType;
        public Vector2 Position;
    }

    [Serializable]
    public class BTEdgeData
    {
        [SerializeField] public string fromNodeGuid;
        [SerializeField] public string toNodeGuid;
    }

    public static class BTNodeFactory
    {
        public static string NewGuid()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
