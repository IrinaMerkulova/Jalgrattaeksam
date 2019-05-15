using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JalgrattaeksamMVC.Models;

namespace JalgrattaeksamMVC.Controllers
{
	[Authorize]
	public class JalgrattaeksamsController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		// GET: Jalgrattaeksams
		public ActionResult Index()
		{
			return View(db.Jalgrattaeksams.ToList());
		}
		public ActionResult Theory()
		{
			var model = db.Jalgrattaeksams.
				Where(m=>m.Teooria==-1).
				ToList();
			return View(model);
		}
		public ActionResult Slalom()
		{
            //TODO Vaata Theory actioni koodi ja lisa siia sobiv kitsendus (where)
			var model = db.Jalgrattaeksams.
                Where(m => m.Teooria >= 30).
				ToList();
			return View(model);
		}
		public ActionResult PassFail(int? id,string part,int result)
		{			
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Jalgrattaeksam jalgrattaeksam = db.Jalgrattaeksams.Find(id);
			if (jalgrattaeksam == null)
			{
				return HttpNotFound();
			}
			if (ModelState.IsValid)
			{
				switch (part)
				{
					case "Slalom": { jalgrattaeksam.Slaalom = result; break; }
					case "Circle": { jalgrattaeksam.Ringtee = result; break; }
					case "Street":	{
							jalgrattaeksam.Uulits = result;
							if (result==1)
							{
								jalgrattaeksam.Luba = 2;
							}
							break;
						}
					case "License": { jalgrattaeksam.Luba = result; break; }
					default:	{	return HttpNotFound();	}						
				}
				db.Entry(jalgrattaeksam).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("License");
			}
			return RedirectToAction("License");
		}

		public ActionResult Circle()
		{
			var model = db.Jalgrattaeksams.
				Where(m=>m.Teooria>=9&&m.Ringtee==-1).
				ToList();
			return View(model);
		}
		public ActionResult Street()
		{
			var model = db.Jalgrattaeksams.
				Where(m => m.Teooria >= 9 &&
															m.Ringtee == 1 &&
															m.Slaalom==1 &&
															m.Uulits==-1).
										ToList();
			return View(model);
		}
		public ActionResult License()
		{
            //TODO Muuda lubade sorteerimist nii, et väljastatud lubadega kirjed on tabeli lõpus
			var model = db.Jalgrattaeksams.
				Where(m => m.Teooria >= 0).
				OrderBy(m=>m.Luba).
				ThenBy(m=>m.Perenimi).
				ToList();
			return View(model);
		}
		// GET: Jalgrattaeksams/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Jalgrattaeksam jalgrattaeksam = db.Jalgrattaeksams.Find(id);
			if (jalgrattaeksam == null)
			{
				return HttpNotFound();
			}
			return View(jalgrattaeksam);
		}

		// GET: Jalgrattaeksams/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Jalgrattaeksams/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,Eesnimi,Perenimi,Teooria,Slaalom,Ringtee,Uulits,Luba")] Jalgrattaeksam jalgrattaeksam)
		{
			if (ModelState.IsValid)
			{
				db.Jalgrattaeksams.Add(jalgrattaeksam);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(jalgrattaeksam);
		}
		[AllowAnonymous]
		// GET: Jalgrattaeksams/Register
		public ActionResult Register()
		{
			return View();
		}

		// POST: Jalgrattaeksams/Register
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Register([Bind(Include = "Eesnimi,Perenimi")] Jalgrattaeksam jalgrattaeksam)
		{
			if (ModelState.IsValid)
			{				
				db.Jalgrattaeksams.Add(jalgrattaeksam);
				db.SaveChanges();
				return RedirectToAction("Theory");
			}

			return View(jalgrattaeksam);
		}
		// GET: Jalgrattaeksams/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Jalgrattaeksam jalgrattaeksam = db.Jalgrattaeksams.Find(id);
			if (jalgrattaeksam == null)
			{
				return HttpNotFound();
			}
			return View(jalgrattaeksam);
		}

		// POST: Jalgrattaeksams/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Eesnimi,Perenimi,Teooria,Slaalom,Ringtee,Uulits,Luba")] Jalgrattaeksam jalgrattaeksam)
		{
			if (ModelState.IsValid)
			{
				db.Entry(jalgrattaeksam).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(jalgrattaeksam);
		}
		// GET: Jalgrattaeksams/Edit/5
		public ActionResult TheoryResult(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Jalgrattaeksam jalgrattaeksam = db.Jalgrattaeksams.Find(id);
			if (jalgrattaeksam == null)
			{
				return HttpNotFound();
			}
			return View(jalgrattaeksam);
		}

		// POST: Jalgrattaeksams/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult TheoryResult([Bind(Include = "Id,Eesnimi,Perenimi,Teooria,Slaalom,Ringtee,Uulits,Luba")] Jalgrattaeksam jalgrattaeksam)
		{
			if (ModelState.IsValid)
			{
				db.Entry(jalgrattaeksam).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("License");
			}
			return View(jalgrattaeksam);
		}
		// GET: Jalgrattaeksams/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Jalgrattaeksam jalgrattaeksam = db.Jalgrattaeksams.Find(id);
			if (jalgrattaeksam == null)
			{
				return HttpNotFound();
			}
			return View(jalgrattaeksam);
		}

		// POST: Jalgrattaeksams/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Jalgrattaeksam jalgrattaeksam = db.Jalgrattaeksams.Find(id);
			db.Jalgrattaeksams.Remove(jalgrattaeksam);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
