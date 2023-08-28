﻿
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;
using StudentManagement.Models;
using web4.Models;
using System.Data.Odbc;
using System.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using DevExpress.DataProcessing.InMemoryDataProcessor;

namespace web4.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand sqlc = new SqlCommand();
        SqlDataReader dt;

        // GET: /Account/
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
      
        public void connectSQL()
        {
            con.ConnectionString = "Data source=" + "118.69.109.109" + ";database=" + "SAP_OPC" + ";uid=sa;password=Hai@thong";
        }
        public ActionResult DoiMatKhau()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Verify(Account Acc, Top10DoanhThuItem Top10)
        {
            connectSQL();
            con.Open();
            string username = "";
            sqlc.Connection = con;
            sqlc.CommandText = "select * from view_user where Tendangnhap ='" + Acc.Name + "'And matkhau='" + Acc.Password + "'and ma_DvCs='" + Acc.Ma_DvCs + "'";
            dt = sqlc.ExecuteReader();
            if (dt.Read())
            {
                Response.Cookies["UserName"].Value = Acc.Name.ToString();
                Response.Cookies["MA_DVCS"].Value = Acc.Ma_DvCs.ToString();

                con.Close();
                username = Request.Cookies["UserName"].Value;
                ViewBag.Username = username;
                return About(); 
            }
            else
            {
                ViewBag.Message = "Sai Mật Khẩu";
                return View("Login");
            }
        }
        public ActionResult About()
        {
            //DataSet ds = new DataSet();
            //connectSQL();
            //Top10.Ma_DvCs = Request.Cookies["MA_DVCS"].Value;


            //DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);


            //DateTime currentDate = DateTime.Now;

            //string Pname = "[usp_Top10DoanhThu_SAP]";


            //using (SqlCommand cmd = new SqlCommand(Pname, con))
            //{
            //    cmd.CommandTimeout = 950;
            //    cmd.Connection = con;
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    Top10.From_date = firstDayOfMonth.ToString();
            //    Top10.To_date = currentDate.ToString();

            //    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            //    {
            //        cmd.Parameters.AddWithValue("@_Tu_Ngay", Top10.From_date);
            //        cmd.Parameters.AddWithValue("@_Den_Ngay", Top10.To_date);
            //        cmd.Parameters.AddWithValue("@_ma_dvcs", Top10.Ma_DvCs);
            //        sda.Fill(ds);
            //    }
            //}
            var username = Request.Cookies["UserName"].Value;
            ViewBag.Username = username;

            return View();
        }
       
    


        //Báo cáo công nợ quản trị lấy  ra 5 bảng;
        public ActionResult BCCN_main()
        {
            return View();
        }
        public ActionResult Index()
        {

           
        
            return View();
        }
        public ActionResult baocaocongno(Account Acc)
        {
            DataSet ds = new DataSet();
            connectSQL();
            Acc.Ma_DvCs_1 = Request.Cookies["MA_DVCS"].Value;
            //string query = "exec usp_Vth_BC_BHCNTK_CN @_ngay_Ct1 = '" + Acc.From_date + "',@_Ngay_Ct2 ='"+ Acc.To_date+"',@_Ma_Dvcs='"+ Acc.Ma_DvCs_1+"'";
            string Pname = "[usp_Vth_BC_BHCNTK_CN]";
       
            using (SqlCommand cmd = new SqlCommand(Pname, con))
            {
                cmd.CommandTimeout = 950;
               
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.AddWithValue("@_Ma_Dvcs", Acc.Ma_DvCs_1);
                        cmd.Parameters.AddWithValue("@_Ngay_Ct1", Acc.From_date);
                        cmd.Parameters.AddWithValue("@_Ngay_Ct2", Acc.To_date);
                        cmd.Parameters.AddWithValue("@_WEB", 1);
                        sda.Fill(ds);
                    }
            }
              

            return View(ds);

        }
        //báo cáo doanh thu TDV
        public ActionResult BCDT_TDV()
        {
            return View();
        }
        //public ActionResult BaoCaoDoanhThu_TDV(Account Acc)
        //{
        //    connectSQL();
        //    con.Open();
        //    sqlc.Connection = con;
        //    string Pname = "usp_Vth_BaoCaoDoanhThuTDV";
        //    Acc.Ma_DvCs_1 = Request.Cookies["MA_DVCS"].Value;
        //    SqlCommand com = new SqlCommand(Pname, con);
        //        DataTable dt;
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.AddWithValue("@_donvicoso", Acc.Ma_DvCs_1);
        //        com.Parameters.AddWithValue("@_Ngay_Ct1", Acc.From_date);
        //        com.Parameters.AddWithValue("@_Ngay_Ct2", Acc.To_date);
        //        com.Parameters.AddWithValue("@_web", 1);
        //        SqlDataReader dr = com.ExecuteReader();
        //        dt = new DataTable();
        //        dt.Load(dr);
        //    return View(dt);
        //}
        public ActionResult BaoCaoDoanhThu_TDV(Account Acc)
        {
            DataSet ds = new DataSet();
            connectSQL();
            Acc.Ma_DvCs_1 = Request.Cookies["MA_DVCS"].Value;
            //string query = "exec usp_Vth_BC_BHCNTK_CN @_ngay_Ct1 = '" + Acc.From_date + "',@_Ngay_Ct2 ='"+ Acc.To_date+"',@_Ma_Dvcs='"+ Acc.Ma_DvCs_1+"'";
            string Pname = "[usp_Vth_BaoCaoDoanhThuTDV]";

            using (SqlCommand cmd = new SqlCommand(Pname, con))
            {
                cmd.CommandTimeout = 950;

                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@_donvicoso", Acc.Ma_DvCs_1);
                    cmd.Parameters.AddWithValue("@_Ngay_Ct1", Acc.From_date);
                    cmd.Parameters.AddWithValue("@_Ngay_Ct2", Acc.To_date);
                    cmd.Parameters.AddWithValue("@_web", 1);
                    sda.Fill(ds);
                }
            }


            return View(ds);

        }
        //Báo cáo bán hàng TDV chi nhánh
        public ActionResult BCBH_CN()
        {
            return View();
        }
        //layout đổi mật khẩu 
        public ActionResult Check_Doipass(ChangePasswordViewModel Acc)
        {
            connectSQL();
            con.Open();
            sqlc.Connection = con;
            Acc.UserName = Request.Cookies["UserName"].Value;
            string Pname = "update view_user set matkhau ='" + Acc.NewPassword + "'Where tendangnhap='" + Acc.UserName + "'and matkhau='" + Acc.OldPassword + "'";
            SqlCommand com = new SqlCommand(Pname, con);
            com.Parameters.AddWithValue("@matkhau", Acc.NewPassword);
            SqlDataReader dr = com.ExecuteReader();
            con.Close();
            connectSQL();
            con.Open();
            sqlc.Connection = con;
            sqlc.CommandText = "select * from view_user where Tendangnhap ='" + Acc.UserName + "'And matkhau='" + Acc.NewPassword + "'";
            dt = sqlc.ExecuteReader();
            if (dt.Read())
            {
                //ViewBag.Message = "Đã Đổi Mật Khẩu Thành Công";
                con.Close();
                return View("About");
            }
            else
            {
                ViewBag.Message = "khong thanh cong vui long thu lai";
                return View("DoiMatKhau");
            }
            return View("About");

        }

        public ActionResult BaoCaoDoanhThu_THSP(Account Acc)
        {
            DataSet ds = new DataSet();
            connectSQL();
            Acc.Ma_DvCs_1 = Request.Cookies["MA_DVCS"].Value;
            Acc.UserName = Request.Cookies["UserName"].Value;
            //string query = "exec usp_Vth_BC_BHCNTK_CN @_ngay_Ct1 = '" + Acc.From_date + "',@_Ngay_Ct2 ='"+ Acc.To_date+"',@_Ma_Dvcs='"+ Acc.Ma_DvCs_1+"'";
            string Pname = "[usp_Vth_BC_DTSP_ALL]";

            using (SqlCommand cmd = new SqlCommand(Pname, con))
            {   
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {

                    cmd.Parameters.AddWithValue("@_Ngay_Ct1", Acc.From_date);
                    cmd.Parameters.AddWithValue("@_Ngay_Ct2", Acc.To_date);

                    sda.Fill(ds);
                }

            }

            return View(ds);    
        }
        
        public ActionResult MainBaoCao()
        {
            var username = Request.Cookies["UserName"].Value;
            ViewBag.Username = username;

            return View();
        }

        public ActionResult MauIn()
        {
            var username = Request.Cookies["UserName"].Value;
            ViewBag.Username = username;

            return View();
        }
        //public ActionResult About()
        //{
        //    var username = Request.Cookies["UserName"].Value;
        //    ViewBag.Username = username;

        //    return View();
        //}
       

    }
}