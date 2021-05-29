using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public abstract class BTBase
    {
        public BTNodeData Data;
        public List<BTBase> ConnectionNodeList;
        public BTStatus Status { get { return status; } set { status = value; } }
        private BTStatus status;

        public BTBase()
        {
            ConnectionNodeList = new List<BTBase>();
        }

        public abstract BTStatus Exec(BTData data, bool traverseRunning);
        public virtual string ToJson()
        {
            return "";
        }
        public virtual void FromJson(string json)
        {

        }
        public virtual void Reset(bool forceInit = false)
        {
            if (forceInit || Status != BTStatus.Running)
            {
                Status = BTStatus.Ready;
            }
        }
    }
}
