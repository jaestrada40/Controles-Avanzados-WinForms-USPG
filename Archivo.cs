using ControlesAvanzados.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlesAvanzados
{
    public partial class Archivo : Form
    {
        List<Venta> ventas = new List<Venta>();
        public Archivo()
        {
            InitializeComponent();
            agregarVentas();
            inicializarListBox();
            inicializarComboBoxAnios();
            inicializarComboBoxMeses();
        }

        private void agregarVentas()
        {
            ventas.Add(new Venta(2024, 1, "Guatemala", 100000));
            ventas.Add(new Venta(2024, 2, "Guatemala", 80000));
            ventas.Add(new Venta(2024, 3, "Guatemala", 95000));
            ventas.Add(new Venta(2024, 4, "Guatemala", 120000));
            ventas.Add(new Venta(2024, 5, "Guatemala", 100000));
            ventas.Add(new Venta(2024, 6, "Guatemala", 110000));
            ventas.Add(new Venta(2024, 1, "Jutiapa", 50000));
            ventas.Add(new Venta(2024, 2, "Jutiapa", 80000));
            ventas.Add(new Venta(2024, 3, "Jutiapa", 67000));
            ventas.Add(new Venta(2024, 4, "Jutiapa", 55000));
            ventas.Add(new Venta(2024, 5, "Jutiapa", 67000));
            ventas.Add(new Venta(2024, 6, "Jutiapa", 45000));
            ventas.Add(new Venta(2024, 1, "Chiquimula", 43000));
            ventas.Add(new Venta(2024, 2, "Chiquimula", 55000));
            ventas.Add(new Venta(2024, 3, "Chiquimula", 23000));
            ventas.Add(new Venta(2024, 4, "Chiquimula", 34000));
            ventas.Add(new Venta(2021, 5, "Chiquimula", 56000));
            ventas.Add(new Venta(2024, 6, "Chiquimula", 78000));
            ventas.Add(new Venta(2022, 1, "Escuintla", 86000));
            ventas.Add(new Venta(2024, 2, "Escuintla", 75000));
            ventas.Add(new Venta(2024, 3, "Escuintla", 64000));
            ventas.Add(new Venta(2024, 4, "Escuintla", 78000));
            ventas.Add(new Venta(2022, 5, "Escuintla", 79000));
            ventas.Add(new Venta(2024, 6, "Escuintla", 90000));
            ventas.Add(new Venta(2023, 6, "Zacapa", 10000));
        }

        private void mostrarVentas()
        {
            listadoVentas.Controls.Clear();

            List<Venta> ventasFiltradas = ventas
                .Where(venta => selectorDepartamento.SelectedItem == null || selectorDepartamento.SelectedItems.Contains(venta.Departamento))
                // Si es Todos los años
                .Where(venta => comboBoxAnios.SelectedIndex == 0 || venta.Anio == (int)comboBoxAnios.SelectedItem)
                // Si es Todos los meses
                .Where(venta => comboBoxMeses.SelectedIndex == 0 || venta.Mes == comboBoxMeses.SelectedIndex) 
                .ToList();

            foreach (Venta venta in ventasFiltradas)
            {
                Label labelVenta = crearEqituetaVenta(venta);
                listadoVentas.Controls.Add(labelVenta);
            }
        }


        private void inicializarListBox()
        {
            List<string> departamentos = new List<string>();
            foreach (Venta venta in ventas)
            {
                if (!departamentos.Contains(venta.Departamento))
                {
                    departamentos.Add(venta.Departamento);
                }
            }
            foreach (string departamento in departamentos)
            {
                selectorDepartamento.Items.Add(departamento);
            }
        }

        private void inicializarComboBoxAnios()
        {
            comboBoxAnios.Items.Clear();
            // Opción para seleccionar todos los años
            comboBoxAnios.Items.Add("Todos los años"); 

            List<int> anios = new List<int>();
            foreach (Venta venta in ventas)
            {
                if (!anios.Contains(venta.Anio))
                {
                    anios.Add(venta.Anio);
                }
            }
            foreach (int anio in anios)
            {
                comboBoxAnios.Items.Add(anio);
            }
            // Seleccionar Todos los años por defecto
            comboBoxAnios.SelectedIndex = 0; 
        }

        private void inicializarComboBoxMeses()
        {
            comboBoxMeses.Items.Clear();
            // Opción para todos los meses
            comboBoxMeses.Items.Add("Todos los meses"); 

            List<string> meses = new List<string>();
            foreach (Venta venta in ventas)
            {
                string nombreMes = obtenerNombreMesPorNumero(venta.Mes);
                if (!meses.Contains(nombreMes))
                {
                    meses.Add(nombreMes);
                }
            }
            foreach (string mes in meses)
            {
                comboBoxMeses.Items.Add(mes);
            }
            // Seleccionar Todos los meses por defecto
            comboBoxMeses.SelectedIndex = 0; 
        }

        private string obtenerNombreMesPorNumero(int numeroMes)
        {
            string[] meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            return meses[numeroMes - 1];
        }

        private Label crearEqituetaVenta(Venta venta)
        {
            Label labelVenta = new Label();
            // Se formateo la venta con :N2
            labelVenta.Text = $"Año: {venta.Anio}\nMes: {obtenerNombreMesPorNumero(venta.Mes)}\nDepartamento: {venta.Departamento}\nVentas: Q.{venta.Ventas:N2}";
            labelVenta.Font = new Font("Arial", 12, FontStyle.Bold);
            labelVenta.Padding = new Padding(10);
            labelVenta.BorderStyle = BorderStyle.FixedSingle;
            labelVenta.BackColor = Color.LightGray;
            // Alineación de texto a la izquierda
            labelVenta.TextAlign = ContentAlignment.MiddleLeft;
            // Tamaño fijo de la caja
            labelVenta.Size = new Size(300, 100); 
            labelVenta.Margin = new Padding(10);

            return labelVenta;
        }

        private void selectorDepartamento_SelectedValueChanged(object sender, EventArgs e)
        {
            mostrarVentas();
        }

        private void comboBoxAnios_SelectedValueChanged(object sender, EventArgs e)
        {
            mostrarVentas();
        }

        private void comboBoxMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            mostrarVentas();
        }
    }
}
