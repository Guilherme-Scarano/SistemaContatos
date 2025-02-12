using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaBiblioteca3
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnEntrar_Click_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=127.0.0.1;Port=3306;Database=CadastroContatosDB;Uid=root;Pwd=admin;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand("sp_ValidarLogin", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("p_NomeUsuario", txtUsuario.Text);
                    command.Parameters.AddWithValue("p_Senha", txtSenha.Text);

                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        MessageBox.Show("Login realizado com sucesso!");
                        this.Hide();
                        Form1 principal = new Form1();
                        principal.Show();
                    }
                    else
                    {
                        MessageBox.Show("Usuário ou senha incorretos!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao tentar logar: " + ex.Message);
                }
            }
        }
    }
}
