using System;
using _Game.Utilities.Scripts;
using UnityEngine;

namespace _Game.Core.Grid.Scripts
{
    [Serializable]
    public class GridData
    {
        public GridData(Vector2 coordinate, GameObject gameObject)
        {
            SetIsEmpty(false);
            SetCoordinate(coordinate);
            SetGameObject(gameObject);

            NeighborGridData = new GridData[4];
        }

        public bool IsEmpty { get; private set; }
        public bool HasNeedFall { get; private set; }
        public Vector2 Coordinate { get; private set; }
        public GameObject GameObject { get; private set; }
        public Vector2 BottomLeft => Coordinate - VectorHelper.HalfSize;
        public Vector2 TopRight => Coordinate + VectorHelper.HalfSize;
        public GridData[] NeighborGridData { get; private set; }

        public void SetNeighborGridData(GridData[] neighborGridData)
        {
            Array.Copy(neighborGridData, NeighborGridData, NeighborGridData.Length);
        }

        public void SetCoordinate(Vector2 coordinate)
        {
            Coordinate = coordinate;
        }

        public void SetIsEmpty(bool isEmpty)
        {
            IsEmpty = isEmpty;
        }

        public void SetHasNeedFall(bool hasNeedFall)
        {
            HasNeedFall = hasNeedFall;
        }

        public void SetGameObject(GameObject gameObject)
        {
            GameObject = gameObject;
        }

        public T GetGridComponents<T>()
        {
            return GameObject.GetComponent<T>();
        }
    }
}