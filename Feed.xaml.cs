using System.Windows;
using CRUD.Modelos;
using MySql.Data.MySqlClient;

namespace CRUD;

public partial class Feed : Window
{
    public Feed()
    {
        InitializeComponent();
        CarregarPosts_QuandoIniciar();
    }

    private void CarregarPosts_QuandoIniciar()
    {
        List<Postagem> listaPostagens = [];
        
        const string query = "SELECT p.id,\n       p.conteudo,\n       p.curtidas,\n       p.postado_em,\n       u.nome,\n       u.username\nFROM postagens p\nINNER JOIN usuarios u \n    ON p.usuario_id = u.id";

        using var conexao = new MySqlConnection(App.StringConexao);
        
        using var comando = new MySqlCommand(query, conexao);

        try
        {
            conexao.Open();
            using var reader = comando.ExecuteReader();
            if (!reader.HasRows)
            {
                MessageBox.Show("Nenhuma Postagem encontrada");
                return;
            }

            while (reader.Read())
            {
                var post = new Postagem
                {
                    id = reader.GetInt32("id"),
                    Conteudo = reader.GetString("conteudo"),
                    Curtidas = reader.GetInt32("curtidas"),
                    Postado_em = reader.GetDateTime("postado_em"),
                    Usuario = new Usuario
                    {
                        Nome = reader.GetString("nome"),
                        Username = reader.GetString("username")
                    }
                };
                
                listaPostagens.Add(post);
            }
            ItemsControlFeed.ItemsSource = listaPostagens;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }
}