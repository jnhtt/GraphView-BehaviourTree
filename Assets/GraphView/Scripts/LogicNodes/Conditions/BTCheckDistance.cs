using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public enum CheckTarget : int
    {
        Point,
        Character,
    }
    public enum CheckType : int
    {
        Less,
        LessEqual,
        Greater,
        GreaterEqual,
        Equal,
        NotEqual,
    }

    public class BTCheckDistance : BTCondition
    {
        public CheckType checkType = CheckType.Less;
        public string targetName;
        public float distance = 0f;

        public override BTStatus Exec(BTData data)
        {
            if (ConnectionNodeList == null || ConnectionNodeList.Count <= 0)
            {
                return BTStatus.Failure;
            }
            var target = data.Get(targetName);
            if (target == null)
            {
                return BTStatus.Failure;
            }

            float dist = Vector3.Distance(target.position, data.self.position);
            // true -> 0, false -> 1
            if (CheckDistance(dist))
            {
                return ConnectionNodeList[0].Exec(data);
            }
            else if (ConnectionNodeList.Count == 1 || ConnectionNodeList[1] == null)
            {
                return BTStatus.Failure;
            }
            else
            {
                return ConnectionNodeList[1].Exec(data);
            }
        }

        private bool CheckDistance(float dist)
        {
            switch (checkType)
            {
                case CheckType.Equal:
                    return dist == distance;
                case CheckType.NotEqual:
                    return dist != distance;
                case CheckType.Less:
                    return dist < distance;
                case CheckType.LessEqual:
                    return dist <= distance;
                case CheckType.Greater:
                    return dist > distance;
                case CheckType.GreaterEqual:
                    return dist >= distance;
                default:
                    return false;
            }
        }

        public override string ToJson()
        {
            var list = new BTParameterList();
            list.ParameterList.Add(new BTParamter() { Name = "CheckType", Value = ((int)checkType).ToString() });
            list.ParameterList.Add(new BTParamter() { Name = "TargetName", Value = targetName });
            list.ParameterList.Add(new BTParamter() { Name = "Distance", Value = distance.ToString() });
            return JsonUtility.ToJson(list);
        }

        public override void FromJson(string json)
        {
            var list = JsonUtility.FromJson<BTParameterList>(json);
            if (list != null)
            {
                checkType = (CheckType)list.GetValue<int>("CheckType");
                targetName = list.GetValue<string>("TargetName");
                distance = list.GetValue<float>("Distance");
            }
        }
    }
}
