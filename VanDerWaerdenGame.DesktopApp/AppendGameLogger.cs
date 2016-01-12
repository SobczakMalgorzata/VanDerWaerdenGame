using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanDerWaerdenGame.Model;

namespace VanDerWaerdenGame.DesktopApp
{
    public class AppendGameLogger : IGameLogger
    {
        public string FileName{ get; private set; }
        private List<int> movesHistory= new List<int>();
        public AppendGameLogger(string fileName)
        {
            this.FileName = fileName;
        }
        public void LogColorMove(int color)
        {
            movesHistory.Add(color);
        }

        public void LogPositionMove(int position)
        {
            movesHistory.Add(position);
        }

        public void SaveGame()
        {
            if(movesHistory.Count > 0)
                File.AppendAllLines(FileName, new string[] {Math.Ceiling(movesHistory.Count/2.0) + ";" + string.Join(";", movesHistory)} );
            movesHistory.Clear();
        }
    }
}
