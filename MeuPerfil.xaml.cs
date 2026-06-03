using System.Windows;
using CRUD.Modelos;
using MySql.Data.MySqlClient;

namespace CRUD;

public partial class MeuPerfil : Window
{
    private Usuario UsuarioAtual;

    public MeuPerfil(Usuario usuario)
    {
        InitializeComponent();
        UsuarioAtual = usuario;
        txtNome.Text = UsuarioAtual.Nome;
        txtUsuario.Text = UsuarioAtual.Username;
        txtEmail.Text = UsuarioAtual.Email;
    }

    private void BtnSalvar_OnClick(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtNome.Text) ||
            string.IsNullOrWhiteSpace(txtEmail.Text))
        {
            MessageBox.Show("Campos incompletos!");
            return;
        }

        var senhaFoiAlterada = !string.IsNullOrWhiteSpace(txtSenha.Password);

        UsuarioAtual.Username = txtUsuario.Text;
        UsuarioAtual.Nome = txtNome.Text;
        UsuarioAtual.Email = txtEmail.Text;
        if (senhaFoiAlterada) UsuarioAtual.Senha = txtSenha.Password;
        

        using var conexao = new MySqlConnection(App.StringConexao);
        var query = "UPDATE usuarios SET username= @username, email = @email, nome = @nome ";

        if (senhaFoiAlterada) query += ", senha = @senha";

        query += " WHERE id = @id";
        
        using var comando = new MySqlCommand(query, conexao);
        
        comando.Parameters.AddWithValue("@username", UsuarioAtual.Username);
        comando.Parameters.AddWithValue("@nome", UsuarioAtual.Nome);
        comando.Parameters.AddWithValue("@email", UsuarioAtual.Email);
        comando.Parameters.AddWithValue("@id", UsuarioAtual.Id);
        
        if (senhaFoiAlterada) comando.Parameters.AddWithValue("@senha", UsuarioAtual.Senha);

        try
        {
            conexao.Open();
            var linhasAfetadas = comando.ExecuteNonQuery();
            
            if (linhasAfetadas > 0)
                MessageBox.Show("cadastro atualizado com sucesso!");
            else
                MessageBox.Show("Erro ao atualizar os dados!");

        }
        catch (Exception exception)
        {
            MessageBox.Show("Erro de DB.");
        }

    }

    private void BtnDeletar_OnClick(object sender, RoutedEventArgs e)
    {
        
    }
}

