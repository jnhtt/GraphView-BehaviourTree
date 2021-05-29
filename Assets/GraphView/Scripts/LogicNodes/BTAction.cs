using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public abstract class BTAction : BTBase
    {
        public abstract void OnUpdate(BTData data);
    }

}