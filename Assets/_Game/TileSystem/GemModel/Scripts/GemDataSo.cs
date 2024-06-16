using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace _Game.TileSystem.GemModel.Scripts
{
    [CreateAssetMenu(fileName = "GemDataSo", menuName = "Game/Gem/Data")]
    public class GemDataSo : ScriptableObjectInstaller<GemDataSo>
    {
        [Header("SpriteReference")] public List<GemSpriteData> gemSpriteData = new();

        public Sprite GetSpriteByGemId(GemId gemId)
        {
            return gemSpriteData.FirstOrDefault(x => x.gemId == gemId).sprite;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(this);
        }
    }
}