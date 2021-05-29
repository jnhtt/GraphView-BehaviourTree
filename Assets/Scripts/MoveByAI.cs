using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByAI : MonoBehaviour
{
    [SerializeField] private Environment environment;

    private BT.BTData data;
    private BT.BTGraph graph;

    private int frame;

    private void Start()
    {
        data = new BT.BTData();
        data.transformList = environment.pointList;
        data.self = transform;

        graph = BT.BTGraphFactory.Load("test001");

        //
        var go = GameObject.Find("MoveByInput");
        if (go != null)
        {
            data.paramDict.Add("PlayerPos", "MoveByInput");
            data.transformList.Add(go.transform);
        }
    }


    private void Update()
    {
        UpdateBT();
        UpdateBTAction();
    }

    private void UpdateBTAction()
    {
        if (data.runningAction != null)
        {
            data.runningAction.OnUpdate(data);
        }
    }

    private void UpdateBT()
    {
        if (frame % 13 == 0)
        {
            data.runningAction = null;
            graph.Reset();

            graph.Exec(data);
            frame = 1;
        }
        frame++;
    }
}
