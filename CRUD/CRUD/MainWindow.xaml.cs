using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace MySQL
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Mostrar();
        }

        private void Limpiar()
        {
            Nom.Text = "";
            Apellidos.Text = "";
            Telefono.Text = "";
        }

        private void BtnN_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "datasource=127.0.0.1;username=root;password=contra;database=registros;";
            if (Nom.Text != "")
            {
                if (Apellidos.Text != "")
                {
                    if (Telefono.Text != "")
                    {
                        string querty = "INSERT INTO registros(id,Nombre,Correo,Telefono) " + "VALUES(NULL,'" + Nom.Text + "' , '" + Apellidos.Text + "', " + Telefono.Text + ")";

                        MySqlConnection connection = new MySqlConnection(connectionString);
                        MySqlCommand cmd = new MySqlCommand(querty, connection);
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Registro exitoso.");
                        connection.Close();
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("Favor introduce tu telefono", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Favor introduce tu correo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Favor introduce tu nombre", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Mostrar()
        {
            string connectionString = "datasource=127.0.0.1;username=root;password=contra;database=registros;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM registros", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();
            dgrid.DataContext = dt;
        }
        private void BUpdate_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "datasource=127.0.0.1;username=root;password=contra;database=registros;";
            string querty = "UPDATE registros SET Nombre = '" + Nom.Text + "' , Correo = '" + Apellidos.Text + "', Telefono = " + Telefono.Text + " WHERE id = " + id.Text + "";

            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand(querty, connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            Limpiar();
            Mostrar();
            SPEdit.Visibility = System.Windows.Visibility.Hidden;
            BUpdate.Visibility = System.Windows.Visibility.Hidden;
        }

        private void BEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgrid.SelectedItems.Count > 0)
            {
                DataRowView row = (DataRowView)dgrid.SelectedItems[0];
                id.Text = row["id"].ToString();
                Nom.Text = row["Nombre"].ToString();
                Apellidos.Text = row["Correo"].ToString();
                Telefono.Text = row["Telefono"].ToString();
                id.IsEnabled = false;
                SPEdit.Visibility = System.Windows.Visibility.Visible;
                BUpdate.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Seleccione un registro de la lista");
            }
        }

        private void BCan_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void BDel_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "datasource=127.0.0.1;username=root;password=contra;database=registros;";
            if (dgrid.SelectedItems.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Seguro que quieres eliminar este registro?", "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    DataRowView row = (DataRowView)dgrid.SelectedItems[0];
                    string querty = "DELETE FROM registros WHERE id =" + row["id"].ToString();

                    MySqlConnection connection = new MySqlConnection(connectionString);
                    MySqlCommand cmd = new MySqlCommand(querty, connection);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    Limpiar();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un registro de la lista");
            }
        }
    }
}

