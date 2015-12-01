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
    public class NeuralPositionPlayer1 : PositionPlayerBase, ITrainable
    {
        public override string PlayerName { get { return "Neural network Player"; } }

        public NeuralPositionPlayer1()
        {
            this.Network = ConstructNetwork();
            this.PropertyChanged += NetworkParametersChanged;
        }
        private void NetworkParametersChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(NColors) || e.PropertyName == nameof(ProgressionLength))
                this.Network = ConstructNetwork();
        }

        public override int GetPosition(int[] board)
        {
            var input = new BasicMLData(board.Select(x => (double)x).Concat(new double[Network.InputCount - board.Count()]).ToArray<double>());
            var position = (Network.Compute(input)[0]+1)/2;
            return (int)(position*board.Count());
        }

        public BasicNetwork Network { get; set; }
        private BasicNetwork ConstructNetwork()
        {
            var network = new BasicNetwork();
            network.AddLayer(new BasicLayer(new ActivationTANH(), true, VanDerWaerdenGameRules.VanDerWaerdenNumber(this.NColors, this.ProgressionLength) - 1));
            network.AddLayer(new BasicLayer(new ActivationTANH(), true, VanDerWaerdenGameRules.VanDerWaerdenNumber(this.NColors, this.ProgressionLength)));
            network.AddLayer(new BasicLayer(new ActivationTANH(), true, 1));
            network.Structure.FinalizeStructure();
            return network;
            Debug.Print("Created new Network with parameters nColors = {0} and progression length = {1}.", NColors, ProgressionLength);
        }
    }
}
