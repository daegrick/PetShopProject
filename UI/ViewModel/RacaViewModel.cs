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
        private ObservableCollection<Raca> _racas;
        private Raca _selectedRaca;
        private Guid _selectedRacaIde;
        private int _selectedRacaCodigo;
        private string? _selectedRacaNome;
        private readonly List<string> _errors;
        public string SelectedRacaNome
        {
            get => _selectedRacaNome ?? string.Empty;
            set => OnPropertyChanged(ref _selectedRacaNome, value, nameof(SelectedRacaNome));
        }
        public int SelectedRacaCodigo
        {
            get => _selectedRacaCodigo;
            set => OnPropertyChanged(ref _selectedRacaCodigo, value, nameof(SelectedRacaCodigo));
        }
        public Guid SelectedRacaIde
        {
            get => _selectedRacaIde;
            set => OnPropertyChanged(ref _selectedRacaIde, value, nameof(SelectedRacaIde));
        }
        public Raca SelectedRaca
        {
            get => _selectedRaca;
            set {
                if (_selectedRaca != value)
                {
                    _selectedRaca = value;
                    SetSelectedRacaValues(value);
                    RaisePropertyChange(nameof(SelectedRaca));
                    RaisePropertyChange(nameof(CanEditar));
                }
            }
        }
        public ObservableCollection<Raca> Racas
        {
            get => _racas;
            set => OnPropertyChanged(ref _racas, value, nameof(Racas));
        }
        #endregion

        #region Commands
        private ICommand _insereRacaCommand;
        private ICommand _buscaRacaCommand;
        private ICommand _novaRacaCommand;

        public ICommand NovaRacaCommand => _novaRacaCommand ??= new RelayCommand(NovaRaca, canExecute => true);
        public ICommand BuscaRacaCommand => _buscaRacaCommand ??= new RelayCommand(BuscaRaca, canExecute => true);
        public ICommand InsereRacaCommand => _insereRacaCommand ??= new RelayCommand(InsereRaca, canExecute => CanInserirRaca);
        #endregion

        #region Validation
        public bool CanEditar => SelectedRaca != null;
        public bool CanInserirRaca
        {
            get {
                _errors.Clear();
                _errors.UniqueIfNotEmpty(this[nameof(SelectedRacaNome)]);
                return _errors.Count == 0;
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
            BuscaRaca(null);
        }

        #endregion

        #region Constructor
        public RacaViewModel()
        {
            Racas = new ObservableCollection<Raca>();
            _errors = new();
            SelectedRaca = null;
            BuscaRaca(null);
        }
        #endregion

    }
}