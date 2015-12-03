using Encog.Engine.Network.Activation;
using Encog.ML.Data.Basic;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanDerWaerdenGame.Model;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace VanDerWaerdenGame.Players.PositionChoosers
{
    public class NeuralPositionPlayer1 : PositionPlayerBase
    {
        public override string PlayerName { get { return "Neural network Player"; } }

        public NeuralPositionPlayer1(VanDerWaerdenGameRules rules) : base(rules)
        {
            ConstructNetwork();
        }

        protected override void OnNColorsChanged(int newValue)              { ConstructNetwork(); }
        protected override void OnProgressionLengthChanged(int newValue)    { ConstructNetwork(); }
        
        public override int GetPosition(int[] board)
        {
            var input = new BasicMLData(Array.ConvertAll(board, x => (double)x));
            int position = Network.Winner(input);
            return position;
        }

        public BasicNetwork Network { get; set; }
        private void ConstructNetwork()
        {
            Network = new BasicNetwork();
            Network.AddLayer(new BasicLayer(new ActivationTANH(), true, VanDerWaerdenGameRules.VanDerWaerdenNumber(this.NColors, this.ProgressionLength)));
            Network.AddLayer(new BasicLayer(new ActivationTANH(), true, VanDerWaerdenGameRules.VanDerWaerdenNumber(this.NColors, this.ProgressionLength)));
            Network.AddLayer(new BasicLayer(new ActivationTANH(), true, VanDerWaerdenGameRules.VanDerWaerdenNumber(this.NColors, this.ProgressionLength)));
            Network.Structure.FinalizeStructure();
            Debug.Print("Created new Network with parameters nColors = {0} and progression length = {1}.", NColors, ProgressionLength);
        }
    }
}
