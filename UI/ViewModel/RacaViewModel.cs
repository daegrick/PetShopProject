using BLL.Controle;
using DTO;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace UI.ViewModel
{
    public class RacaViewModel : ViewModelBase
    {
        #region Setters
        private ObservableCollection<Raca> racas;
        private Raca selectedRaca;
        private Guid selectedRacaIde;
        private int selectedRacaCodigo;
        private string? selectedRacaNome;
        public string SelectedRacaNome
        {
            get => selectedRacaNome ?? string.Empty;
            set => OnPropertyChanged(ref selectedRacaNome, value, nameof(SelectedRacaNome));
        }


        public int SelectedRacaCodigo
        {
            get => selectedRacaCodigo;
            set => OnPropertyChanged(ref selectedRacaCodigo, value, nameof(SelectedRacaCodigo));
        }


        public Guid SelectedRacaIde
        {
            get => selectedRacaIde;
            set => OnPropertyChanged(ref selectedRacaIde, value, nameof(SelectedRacaIde));
        }


        public Raca SelectedRaca
        {
            get => selectedRaca;
            set {
                if (selectedRaca != value)
                {
                    selectedRaca = value;
                    SetSelectedRacaValues(value);
                    RaisePropertyChange(nameof(SelectedRaca));
                }
            }
        }

        public ObservableCollection<Raca> Racas
        {
            get => racas;
            set => OnPropertyChanged(ref racas, value, nameof(Racas));
        }
        #endregion

        private void SetSelectedRacaValues(Raca selectedRaca)
        {
            if (selectedRaca is null)
            {
                SelectedRacaCodigo = 0;
                SelectedRacaNome = string.Empty;
                SelectedRacaIde = Guid.Empty;
            }
            else
            {
                SelectedRacaCodigo = selectedRaca.Codigo;
                SelectedRacaIde = selectedRaca.Ide;
                SelectedRacaNome = selectedRaca.Nome;
            }
        }

        private ICommand insereRacaCommand;
        private ICommand buscaRacaCommand;
        private ICommand novaRacaCommand;
        public ICommand NovaRacaCommand => novaRacaCommand ?? (novaRacaCommand = new RelayCommand(NovaRaca, canExecute => true));
        

        public ICommand BuscaRacaCommand => buscaRacaCommand ?? (buscaRacaCommand = new RelayCommand(BuscaRaca, canExecute => true));
        public ICommand InsereRacaCommand => insereRacaCommand ?? (insereRacaCommand = new RelayCommand(InsereRaca, canExecute => true));

        public void NovaRaca(object o)
        {
            Racas.Add(new Raca());
            SelectedRaca = Racas.Last();
        }
        public void BuscaRaca(object o)
        {
            Racas.Clear();
            foreach (var raca in RacaBLL.Busca())
            {
                Racas.Add(raca);
            }
        }
        public void InsereRaca(object o)
        {
            Raca raca = new Raca()
            {
                Codigo = SelectedRacaCodigo,
                Nome = SelectedRacaNome,
                Ide = SelectedRacaIde
            };
            MessageBox.Show(RacaBLL.Insere(raca));
            RaisePropertyChange(nameof(Racas));
        }

        public RacaViewModel()
        {
            Racas = new ObservableCollection<Raca>();
            
            SelectedRaca = null;
            BuscaRaca(null);
        }
    }
}