﻿/****************************** Ghost.github.io ******************************\
*	Module Name:	FrmShoabatList.cs
*	Project:		SystemManagement
*	Copyright (C) 2018 Kamal Khayati, All rights reserved.
*	This software may be modified and distributed under the terms of the MIT license.  See LICENSE file for details.
*
*	Written by Kamal Khayati <Kamal1355@gmail.com>,  2018 / 12 / 31   04:12 ب.ظ
*	
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HelpClassLibrary;
using DevExpress.XtraBars;
using DBHesabdari_TG;


namespace SystemManagement.DafaterMali
{
    public partial class FrmShoabatList : DevExpress.XtraEditors.XtraForm
    {
        public FrmShoabatList()
        {
            InitializeComponent();
        }

        bool isActive = true;
        private void btnCreate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmShoabatCed fm = new FrmShoabatCed(this);
            HelpClass1.FormNewRecordCreate(fm);
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0 && btnEdit.Visibility == BarItemVisibility.Always)
            {
                FrmShoabatCed fm = new FrmShoabatCed(this);
                HelpClass1.FormCurrentRecordEdit(gridView1, fm);
            }
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                FrmShoabatCed fm = new FrmShoabatCed(this);
                HelpClass1.FormCurrentRecordDelete(gridView1, fm);
            }
        }

        private void btnPrintPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            HelpClass1.PrintPreview(gridControl1, gridView1);
        }

        private void btnAdvancedSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.OptionsFind.AlwaysVisible = gridView1.OptionsFind.AlwaysVisible ? false : true;

        }

        public void btnDisplyActiveList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            isActive = true;
            FillGridViewWhitInstantFeedbackSource();
        }

        public void btnDisplyNotActiveList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            isActive = false;
            FillGridViewWhitInstantFeedbackSource();

        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_ItemClick(null, null);

        }

        private void gridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnEdit_ItemClick(null, null);
            }

        }

        public void FillGridViewWhitInstantFeedbackSource()
        {
            gridControl1.DataSource = null;
            // This line of code is generated by Data Source Configuration Wizard
            this.pLinqInstantFeedbackSource1.GetEnumerable += pLinqInstantFeedbackSource1_GetEnumerable;
            // This line of code is generated by Data Source Configuration Wizard
            this.pLinqInstantFeedbackSource1.DismissEnumerable += pLinqInstantFeedbackSource1_DismissEnumerable;
            gridControl1.DataSource = pLinqInstantFeedbackSource1;
        }

        private void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            HelpClass1.SetNumberRowsColumnUnboundGirdView(sender, e);
        }

        // This event is generated by Data Source Configuration Wizard
        void pLinqInstantFeedbackSource1_GetEnumerable(object sender, DevExpress.Data.PLinq.GetEnumerableEventArgs e)
        {

            // Instantiate a new DataContext
            DBHesabdari_TG.MyContext dataContext = new DBHesabdari_TG.MyContext();
            // Assign a queryable source to the PLinqInstantFeedbackSource
            if (isActive == true)
                e.Source = dataContext.MsShobes.Where(p => p.ShobeIsActive == true);
            else
                e.Source = dataContext.MsShobes.Where(p => p.ShobeIsActive == false);
            // Assign the DataContext to the Tag property,
            // to dispose of it in the DismissEnumerable event handler
            e.Tag = dataContext;
        }

        // This event is generated by Data Source Configuration Wizard
        void pLinqInstantFeedbackSource1_DismissEnumerable(object sender, DevExpress.Data.PLinq.GetEnumerableEventArgs e)
        {

            // Dispose of the DataContext
            ((DBHesabdari_TG.MyContext)e.Tag).Dispose();
        }

        private void FrmShoabatList_Load(object sender, EventArgs e)
        {

            using (var db = new MyContext())
            {
                try
                {
                    int _UserId = Convert.ToInt32(lblUserId.Text);
                    var q1 = db.RmsUserhaBmsAccessLevel1has.Where(s => s.MsUserId == _UserId).ToList();
                    if (q1.Count() > 0)
                    {
                        btnCreate.Visibility = q1.Any(s => s.MsAccessLevel1Id == 55131) ? BarItemVisibility.Never : BarItemVisibility.Always;
                        btnEdit.Visibility = q1.Any(s => s.MsAccessLevel1Id == 55132) ?   BarItemVisibility.Never : BarItemVisibility.Always;
                        btnDelete.Visibility = q1.Any(s => s.MsAccessLevel1Id == 55133) ? BarItemVisibility.Never : BarItemVisibility.Always;
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("عملیات با خطا مواجه شد" + "\n" + ex.Message,
                        "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

}
