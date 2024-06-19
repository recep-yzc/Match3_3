using _Game.TileSystem.AbilityModel.Blast.Scripts;
using _Game.TileSystem.AbilityModel.Fall.Scripts;
using _Game.TileSystem.AbilityModel.ScaleUpDown.Scripts;
using _Game.TileSystem.AbilityModel.Shake.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Game.TileSystem.GemModel.Scripts
{
    public class Gem : Tile, IGem, IShake, IFall, IBlast
    {
        #region Public

        public GemId GemId { get; set; }

        #endregion

        public void SetGemId(GemId gemId)
        {
            GemId = gemId;
        }

        public void Blast()
        {
            gameObject.SetActive(false);
        }

        public UniTaskVoid ShakeAsync(ShakeDataSo shakeDataSo)
        {
            return ShakeHelper.Handle(transform, shakeDataSo.duration, shakeDataSo.force, shakeDataSo.animationCurve);
        }

        public void Fall(Vector2 position)
        {
            transform.DOMove(position, 0.3f);
        }
    }
}