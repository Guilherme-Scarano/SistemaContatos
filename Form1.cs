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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection("server=localhost;user=root;database=CadastroContatosDB;port=3306;password=admin");
        }


        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text;
            string telefone = txtTelefone.Text;
            string email = txtEmail.Text;
            string endereco = txtEndereco.Text;

            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand("SP_CadastrarContato", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Nome", nome);
                cmd.Parameters.AddWithValue("p_Telefone", telefone);
                cmd.Parameters.AddWithValue("p_Email", email);
                cmd.Parameters.AddWithValue("p_Endereco", endereco);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Contato cadastrado com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string nomeBusca = txtBusca.Text;

            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand("SP_BuscarContatoPorNome", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("p_Nome", nomeBusca);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvContatos.DataSource = dt;
            }
        }

        private void AtualizarDataGridView()
        {
            string query = "SELECT * FROM Contatos";
            MySqlDataAdapter da = new MySqlDataAdapter(query, GetConnection());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvContatos.DataSource = dt;
        }

        private void btnAlterarContato_Click(object sender, EventArgs e)
        {
            if (dgvContatos.SelectedRows.Count > 0)
            {
                int idContato = Convert.ToInt32(dgvContatos.SelectedRows[0].Cells["ID"].Value);
                string nome = txtAlterarNome.Text;
                string telefone = txtAlterarTelefone.Text;
                string email = txtAlterarEmail.Text;
                string endereco = txtAlterarEndereco.Text;

                using (MySqlConnection conn = GetConnection())
                {
                    string query = @"
                UPDATE Contatos 
                SET Nome = @Nome, 
                    Telefone = @Telefone, 
                    Email = @Email, 
                    Endereco = @Endereco 
                WHERE ID = @ID;";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@ID", idContato);
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    cmd.Parameters.AddWithValue("@Telefone", telefone);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Endereco", endereco);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Contato alterado com sucesso!");
                            AtualizarDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Nenhuma alteração realizada. Verifique os dados.");
                        }

                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um contato para alterar.");
            }
        }

        private void dgvContatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvContatos.Rows[e.RowIndex];

                txtAlterarNome.Text = row.Cells["Nome"].Value.ToString();
                txtAlterarTelefone.Text = row.Cells["Telefone"].Value.ToString();
                txtAlterarEmail.Text = row.Cells["Email"].Value.ToString();
                txtAlterarEndereco.Text = row.Cells["Endereco"].Value.ToString();
            }
        }

        private void ID_Click(object sender, EventArgs e)
        {

        }
    }
}
