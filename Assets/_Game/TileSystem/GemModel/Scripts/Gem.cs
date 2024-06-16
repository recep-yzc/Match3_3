using _Game.TileSystem.AbilityModel.Blast.Scripts;
using _Game.TileSystem.AbilityModel.Shake.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.TileSystem.GemModel.Scripts
{
    public class Gem : Tile, IGem, IShake, IBlast
    {
        public void Blast()
        {
            Debug.Log("blasted");
        }

        #region Public

        public GemId GemId { get; set; }

        #endregion

        public void SetGemId(GemId gemId)
        {
            GemId = gemId;
        }

        public UniTaskVoid ShakeAsync(ShakeDataSo shakeDataSo)
        {
            return ShakeHelper.Shake(transform, shakeDataSo.duration, shakeDataSo.force, shakeDataSo.animationCurve);
        }
    }
}