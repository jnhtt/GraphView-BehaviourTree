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
                    NodeType = node.NodeType,
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

        private static BTNode CreateBTNode(string guid, BTNodeType nodeType, Vector2 pos)
        {
            var n = BTNodeEditorFactory.CreateNode(guid, nodeType, pos);
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
                graphView.AddElement(CreateBTNode(nodeData.Guid, nodeData.NodeType, nodeData.Position));
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

    public static class BTNodeEditorFactory
    {
        public static BTNode CreateNode(string guid, BTNodeType nodeType, Vector2 pos)
        {
            BTNode node = null;
            switch (nodeType)
            {
                case BTNodeType.Start:
                    node = new BTStartNode() { Guid = guid, NodeType = nodeType };
                    break;
                case BTNodeType.Action:
                    node = new BTActionNode() { Guid = guid, NodeType = nodeType };
                    break;
                case BTNodeType.Decorator:
                    node = new BTDecoratorNode() { Guid = guid, NodeType = nodeType };
                    break;
                case BTNodeType.Selector:
                    node = new BTSelectorNode() { Guid = guid, NodeType = nodeType };
                    break;
                case BTNodeType.Sequencer:
                    node = new BTSequencerNode() { Guid = guid, NodeType = nodeType };
                    break;
            }
            return node;
        }

        public static string NewGuid()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}