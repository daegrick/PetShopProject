using PetShopDesafeto;
using System;

namespace UI.Services
{
    public class WindowService : IWindowService
    {
        public void AbrirTelaAdocao(object dataContext)
        {
            new AdocaoUI() { DataContext= dataContext }.ShowDialog();
        }

        public void AbrirTelaBuscaPessoa(object dataContext)
        {
            new PessoaSearchUI() { DataContext= dataContext }.ShowDialog();
        }

        public void AbrirTelaBuscaPet(object dataContext)
        {
            new PetSearchUI() { DataContext= dataContext }.ShowDialog();
        }

        public void AbrirTelaPessoa(object dataContext)
        {
            new PessoaUI() { DataContext= dataContext }.ShowDialog();
        }

        public void AbrirTelaPet(object dataContext)
        {
            new PetUI() { DataContext= dataContext }.ShowDialog();
        }

        public void AbrirTelaRaca()
        {
            new RacaUI().ShowDialog();
        }

        public void AbrirTelaConfiguracaoArqID()
        {
            new ArqIDUI().ShowDialog();
        }
    }
}
