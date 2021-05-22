using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace BT
{
    [Serializable]
    public class BTParamter
    {
        public string Name;
        public string Typename;
        public string Value;
    }

    [Serializable]
    public class BTParameterList
    {
        public List<BTParamter> ParameterList = new List<BTParamter>();

        public T GetValue<T>(string name)
        {
            foreach (var t in ParameterList)
            {
                if (t.Name == name)
                {
                    var converter = TypeDescriptor.GetConverter(typeof(T));
                    if (converter != null)
                    {
                        //ConvertFromString(string text)の戻りは object なので T型でキャストする
                        return (T)converter.ConvertFromString(t.Value);
                    }
                }
            }
            return default(T);
        }
    }

    public class BTData
    {
        public Transform self;
        public List<Transform> transformList;

        public Transform Get(string targetName)
        {
            if (transformList == null)
            {
                return null;
            }
            foreach (var trans in transformList)
            {
                if (targetName == trans.name)
                {
                    return trans;
                }
            }
            return null;
        }
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
