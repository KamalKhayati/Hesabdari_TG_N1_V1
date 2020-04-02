﻿/****************************** Ghost.github.io ******************************\
*	Module Name:	FrmAccesslevelCodingHesabdari.cs
*	Project:		SystemManagement
*	Copyright (C) 2018 Kamal Khayati, All rights reserved.
*	This software may be modified and distributed under the terms of the MIT license.  See LICENSE file for details.
*
*	Written by Kamal Khayati <Kamal1355@gmail.com>,  2019 / 2 / 14   11:24 ب.ظ
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
using DBHesabdari_PG;
using System.Data.Entity;
using DBHesabdari_PG.Models.Ms.SystemUsers;

namespace SystemManagement.UsersSystem
{
    public partial class FrmAccesslevelCodingHesabdari : DevExpress.XtraEditors.XtraForm
    {
        public FrmAccesslevelCodingHesabdari()
        {
            InitializeComponent();
        }

        public void FillcmbUsersList()
        {
            using (var dbContext = new MyContext())
            {
                try
                {
                    if (lblUserId.Text == "1")
                    {
                        // This line of code is generated by Data Source Configuration Wizard
                        dbContext.MsUsers.Where(s => s.UserIsActive == true && s.MsUserId != 1).Load();
                        // Bind data to control when loading complete
                        msUserBindingSource.DataSource = dbContext.MsUsers.Local.ToBindingList();
                    }
                    else
                    {
                        int _UserId = Convert.ToInt32(lblUserId.Text);
                        // This line of code is generated by Data Source Configuration Wizard
                        dbContext.MsUsers.Where(s => s.UserIsActive == true && s.MsUserId == _UserId).Load();
                        // Bind data to control when loading complete
                        msUserBindingSource.DataSource = dbContext.MsUsers.Local.ToBindingList();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("عملیات با خطا مواجه شد" + "\n" + ex.Message,
                        "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void FilltreeList_CodingHesabdari()
        {
            using (var dbContext = new MyContext())
            {
                try
                {
                    if (dbContext.EpAllCodingHesabdaris.Any())
                    {
                        // Call the Load method to get the data for the given DbSet from the database.
                        dbContext.EpAllCodingHesabdaris.Where(s => s.IsActive == true).OrderBy(s => s.KeyCode).Load();
                        // This line of code is generated by Data Source Configuration Wizard
                        EpAllCodingHesabdarisBindingSource.DataSource = dbContext.EpAllCodingHesabdaris.Local.ToBindingList();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("عملیات با خطا مواجه شد" + "\n" + ex.Message,
                        "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var db = new MyContext())
            {
                try
                {
                    int _UserId = Convert.ToInt32(cmbUsersList.EditValue);
                    /////////////////////////// ذخیره سطح دسترسی کاربران به لیست کدینگ حسابداری /////////////////////////////
                    var q = db.RmsUserBallCodingHesabdaris.Where(s => s.UserId == _UserId).ToList();
                    if (q.Count > 0)
                    {
                        db.RmsUserBallCodingHesabdaris.RemoveRange(q);
                    }

                    foreach (var item in treeListCodingHesabdari.GetNodeList().Except(treeListCodingHesabdari.GetAllCheckedNodes()))
                    {
                        if (item.CheckState == CheckState.Unchecked)
                        {
                            RmsUserBallCodingHesabdari obj1 = new RmsUserBallCodingHesabdari();
                            obj1.UserId = _UserId;
                            obj1.CodingHesabdariId = Convert.ToInt32(item.GetValue(colId2));
                            obj1.KeyCode = Convert.ToInt32(item.GetValue(colKeyId2));
                            obj1.HesabGroupId = Convert.ToInt32(item.GetValue(colHesabGroupId2));
                            obj1.HesabColId = Convert.ToInt32(item.GetValue(colHesabColId2));
                            obj1.HesabMoinId = Convert.ToInt32(item.GetValue(colHesabMoinId2));
                            obj1.IsActive = Convert.ToBoolean(item.GetValue(colIsActive2));

                            db.RmsUserBallCodingHesabdaris.Add(obj1);
                        }
                    }
                    //////////////////////////////////////////////////////////////////////////////////////////////////
                    db.SaveChanges();
                    XtraMessageBox.Show("عملیات باموفقیت انجام شد", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("عملیات با خطا مواجه شد" + "\n" + ex.Message,
                        "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void chkSelectAll_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (chkSelectAll.Checked)
                treeListCodingHesabdari.CheckAll();
            else
                treeListCodingHesabdari.UncheckAll();
        }

        private void chkOpenClose_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (chkOpenClose.Checked)
                treeListCodingHesabdari.ExpandAll();
            else
                treeListCodingHesabdari.CollapseAll();

        }

        private void btnPrintPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            treeListCodingHesabdari.ShowRibbonPrintPreview();
        }

        private void cmbUsersList_EditValueChanged(object sender, EventArgs e)
        {
            using (var db = new MyContext())
            {
                try
                {
                    int _UserId = Convert.ToInt32(cmbUsersList.EditValue);
                    var q2 = db.RmsUserBallCodingHesabdaris.Where(f => f.UserId == _UserId).ToList();
                    //for (int i = tabPane1.Pages.Count - 1; i >= 0; i--)
                    //{
                    //    tabPane1.SelectedPageIndex = i;
                    //}
                    //treeListDafaterMali.CheckAll();
                    treeListCodingHesabdari.CheckAll();
                    if (q2.Count > 0)
                    {
                        q2.ForEach(item =>
                        {
                            var node2 = treeListCodingHesabdari.FindNodeByKeyID(item.KeyCode);
                            if (node2 != null)
                                treeListCodingHesabdari.SetNodeCheckState(node2, CheckState.Unchecked, false);
                        });
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("عملیات با خطا مواجه شد" + "\n" + ex.Message,
                        "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void FrmAccesslevelCodingHesabdari_Load(object sender, EventArgs e)
        {
            FillcmbUsersList();
            FilltreeList_CodingHesabdari();

            //using (var db = new MyContext())
            //{
            //    try
            //    {
            //        int _UserId = Convert.ToInt32(lblUserId.Text);
            //        var q1 = db.RmsUserBmsAccessLevelMenus.Where(s => s.MsUserId == _UserId).ToList();
            //        if (q1.Count() > 0)
            //        {
            //            //btnCreate.Visibility = q1.Any(s => s.MsAccessLevelMenuId == 55331) ? BarItemVisibility.Never : BarItemVisibility.Always;
            //            //btnSave_ListDafaterMali.Visibility = q1.Any(s => s.MsAccessLevelMenuId == 550302021) ? BarItemVisibility.Never : BarItemVisibility.Always;
            //            //btnDelete.Visibility = q1.Any(s => s.MsAccessLevelMenuId == 55333) ? BarItemVisibility.Never : BarItemVisibility.Always;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        XtraMessageBox.Show("عملیات با خطا مواجه شد" + "\n" + ex.Message,
            //            "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}

        }

        private void treeListCodingHesabdari_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            int a = Convert.ToInt32(e.Node.GetValue(colKeyId2));
            if (a > 0)
            {
                if (a.ToString().Length == 1)
                {
                    if (treeListCodingHesabdari.FindNodeByKeyID(a).CheckState == CheckState.Checked)
                    {
                        treeListCodingHesabdari.GetNodeList().ForEach(item =>
                        {
                            if (Convert.ToInt32(item[colKeyId2]) > a)
                            {
                                if (a.ToString().Substring(0, 1) == item[colKeyId2].ToString().Substring(0, 1))
                                {
                                    var node1 = treeListCodingHesabdari.FindNodeByKeyID(item[colKeyId2]);
                                    treeListCodingHesabdari.SetNodeCheckState(node1, CheckState.Checked, false);
                                }
                            }
                        });
                    }
                    else
                    {
                        treeListCodingHesabdari.GetNodeList().ForEach(item =>
                        {
                            if (Convert.ToInt32(item[colKeyId2]) > a)
                            {
                                if (a.ToString().Substring(0, 1) == item[colKeyId2].ToString().Substring(0, 1))
                                {
                                    var node1 = treeListCodingHesabdari.FindNodeByKeyID(item[colKeyId2]);
                                    treeListCodingHesabdari.SetNodeCheckState(node1, CheckState.Unchecked, false);
                                }
                            }
                        });
                    }
                }
                else if (a.ToString().Length == 2)
                {
                    if (treeListCodingHesabdari.FindNodeByKeyID(a).CheckState == CheckState.Checked)
                    {
                        treeListCodingHesabdari.GetNodeList().ForEach(item =>
                        {
                            if (Convert.ToInt32(item[colKeyId2]) > a)
                            {
                                if (a.ToString().Substring(0, 2) == item[colKeyId2].ToString().Substring(0, 2))
                                {
                                    var node1 = treeListCodingHesabdari.FindNodeByKeyID(item[colKeyId2]);
                                    treeListCodingHesabdari.SetNodeCheckState(node1, CheckState.Checked, false);
                                }
                            }
                            else
                            {
                                if (item[colKeyId2].ToString().Length == 1)
                                {
                                    if (a.ToString().Substring(0, 1) == item[colKeyId2].ToString().Substring(0, 1))
                                    {
                                        var node1 = treeListCodingHesabdari.FindNodeByKeyID(item[colKeyId2]);
                                        treeListCodingHesabdari.SetNodeCheckState(node1, CheckState.Checked, false);
                                    }
                                }
                            }
                        });
                    }
                    else
                    {
                        treeListCodingHesabdari.GetNodeList().ForEach(item =>
                        {
                            if (Convert.ToInt32(item[colKeyId2]) > a)
                            {
                                if (a.ToString().Substring(0, 2) == item[colKeyId2].ToString().Substring(0, 2))
                                {
                                    var node1 = treeListCodingHesabdari.FindNodeByKeyID(item[colKeyId2]);
                                    treeListCodingHesabdari.SetNodeCheckState(node1, CheckState.Unchecked, false);
                                }
                            }
                            //else
                            //{
                            //    if (item[colKeyId].ToString().Length == 1)
                            //    {
                            //        if (a.ToString().Substring(0, 1) == item[colKeyId].ToString().Substring(0, 1))
                            //        {
                            //            var node1 = treeListCodingHesabdari.FindNodeByKeyID(item[colKeyId]);
                            //            treeListCodingHesabdari.SetNodeCheckState(node1, CheckState.Unchecked, false);
                            //        }
                            //    }
                            //}

                        });
                    }
                }
                else if (a.ToString().Length == 4)
                {
                    if (treeListCodingHesabdari.FindNodeByKeyID(a).CheckState == CheckState.Checked)
                    {
                        treeListCodingHesabdari.GetNodeList().ForEach(item =>
                        {
                            if (Convert.ToInt32(item[colKeyId2]) > a)
                            {
                                if (a.ToString().Substring(0, 4) == item[colKeyId2].ToString().Substring(0, 4))
                                {
                                    var node1 = treeListCodingHesabdari.FindNodeByKeyID(item[colKeyId2]);
                                    treeListCodingHesabdari.SetNodeCheckState(node1, CheckState.Checked, false);
                                }
                            }
                            else
                            {
                                if (item[colKeyId2].ToString().Length == 2)
                                {
                                    if (a.ToString().Substring(0, 2) == item[colKeyId2].ToString().Substring(0, 2))
                                    {
                                        var node1 = treeListCodingHesabdari.FindNodeByKeyID(item[colKeyId2]);
                                        treeListCodingHesabdari.SetNodeCheckState(node1, CheckState.Checked, false);
                                    }
                                }
                                else if (item[colKeyId2].ToString().Length == 1)
                                {
                                    if (a.ToString().Substring(0, 1) == item[colKeyId2].ToString().Substring(0, 1))
                                    {
                                        var node1 = treeListCodingHesabdari.FindNodeByKeyID(item[colKeyId2]);
                                        treeListCodingHesabdari.SetNodeCheckState(node1, CheckState.Checked, false);
                                    }
                                }
                            }
                        });
                    }
                    else
                    {
                        treeListCodingHesabdari.GetNodeList().ForEach(item =>
                        {
                            if (Convert.ToInt32(item[colKeyId2]) > a)
                            {
                                if (a.ToString().Substring(0, 4) == item[colKeyId2].ToString().Substring(0, 4))
                                {
                                    var node1 = treeListCodingHesabdari.FindNodeByKeyID(item[colKeyId2]);
                                    treeListCodingHesabdari.SetNodeCheckState(node1, CheckState.Unchecked, false);
                                }
                            }
                            //else
                            //{
                            //    if (item[colKeyId].ToString().Length == 2)
                            //    {
                            //        if (a.ToString().Substring(0, 2) == item[colKeyId].ToString().Substring(0, 2))
                            //        {
                            //            var node1 = treeListCodingHesabdari.FindNodeByKeyID(item[colKeyId]);
                            //            treeListCodingHesabdari.SetNodeCheckState(node1, CheckState.Unchecked, false);
                            //        }
                            //    }
                            //    else if (item[colKeyId].ToString().Length == 1)
                            //    {
                            //        if (a.ToString().Substring(0, 1) == item[colKeyId].ToString().Substring(0, 1))
                            //        {
                            //            var node1 = treeListCodingHesabdari.FindNodeByKeyID(item[colKeyId]);
                            //            treeListCodingHesabdari.SetNodeCheckState(node1, CheckState.Unchecked, false);
                            //        }
                            //    }
                            //}
                        });
                    }
                }
                #region
                //else
                //{
                //    if (treeListCodingHesabdari.FindNodeByKeyID(a).CheckState == CheckState.Checked)
                //    {
                //        treeListCodingHesabdari.GetNodeList().ForEach(item =>
                //        {
                //            if (item[colKeyId2].ToString().Length == 6)
                //            {
                //                if (a.ToString().Substring(0, 6) == item[colKeyId2].ToString().Substring(0, 6))
                //                {
                //                    var node1 = treeListCodingHesabdari.FindNodeByKeyID(item[colKeyId2]);
                //                    treeListCodingHesabdari.SetNodeCheckState(node1, CheckState.Checked, false);
                //                }
                //            }
                //            else if (item[colKeyId2].ToString().Length == 4)
                //            {
                //                if (a.ToString().Substring(0, 4) == item[colKeyId2].ToString().Substring(0, 4))
                //                {
                //                    var node1 = treeListCodingHesabdari.FindNodeByKeyID(item[colKeyId2]);
                //                    treeListCodingHesabdari.SetNodeCheckState(node1, CheckState.Checked, false);
                //                }
                //            }
                //            else if (item[colKeyId2].ToString().Length == 2)
                //            {
                //                if (a.ToString().Substring(0, 2) == item[colKeyId2].ToString().Substring(0, 2))
                //                {
                //                    var node1 = treeListCodingHesabdari.FindNodeByKeyID(item[colKeyId2]);
                //                    treeListCodingHesabdari.SetNodeCheckState(node1, CheckState.Checked, false);
                //                }
                //            }
                //        });
                //    }
                //else
                //{
                //    treeListCodingHesabdari.GetNodeList().ForEach(item =>
                //    {
                //        if (item[colKeyId].ToString().Length == 6)
                //        {
                //            if (a.ToString().Substring(0, 6) == item[colKeyId].ToString().Substring(0, 6))
                //            {
                //                var node1 = treeListCodingHesabdari.FindNodeByKeyID(item[colKeyId]);
                //                treeListCodingHesabdari.SetNodeCheckState(node1, CheckState.Unchecked, false);
                //            }
                //        }
                //        else if (item[colKeyId].ToString().Length == 4)
                //        {
                //            if (a.ToString().Substring(0, 4) == item[colKeyId].ToString().Substring(0, 4))
                //            {
                //                var node1 = treeListCodingHesabdari.FindNodeByKeyID(item[colKeyId]);
                //                treeListCodingHesabdari.SetNodeCheckState(node1, CheckState.Unchecked, false);
                //            }
                //        }
                //        else if (item[colKeyId].ToString().Length == 2)
                //        {
                //            if (a.ToString().Substring(0, 2) == item[colKeyId].ToString().Substring(0, 2))
                //            {
                //                var node1 = treeListCodingHesabdari.FindNodeByKeyID(item[colKeyId]);
                //                treeListCodingHesabdari.SetNodeCheckState(node1, CheckState.Unchecked, false);
                //            }
                //        }
                //    });

                //}
                #endregion
            }
        }

        private void btnReloadList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            cmbUsersList.EditValue=0 ;
            FillcmbUsersList();
            EpAllCodingHesabdarisBindingSource.DataSource = null;
            FilltreeList_CodingHesabdari();

        }
    }
}
