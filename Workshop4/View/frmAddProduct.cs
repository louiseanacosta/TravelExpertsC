﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Workshop4.Controller;

namespace Workshop4
{
    //--------------------------- LOUISE ACOSTA ------------------------------
    public partial class frmAddProduct : Form
    {
        public Package package;
        public BindingSource productsInPackageBindingSource;
        public bool addToExistingPackage; // indicates whether it is adding to an existing package(modify tab)
        List<AvailableProducts> products = AvailableProductsDB.GetAvailableProducts();

        public frmAddProduct(BindingSource incommingProductsInPackageBindingSource)
        {
            this.productsInPackageBindingSource = incommingProductsInPackageBindingSource;
            InitializeComponent();
        }

    
        private void AddProduct_Load(object sender, EventArgs e)
        {
            var _sortableProducts = new SortableBindingList<AvailableProducts>(products);
            availableProductsBindingSource.DataSource = _sortableProducts;// bind list of products in package to gridview
        }

        
        // Add products to package
        private void btnAddToPackage_Click(object sender, EventArgs e)
        {
            if(addToExistingPackage) // add to modify tab
            {
                foreach (DataGridViewRow row in dataGridViewAvailableProducts.SelectedRows)
                {
                    // get selected 
                    ProductsInPackage productsInPackage = new ProductsInPackage();
                    AddToPackage(productsInPackage, row);
                }

            }
            else // add to gridview in new package tab
            {
                foreach (DataGridViewRow row in dataGridViewAvailableProducts.SelectedRows)
                {
                    // get selected 
                    ProductsInPackage newProductsInPackage = new ProductsInPackage();
                   AddToPackage(newProductsInPackage, row);

                }
            }

            this.Close();
        }

        // search available products
        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            // get search
            string search = txtSearch.Text.Trim();

            // get products filtered by search
            List<AvailableProducts> availableProducts = AvailableProductsDB.GetAvailableProducts(search);

            // update binding source
            availableProductsBindingSource.DataSource = availableProducts;
        }

        /// <summary>
        /// Add to selected row to package
        /// </summary>
        /// <param name="productsInPackage"></param>
        /// <param name="row"></param>
        private void AddToPackage(ProductsInPackage productsInPackage, DataGridViewRow row)
        {
            productsInPackage.ProductSupplierId = Convert.ToInt32(row.Cells[0].Value);
            productsInPackage.ProdName = row.Cells[1].Value.ToString();
            productsInPackage.SupName = row.Cells[2].Value.ToString();

            // add to datagridview in modify tab
            this.productsInPackageBindingSource.Add(productsInPackage);
        }

    }
}
//--------------------------- LOUISE ACOSTA - END ------------------------------
