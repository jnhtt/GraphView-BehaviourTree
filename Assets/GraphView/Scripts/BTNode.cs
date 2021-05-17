using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

namespace BT
{
    public class BTNode : Node
    {
        public string Guid;

        public BTNode()
        {
            title = "BTNode";

            var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(float));
            inputPort.portName = "In";
            inputContainer.Add(inputPort);

            var ouputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            ouputPort.portName = "Out";
            outputContainer.Add(ouputPort);

            Guid = BTNodeFactory.NewGuid();
        }
    }

    [Serializable]
    public class BTNodeData
    {
        public string Guid;
        public Vector2 Position;
    }

    [Serializable]
    public class BTEdgeData
    {
        [SerializeField] public string fromNodeGuid;
        [SerializeField] public string toNodeGuid;
    }

    public class BTGraphDataContainer : ScriptableObject
    {
        public List<BTNodeData> Nodes = new List<BTNodeData>();
        public List<BTEdgeData> Edges = new List<BTEdgeData>();
    }

    public static class BTNodeFactory
    {

        public static string NewGuid()
        {
            return Guid.NewGuid().ToString("N");
        }
    }

    public static class BTGraphSaveUtility
    {
        public static void Save(string filename, GraphView graphView)
        {
            var edges = GetEdges(graphView);
            //if (!edges.Any()) return;

            var graphData = ScriptableObject.CreateInstance<BTGraphDataContainer>();
            foreach (var edge in edges)
            {
                var outputNode = edge.output.node as BTNode;
                var inputNode = edge.input.node as BTNode;

                Debug.LogError(outputNode.Guid + " -> " + inputNode.Guid);
                graphData.Edges.Add(new BTEdgeData()
                {
                    fromNodeGuid = outputNode.Guid,
                    toNodeGuid = inputNode.Guid
                });
            }

            var nodes = GetNodes(graphView);
            foreach (var node in nodes)
            {
                graphData.Nodes.Add(new BTNodeData()
                {
                    Guid = node.Guid,
                    Position = node.GetPosition().position,
                }); ;
            }

            if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            {
                AssetDatabase.CreateFolder("Assets", "Resources");
            }

            AssetDatabase.CreateAsset(graphData, $"Assets/Resources/{filename}.asset");
            AssetDatabase.SaveAssets();
        }

        public static void Load(string filename, GraphView graphView)
        {
            var graphData = Resources.Load<BTGraphDataContainer>(filename);
            if (graphData == null)
            {
                EditorUtility.DisplayDialog("File not found!", $"{filename} does not exist", "OK");
                return;
            }

            ClearGraph(graphView);
            CreateNodes(graphView, graphData);
            CreateEdges(graphView, graphData);
            ApplyExpandedState(graphView, graphData);
        }

        private static List<Edge> GetEdges(GraphView graphView)
        {
            return graphView?.edges.ToList();
        }

        private static List<BTNode> GetNodes(GraphView graphView)
        {
            var list = new List<BTNode>();
            foreach (var n in graphView?.nodes.ToList())
            {
                var bt = n as BTNode;
                if (bt != null)
                {
                    list.Add(bt);
                }
            }
            return list;
        }

        private static void ClearGraph(GraphView graphView)
        {
            graphView.nodes.ToList().ForEach(graphView.RemoveElement);
            graphView.edges.ToList().ForEach(graphView.RemoveElement);
        }

        private static BTNode CreateBTNode(string guid, Vector2 pos)
        {
            var n = new BTNode();
            n.Guid = guid;
            var rect = n.GetPosition();
            rect.position = pos;
            n.SetPosition(rect);

            return n;
        }

        private static void CreateNodes(GraphView graphView, BTGraphDataContainer graphData)
        {
            foreach (var nodeData in graphData.Nodes)
            {
                graphView.AddElement(CreateBTNode(nodeData.Guid, nodeData.Position));
            }
        }

        private static void CreateEdges(GraphView graphView, BTGraphDataContainer graphData)
        {
            var nodes = GetNodes(graphView);
            foreach (var edgeData in graphData.Edges)
            {
                var fromNode = nodes.First(x => x.Guid == edgeData.fromNodeGuid);
                var toNode = nodes.First(x => x.Guid == edgeData.toNodeGuid);
                if (fromNode == null || toNode == null)
                {
                    continue;
                }
                Debug.LogError(edgeData.fromNodeGuid + " -> " + edgeData.toNodeGuid);

                var inputPort = GetT<Port>(toNode.inputContainer);
                var outputPort = GetT<Port>(fromNode.outputContainer);
                var edge = ConnectPorts(inputPort, outputPort);
                graphView.Add(edge);
            }
        }

        private static T GetT<T>(VisualElement elem) where T : VisualElement
        {
            foreach (var el in elem.Children())
            {
                if (el is T)
                {
                    return el as T;
                }
            }
            return default(T);
        }

        private static Edge ConnectPorts(Port input, Port output)
        {
            var tempEdge = new Edge() { input = input, output = output };
            tempEdge.input.Connect(tempEdge);
            tempEdge.output.Connect(tempEdge);
            return tempEdge;
        }

        private static void ApplyExpandedState(GraphView graphView, BTGraphDataContainer graphData)
        {

        }
    }
}
