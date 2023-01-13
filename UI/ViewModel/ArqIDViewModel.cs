using BLL.Controle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UI.Extras;

namespace UI.ViewModel
{
    public class ArqIDViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Actions
        public Action CloseAction;
        #endregion

        #region Setters
        private string _servidor;
        private int _porta;
        private string _usuario;
        private readonly List<string> _errors;
        private const string arqIdFilePath = "ArqId.txt";
        private readonly StringBuilder _stringBuilder;
        public bool CanSalvar { get; private set; }

        public string Usuario
        {
            get => _usuario ??= string.Empty;
            set => OnPropertyChanged(ref _usuario, value, nameof(Usuario));
        }
        public int Porta
        {
            get => _porta;
            set => OnPropertyChanged(ref _porta, value, nameof(Porta));
        }
        public string Servidor
        {
            get => _servidor ??= string.Empty;
            set => OnPropertyChanged(ref _servidor, value, nameof(Servidor));
        }
        #endregion

        #region Commands
        private ICommand _salvarCommand; private ICommand _testarServidorCommand;

        public ICommand TestarServidorCommand => _testarServidorCommand ??= (_testarServidorCommand = new RelayCommand(TestarServidor, canExecute => CanProceed));
        public ICommand SalvarCommand => _salvarCommand ??= (_salvarCommand = new RelayCommand(Salvar, canExecute => CanSalvar));

        #endregion

        #region Methods
        public void Salvar(object o)
        {
            try
            {
                File.WriteAllText(arqIdFilePath,GeraDadosConexao(o));
                CloseAction();
            }catch(NullReferenceException ex) {
            
            }
        }
        public void TestarServidor(object o)
        {
            try
            {
                PasswordBox pbox = o as PasswordBox;
                string mensagem = string.Empty;
                CanSalvar = ArqIDBLL.TestaConexao(GeraDadosConexao(o), out mensagem);
                MessageBox.Show(mensagem);
                RaisePropertyChange(nameof(CanSalvar));
                if(!CanSalvar)
                {
                    return;
                }
                pbox.IsEnabled = false;
            }
            catch (NullReferenceException ex)
            {

            }
        }

        private string GeraDadosConexao(object o)
        {
            PasswordBox pbox = o as PasswordBox;
            _stringBuilder.Clear();
            _stringBuilder.Append(Servidor);
            _stringBuilder.AppendLine(Porta > 0 ? $", {Porta}" : string.Empty);
            _stringBuilder.AppendLine(Usuario);
            _stringBuilder.AppendLine(pbox.Password);
            _stringBuilder.AppendLine("30");
            return _stringBuilder.ToString();
        }

        #endregion

        #region Validation
        public bool CanProceed
        {
            get {
                _errors.Clear();
                _errors.UniqueIfNotEmpty(this[nameof(Servidor)]);
                _errors.UniqueIfNotEmpty(this[nameof(Porta)]);
                _errors.UniqueIfNotEmpty(this[nameof(Usuario)]);
                return _errors.Count == 0;
            }
        }
        public string this[string columnName]
        {
            get {
                switch (columnName)
                {
                    case nameof(Servidor):
                        if(string.IsNullOrEmpty(Servidor))
                            return "Entre com um servidor!";
                        break;
                        case nameof(Porta):
                        if (Porta < 0)
                            return "Entre com uma porta válida, ou deixe como 0 caso não use uma porta!";
                        break;
                        case nameof(Usuario):
                        if (string.IsNullOrEmpty(Usuario))
                            return "Entre com um usuário!";
                        break;
                    default:
                        break;
                }
                return string.Empty;
            }
        }
        public string Error => throw new System.NotImplementedException();
        #endregion

        #region Constructor
        public ArqIDViewModel()
        {
            _errors = new List<string>();
            _stringBuilder= new StringBuilder();
        }
        #endregion
    }
}