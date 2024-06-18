namespace _Game.TileSystem.AbilityModel.Blast.Scripts
{
    public interface IBlast
    {
        public BlastId BlastId { get; set; }
        public void SetBlastId(BlastId blastId);
        public void Blast();
    }
}