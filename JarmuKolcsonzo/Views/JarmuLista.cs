﻿using JarmuKolcsonzo.Models;
using JarmuKolcsonzo.Presenters;
using JarmuKolcsonzo.ViewInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JarmuKolcsonzo.Views
{
    public partial class JarmuLista : Form, IJarmuListaView
    {
        private JarmuListaPresenter presenter;
        // Combobox létrehozása
        private DataGridViewComboBoxColumn jkCol;
        // Oldaltördelés
        private int perPage = 1;
        private int page = 1;
        private int total = 0;
        private int maxPage;

        public JarmuLista()
        {
            InitializeComponent();
            presenter = new JarmuListaPresenter(this);
            jkCol = new DataGridViewComboBoxColumn();
            maxPage = total / perPage;
        }

        public BindingList<jarmu> bindingList
        {
            get => (BindingList<jarmu>)dataGridView1.DataSource;
            set => dataGridView1.DataSource = value;
        }
        public BindingList<jarmukategoria> jarmukategoriaList
        {
            get => (BindingList<jarmukategoria>)jkCol.DataSource;
            set
            {
                value.Insert(0, new jarmukategoria() { Id = 0 });
                jkCol.DataSource = value;
            }
        }
        public int pageNumber => page;
        public int itemsPerPage => perPage;
        public string search => keresestoolStripTextBox.Text;
        public string sortBy => throw new NotImplementedException();
        public bool ascending => throw new NotImplementedException();
        public int totalItems 
        {
            set
            {
                total = value;
                label1.Text = page.ToString() + "/" + total.ToString();
            }
        }

        private void JarmuLista_Load(object sender, EventArgs e)
        {
            presenter.LoadData();

            // Kategória colbox feltöltése
            jkCol.DataPropertyName = "kategoriaId";
            jkCol.DisplayMember = "kategoriaNev";
            jkCol.Name = "kategoriaId";
            jkCol.HeaderText = "Kategória";
            jkCol.ValueMember = "Id";
            //jkCol.ValueType = typeof(jarmukategoria);

            dataGridView1.Columns.RemoveAt(2);
            dataGridView1.Columns.Insert(2, jkCol);
        }

        private void mentestoolStripButton1_Click(object sender, EventArgs e)
        {
            presenter.Save();
        }

        private void FirstButton_Click(object sender, EventArgs e)
        {
            page = 1;
            presenter.LoadData();
        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            while (page != 1)
            {
                page--;
            }
            presenter.LoadData();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (page != maxPage)
            {
                page++;
            }
            presenter.LoadData();
        }

        private void LastButton_Click(object sender, EventArgs e)
        {
            page = total / perPage;
            presenter.LoadData();
        }
    }
}