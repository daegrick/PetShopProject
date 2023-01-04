namespace UI.Services
{
    public interface IWindowService
    {
        void AbrirTelaPessoa(object dataContext);
        void AbrirTelaPet(object dataContext);
        void AbrirTelaRaca();
        void AbrirTelaAdocao(object dataContext);
        void AbrirTelaBuscaPessoa(object dataContext);
        void AbrirTelaBuscaPet(object dataContext);
    }
}
