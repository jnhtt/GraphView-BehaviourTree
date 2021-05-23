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
            BTStart start = null;
            foreach (var n in btList)
            {
                if (n is BTStart)
                {
                    return start;
                }
            }
            return null;
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
