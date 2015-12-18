namespace VanDerWaerdenGame.Model
{
    public interface IGameLogger
    {
        void LogPositionMove(int position);
        void LogColorMove(int color);
        void SaveGame();
    }
}