﻿using JarmuKolcsonzo.Models;
using JarmuKolcsonzo.Repositories;
using JarmuKolcsonzo.ViewInterfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarmuKolcsonzo.Presenters
{
    class UgyfelPresenter
    {
        private IDataGridList<ugyfel> view;
        private UgyfelRepository repo = new UgyfelRepository();
        public UgyfelPresenter(IDataGridList<ugyfel> param)
        {
            view = param;
        }

        public void LoadData()
        {
            view.bindingList = repo.GetAllUgyfel(
                view.pageNumber, view.itemsPerPage, view.search, view.sortBy, view.ascending);
            view.totalItems = repo.Count();
        }

        public void Save()
        {
            repo.Save();
        }
    }
}