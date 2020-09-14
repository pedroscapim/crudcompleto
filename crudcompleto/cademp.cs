using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Text;

namespace crudcompleto
{
    public partial class cademp : Form
    {
        public cademp()
        {
            InitializeComponent();
        }

        SqlConnection sqlCon = null;
        private string strCon = @"Initial Catalog=cdemp; Password = Phs25042001; Persist Security Info=True;User ID = sa; Initial Catalog = cdemp; Data Source = PEDRAOPC\MSSQLSERVER01";
        private string strSQL = string.Empty;


        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btninserir_Click(object sender, EventArgs e)
        {
            strSQL = "insert into cad (razsoc,cnpj,insest,endereco,cidade,estado,telefone) values (@razsoc, @cnpj, @insest, @endereco, @cidade, @estado, @telefone) ";

            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSQL, sqlCon);

            try
            {
                sqlCon.Open();
                comando.Parameters.Add("@razsoc", SqlDbType.VarChar).Value = txtsoc.Text;
                comando.Parameters.Add("@cnpj", SqlDbType.VarChar).Value = txtcnpj.Text;
                comando.Parameters.Add("@insest", SqlDbType.VarChar).Value = txtinsc.Text;
                comando.Parameters.Add("@endereco", SqlDbType.VarChar).Value = txtend.Text;
                comando.Parameters.Add("@cidade", SqlDbType.VarChar).Value = txtcid.Text;
                comando.Parameters.Add("@estado", SqlDbType.VarChar).Value = txtest.Text;
                comando.Parameters.Add("@telefone", SqlDbType.VarChar).Value = txttel.Text;
                comando.ExecuteNonQuery();

                MessageBox.Show("Resgistro adicionado com sucesso!");


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                sqlCon.Close();
                txtsoc.Clear();
                txtcnpj.Clear();
                txtinsc.Clear();
                txtend.Clear();
                txtcid.Clear();
                txtest.Clear();
                txttel.Clear();
            }

        }

        private void btnpesq_Click(object sender, EventArgs e)
        {
            if (txtp.Text == string.Empty)
            {
                strSQL = "select * from cad ";
                sqlCon = new SqlConnection(strCon);
                SqlCommand comando = new SqlCommand(strSQL, sqlCon);
                SqlDataAdapter da = new SqlDataAdapter(strSQL, strCon);
                DataTable dt = new DataTable();
                da.Fill(dt);
                sqlCon.Open();
                dgid.DataSource = dt;
                sqlCon.Close();
            }
            else
            {
                strSQL = "select * from cad where id=@id";
                sqlCon = new SqlConnection(strCon);
                SqlCommand comando = new SqlCommand(strSQL, sqlCon);


                comando.Parameters.Add("@id", SqlDbType.Int).Value = txtp.Text;


                try
                {



                    if (txtp.Text == string.Empty)
                    {
                        throw new Exception("Digite um ID");
                    }

                    sqlCon.Open();

                    SqlDataReader dr = comando.ExecuteReader();


                    if (dr.HasRows == false)
                    {
                        throw new Exception("ID não registrado");

                    }

                    if (dr.Read())
                    {
                        txtid.Text = Convert.ToString(dr["id"]);
                        txtsoc.Text = Convert.ToString(dr["razsoc"]);
                        txtcnpj.Text = Convert.ToString(dr["cnpj"]);
                        txtinsc.Text = Convert.ToString(dr["insest"]);
                        txtend.Text = Convert.ToString(dr["endereco"]);
                        txtcid.Text = Convert.ToString(dr["cidade"]);
                        txtest.Text = Convert.ToString(dr["estado"]);
                        txttel.Text = Convert.ToString(dr["telefone"]);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    sqlCon.Close();
                    txtsoc.Focus();
                }



            }
        }

        private void btnexclui_Click(object sender, EventArgs e)
        {

            //Teste
            if (MessageBox.Show("Deseja excluir o registro?", "Cuidado", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                MessageBox.Show("Operação cancelada");
            }
            else
            {
                strSQL = "delete from cad where id =@id";
                sqlCon = new SqlConnection(strCon);

                SqlCommand comando = new SqlCommand(strSQL, sqlCon);
                comando.Parameters.Add("@id", SqlDbType.Int).Value = txtid.Text;

                try
                {
                    sqlCon.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Registro apagado");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlCon.Close();
                    txtid.Text = "";
                    txtcnpj.Text = "";
                    txtsoc.Text = "";
                    txtinsc.Text = "";
                    txtend.Text = "";
                    txtcid.Text = "";
                    txtest.Text = "";
                    txttel.Text = "";
                    txtsoc.Focus();
                }




            }
        }

        private void dgid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }
    }
}
