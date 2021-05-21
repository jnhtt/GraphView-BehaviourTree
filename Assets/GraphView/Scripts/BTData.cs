using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace BT
{
    public class BTData
    {
        public Transform self;
        public List<Transform> transformList;
    }

    [Serializable]
    public class BTNodeData
    {
        public string Guid;
        public BTNodeType NodeType;
        public int Priority;
        public Vector2 Position;

        public string parameterJson;
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
