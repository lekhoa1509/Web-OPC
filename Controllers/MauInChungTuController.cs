using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web4.Models;
using System.Web.Mvc;
using System.Net;

namespace web4.Controllers
{
    public class MauInChungTuController : Controller
    {
        SAP_OPCEntities db = new SAP_OPCEntities();
        // GET: MauInChungTu
        public ActionResult Index()
        {

            List<tbl_MauInPhieuNhapXuong> OrderAndCustomerList = db.tbl_MauInPhieuNhapXuong.OrderByDescending(s => s.So_Ct).ToList();
            return View(OrderAndCustomerList);

        }

        [HttpPost]
        public ActionResult Index(MauInChungTu model)
        {
            if (model.So_Ct != "")
            {
                tbl_MauInPhieuNhapDetail or = db.tbl_MauInPhieuNhapDetail.SingleOrDefault(x => x.So_Ct == model.So_Ct);

                or.So_Ct = model.So_Ct;
                or.Ma_Vt = model.Ma_vt;
                or.so_lo = model.So_lo;
                or.Ten_Vt = model.Ten_Vt;
                or.Dvt = model.Dvt;
                or.so_luong = model.So_luong;
                db.SaveChanges();
            }

            return View(model);
        }






      

      


   



      



        // GET: Order/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_MauInPhieuNhapDetail order = db.tbl_MauInPhieuNhapDetail.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.tbl_MauInPhieuNhapXuong, "So_ct", "Ngay_Ct", order.So_Ct);
            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      
      



        // GET: Customer/Edit/5
        public ActionResult EditCustomer(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_MauInPhieuNhapXuong customer = db.tbl_MauInPhieuNhapXuong.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    }
}
