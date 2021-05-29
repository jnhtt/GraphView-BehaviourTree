using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTGraph
    {
        private BTStart start;
        private List<BTBase> btList;

        public void Init(List<BTBase> list)
        {
            btList = list;
            start = GetStart();
        }

        public BTStart GetStart()
        {
            BTStart s = null;
            foreach (var n in btList)
            {
                s = n as BTStart;
                if (s != null)
                {
                    return s;
                }
            }
            return null;
        }

        public void Reset(bool forceReset = false)
        {
            foreach (var n in btList)
            {
                n.Reset(forceReset);
            }
        }

        public void Exec(BTData data)
        {
            if (start != null)
            {
                start.Exec(data);
            }
        }
    }
}
