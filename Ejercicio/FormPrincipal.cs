using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ejercicio.Models;
using Ejercicio.Models.Exportadores;
using Microsoft.Win32.SafeHandles;

namespace Ejercicio
{
    public partial class FormPrincipal : Form
    {
        List<Multa> multas = new List<Multa>();
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbPatente.Text) || string.IsNullOrWhiteSpace(tbImporte.Text))
            {
                MessageBox.Show("Los campos 'Patente' e 'Importe' no pueden estar vacíos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Multa nuevo = new Multa();

            nuevo.Patente = tbPatente.Text;
            nuevo.Vencimiento = dtpVencimiento.Value;
            nuevo.Importe = double.Parse(tbImporte.Text);

            tbImporte.Text = "";
            tbPatente.Text = "";
            dtpVencimiento.Value = DateTime.Now;

            multas.Add(nuevo);
            btnActualizar.Enabled = true;

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            lsbVer.Items.Clear();
            multas.Sort();
            foreach (Multa multa in multas)
            {
                lsbVer.Items.Add(multa.ToString());
            }
            btnExportar.Enabled = true;

        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "csv|*.csv|xml|*.xml|txt|*.txt|json|*.json",
                Title = "Seleccionar archivo para importar"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog.FileName;
                int tipo = openFileDialog.FilterIndex;
                IExportador exportador = (new ExportadorFactory()).GetInstance(tipo);

                try
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            IExportable nuevo = new Multa();
                            if (nuevo.Importar(line, exportador))
                            {
                                multas.Add((Multa)nuevo);
                            }
                        }
                    }
                    btnActualizar.Enabled = true;
                    btnActualizar.PerformClick();
                    btnExportar.Enabled = true;
                    MessageBox.Show("Importación completada con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al importar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "csv|*.csv|xml|*.xml|txt|*.txt|json|*.json",
                Title = "Seleccionar archivo para importar"
            };
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = saveFileDialog.FileName;
                    int tipo = saveFileDialog.FilterIndex;
                    fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                    sw = new StreamWriter(fs);
                    IExportador exportador = (new ExportadorFactory()).GetInstance(tipo);
                    foreach (IExportable multa in multas)
                    {
                        string line = multa.Exportar(exportador);
                        if (line != null)
                        {
                            sw.WriteLine(line);
                        }
                    }
                }
                if (sw != null) { sw.Close(); }
                if (fs != null) { fs.Close(); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
 }

