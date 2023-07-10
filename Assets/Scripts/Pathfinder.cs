using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pathfinder
{
    public class Pathfinder<node>
    {
        int _calculatorPatience = 9999;
        public Func<node, node, float> GetHeuristicDistance;
        public Func<node, Dictionary<node, float>> GetNeighboursAndStepCosts;

        struct nodeData
        {
            public float g;
            public float h;

            public float f { get => g + h; }
        }


        public bool GeneratePath(node startNode,  node endNode, out List<node> path)
        {
            int _patience = _calculatorPatience;
            HashSet<node> CLOSED = new HashSet<node>();
            Dictionary<node, nodeData> OPEN = new Dictionary<node, nodeData> { { startNode, new nodeData { g = 0f, h = GetHeuristicDistance(startNode, endNode) } } };
            Dictionary<node, node> DIRECTIONS = new Dictionary<node, node>();


            while (_patience > 0)
            {
                _patience--;
                if (OPEN.Count == 0) break;
                node _currentNode = OPEN.Aggregate((l,r) => l.Value.f < r.Value.f ? l : r).Key;
                nodeData _currentNodeData = OPEN[_currentNode];

                OPEN.Remove(_currentNode);
                CLOSED.Add(_currentNode);

                if (_currentNode.Equals(endNode))
                {
                    List<node> _finalPath = new List<node>();
                    node _tracebackStep = _currentNode;
                    while (!_tracebackStep.Equals(startNode))
                    {
                        _finalPath.Add(_tracebackStep);
                        _tracebackStep = DIRECTIONS[_tracebackStep];
                    }
                    _finalPath.Reverse();
                    path = _finalPath;
                    return true;
                }

                foreach (KeyValuePair<node, float> neighbourDistancePair in GetNeighboursAndStepCosts(_currentNode))
                {
                    node _neighbour = neighbourDistancePair.Key;
                    float _currentToNeighbourDistance = neighbourDistancePair.Value;
                    if (CLOSED.Contains(_neighbour)) continue;
                    float _starToNeighbourDistance = _currentToNeighbourDistance + _currentNodeData.g;

                    if (!OPEN.ContainsKey(_neighbour) || OPEN[_neighbour].g > _starToNeighbourDistance)
                    {
                        DIRECTIONS[_neighbour] = _currentNode;
                        float _targetToNeighbourDistance = GetHeuristicDistance(_neighbour, endNode);
                        if (!OPEN.ContainsKey(_neighbour))
                        {
                            OPEN.Add(_neighbour, new nodeData { g = _starToNeighbourDistance, h = _targetToNeighbourDistance });
                        }
                        else
                        {
                            OPEN[_neighbour] = new nodeData { g = _starToNeighbourDistance, h = _targetToNeighbourDistance };
                        }
                    }

                }
            }
            path = new List<node>();
            return false;
        }
    }
}

