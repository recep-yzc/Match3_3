using System.Collections.Generic;
using _Game.Level.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.Core.Elements.Element.Scripts
{
    [DefaultExecutionOrder(-2)]
    public abstract class ElementFactoryBase : MonoBehaviour, IElementFactory
    {
        protected abstract void Start();
        public abstract GameObject Create(LevelGridData levelGridData);

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

        #region Parameters

        [Inject] private DiContainer _diContainer;
        protected ElementId ElementId { get; set; }

        #endregion
    }
}