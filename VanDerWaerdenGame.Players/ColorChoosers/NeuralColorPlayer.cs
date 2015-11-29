using Encog.Engine.Network.Activation;
using Encog.ML.Data.Basic;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanDerWaerdenGame.Model;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace VanDerWaerdenGame.Players.ColorChoosers
{
    public class NeuralColorPlayer : ColorPlayerBase
    {
        public override string PlayerName { get { return "Neural network Player"; } }

        public NeuralColorPlayer()
        {
            ConstructNetwork();
            this.PropertyChanged += NetworkParametersChanged;
        }
        private void NetworkParametersChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(NColors) || e.PropertyName == nameof(ProgressionLength))
                ConstructNetwork();
        }

        public override int GetColor(BoardState board)
        {
            var input = new BasicMLData(board.BoardColors.Select(i => (double)i).Concat(new double[Network.InputCount - board.BoardColors.Count()]).ToArray<double>());
            var color = Network.Winner(input);
            return color;
        }

        public BasicNetwork Network { get; set; }
        private void ConstructNetwork()
        {
            Network = new BasicNetwork();
            Network.AddLayer(new BasicLayer(new ActivationTANH(), true, VanDerWaerdenGameRules.VanDerWaerdenNumber(this.NColors, this.ProgressionLength)));
            Network.AddLayer(new BasicLayer(new ActivationTANH(), true, VanDerWaerdenGameRules.VanDerWaerdenNumber(this.NColors, this.ProgressionLength)));
            Network.AddLayer(new BasicLayer(new ActivationTANH(), true, this.NColors));
            Network.Structure.FinalizeStructure();
            Debug.Print("Created new Network with parameters nColors = {0} and progression length = {1}.", NColors, ProgressionLength);
        }
    }
}
