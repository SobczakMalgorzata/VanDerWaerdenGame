using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanDerWaerdenGame.Model;
using System.ComponentModel;

namespace VanDerWaerdenGame.Players
{
    public abstract class PlayerBase : BindableBase, IDisposable
    {
        public PlayerBase(VanDerWaerdenGameRules rules)
        {
            Rules = rules;
            Rules.PropertyChanged += OnGameRulesPropertiesChanged;
        }

        public abstract string PlayerName { get; }

        public VanDerWaerdenGameRules Rules {get; set;}
        public int NColors { get { return Rules.NColors; } }
        public int ProgressionLength { get { return Rules.EndGameProgressionLength; } }


        private void OnGameRulesPropertiesChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Rules.NColors))
                OnNColorsChanged(Rules.NColors);
            if (e.PropertyName == nameof(Rules.EndGameProgressionLength))
                OnProgressionLengthChanged(Rules.EndGameProgressionLength);
        }
        protected virtual void OnNColorsChanged(int newValue) { }
        protected virtual void OnProgressionLengthChanged(int newValue) { }

        public void Dispose()
        {
            Rules.PropertyChanged -= OnGameRulesPropertiesChanged;
        }
    }
}
