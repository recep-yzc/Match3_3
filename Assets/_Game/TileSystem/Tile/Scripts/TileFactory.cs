using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Game.TileSystem.Tile.Scripts
{
    public abstract class TileFactory : MonoBehaviour, ITileFactory
    {
        #region Parameters

        [Inject] private DiContainer _diContainer;

        #endregion

        protected abstract void Start();
        public abstract GameObject Create(Level.Scripts.TileData tileData);

        protected GameObject GetGameObjectInPool(ref List<GameObject> createdGameObjectList, GameObject prefab)
        {
            var find = createdGameObjectList.Find(x => !x.activeInHierarchy);
            if (find != null)
            {
                find.SetActive(true);
                return find;
            }

            find = _diContainer.InstantiatePrefab(prefab);
            createdGameObjectList.Add(find);

            return find;
        }
    }
}