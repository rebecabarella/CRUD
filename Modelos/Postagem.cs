using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CRUD.Modelos;

public class Postagem : INotifyPropertyChanged
{
    private int _curtidas;
    private bool _foiCurtido;
    public int Id { get; set; }
    public string Conteudo { get; set; }

    public int Curtidas
    {
        get => _curtidas;
        set
        {
            _curtidas = value;
            NotificarPropriedadeAlterada();
        }
    }

    public DateTime Postado_em { get; set; }
    public Usuario Usuario { get; set; }

    public bool FoiCurtido
    {
        get => _foiCurtido;
        set
        {
            if (_foiCurtido != value)
            {
                _foiCurtido = value;
                NotificarPropriedadeAlterada();
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotificarPropriedadeAlterada([CallerMemberName] string namePropriedade = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(namePropriedade));
    }
}