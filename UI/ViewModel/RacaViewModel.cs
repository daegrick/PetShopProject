using BLL.Controle;
using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using UI.Extras;

namespace UI.ViewModel
{
    public class RacaViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Setters
        private ObservableCollection<Raca> racas;
        private Raca selectedRaca;
        private Guid selectedRacaIde;
        private int selectedRacaCodigo;
        private string? selectedRacaNome;
        private readonly List<string> errors;
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
                    RaisePropertyChange(nameof(CanEditar));
                }
            }
        }

        public ObservableCollection<Raca> Racas
        {
            get => racas;
            set => OnPropertyChanged(ref racas, value, nameof(Racas));
        }
        #endregion

        #region Commands
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
        public ICommand InsereRacaCommand => insereRacaCommand ?? (insereRacaCommand = new RelayCommand(InsereRaca, canExecute => CanInserirRaca));
        #endregion

        #region Validation
        public bool CanEditar => SelectedRaca != null;
        public bool CanInserirRaca
        {
            get {
                errors.Clear();
                errors.UniqueIfNotEmpty(this[nameof(SelectedRacaNome)]);
                return errors.Count == 0;
            }
        }
        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get {
                switch (columnName)
                {
                    case (nameof(SelectedRacaNome)):
                        if (!Regex.IsMatch(SelectedRacaNome, @"^[\p{L}\p{M}' \.\-]+$"))
                            return "Entre com um nome válido!";
                        break;
                }
                return string.Empty;
            }
        }
        #endregion

        #region Methods
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

        #endregion


        public RacaViewModel()
        {
            Racas = new ObservableCollection<Raca>();
            errors = new();
            SelectedRaca = null;
            BuscaRaca(null);
        }
    }
}