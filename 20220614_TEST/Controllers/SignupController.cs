using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _20220614_TEST.Models;

namespace _20220614_TEST.Controllers
{
    public class SignupController : Controller
    {
        private readonly DataContext _context;

        public SignupController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.tblActiveItem.Select(x => new Sign { cItemID = x.cItemID, cItemName = x.cItemName }).ToListAsync();
            foreach (var item in list)
            {
                item.cItemNum = await _context.tblSignupItem.Where(x => x.cItemID == item.cItemID)?.CountAsync();
            }
            var result = list;
            return View(result);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.tblSignup == null)
            {
                return NotFound();
            }
            var activy = await _context.tblActiveItem.FindAsync(id);
            var act = await _context.tblSignupItem.Where(x => x.cItemID == activy.cItemID).ToListAsync();
            List<tblSignup> result = new List<tblSignup>();
            foreach (var item in act)
            {
                var tblSignup = await _context.tblSignup.Where(x => x.cMobile == item.cMobile).FirstOrDefaultAsync();
                result.Add(tblSignup);
            }


            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            var detail = await _context.tblActiveItem.Select(x => new SelectListItem { Value = x.cItemID.ToString(), Text = x.cActiveDt + " " + x.cItemName }).ToListAsync();
            ViewBag.Activy = detail;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SignDetail tblSignup)
        {
            if (ModelState.IsValid)
            {
                var haveData = _context.tblSignup.Where(x => x.cMobile == tblSignup.cMobile).Count() > 0;
                if (haveData)
                {
                    ViewBag.Error = "手機號碼不可重複";
                    var detail = await _context.tblActiveItem.Select(x => new SelectListItem { Value = x.cItemID.ToString(), Text = x.cActiveDt + " " + x.cItemName }).ToListAsync();
                    ViewBag.Activy = detail;
                    return View(tblSignup);
                }
                tblSignup.cCreateDT = DateTime.Now;
                var signData = new tblSignup() { cName = tblSignup.cName, cMobile = tblSignup.cMobile, cEmail = tblSignup.cEmail, cCreateDT = tblSignup.cCreateDT };
                var signItem = new tblSignupItem() { cItemID = tblSignup.cItemID, cMobile = signData.cMobile };
                //_context.Add(tblSignup);
                _context.tblSignup.Add(signData);
                _context.tblSignupItem.Add(signItem);
                await _context.SaveChangesAsync();

                var list = await _context.tblActiveItem.Select(x => new Sign { cItemID = x.cItemID, cItemName = x.cItemName }).ToListAsync();
                foreach (var item in list)
                {
                    item.cItemNum = await _context.tblSignupItem.Where(x => x.cItemID == item.cItemID)?.CountAsync();
                }
                var result = list;
                ViewBag.Result = "報名成功，報名人員：" + signData.cName + " 手機號碼：" + signData.cMobile + " 報名時間：" + signData.cCreateDT;
                return View(nameof(Index), result);
            }
            return View(tblSignup);
        }

        // GET: Signup/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var data = await _context.tblSignup.Where(x => x.cMobile == id).Select(x => new SignDetail { cMobile = x.cMobile, cEmail = x.cEmail, cName = x.cName, cCreateDT = x.cCreateDT }).FirstOrDefaultAsync();

            var act = await _context.tblSignupItem.Where(x => x.cMobile == data.cMobile).FirstOrDefaultAsync();

            data.cItemID = act.cItemID;

            var detail = await _context.tblActiveItem.Select(x => new SelectListItem { Value = x.cItemID.ToString(), Text = x.cActiveDt + " " + x.cItemName }).ToListAsync();
            ViewBag.Activy = detail;
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, SignDetail tblSignup)
        {
            if (id != tblSignup.cMobile)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var data = await _context.tblSignup.Where(x => x.cMobile == id).FirstOrDefaultAsync();
                    data.cName = tblSignup.cName;
                    data.cEmail = tblSignup.cEmail;
                    data.cCreateDT = tblSignup.cCreateDT;
                    _context.tblSignup.Update(data);

                    var signitem = await _context.tblSignupItem.Where(x => x.cMobile == data.cMobile).FirstOrDefaultAsync();
                    _context.tblSignupItem.Remove(signitem);
                    var updateitem = new tblSignupItem();
                    updateitem.cMobile = tblSignup.cMobile;
                    updateitem.cItemID = tblSignup.cItemID;

                    _context.tblSignupItem.Add(updateitem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tblSignupExists(tblSignup.cMobile))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tblSignup);
        }
        public async Task<IActionResult> Search()
        {
            var detail = await _context.tblActiveItem.Select(x => new SelectListItem { Value = x.cItemID.ToString(), Text = x.cActiveDt + " " + x.cItemName }).ToListAsync();
            ViewBag.Activy = detail;
            var data = new List<SignDetail>();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Search(string? Name, string? Mobile, int? itemID)
        {
            var list = await _context.tblSignup.Select(x => new SignDetail { cMobile = x.cMobile, cEmail = x.cEmail, cName = x.cName, cCreateDT = x.cCreateDT }).ToListAsync();
            var signitem = await _context.tblSignupItem.ToListAsync();
            var act = await _context.tblActiveItem.ToListAsync();
            foreach (var item in list)
            {
                item.cItemID = signitem.Where(x => x.cMobile == item.cMobile).FirstOrDefault().cItemID;
                var data = act.Where(x => x.cItemID == item.cItemID).FirstOrDefault();
                item.cItemData = data.cActiveDt + " " + data.cItemName;
            }
            if (!string.IsNullOrEmpty(Name))
            {
                list = list.Where(x => x.cName.Contains(Name)).ToList();
            }
            if (!string.IsNullOrEmpty(Mobile))
            {
                list = list.Where(x => x.cMobile.Contains(Mobile)).ToList();
            }
            if (itemID != null)
            {
                list = list.Where(x => x.cItemID == itemID).ToList();
            }
            var detail = await _context.tblActiveItem.Select(x => new SelectListItem { Value = x.cItemID.ToString(), Text = x.cActiveDt + " " + x.cItemName }).ToListAsync();
            ViewBag.Activy = detail;
            return View(list);
        }
        // GET: Signup/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var sign = await _context.tblSignup.Where(x => x.cMobile == id).FirstOrDefaultAsync();
            if (sign != null)
            {
                _context.tblSignup.Remove(sign);
            }
            var signitem = await _context.tblSignupItem.Where(x => x.cMobile == id).FirstOrDefaultAsync();
            if (signitem != null)
            {
                _context.tblSignupItem.Remove(signitem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details));
        }

        // POST: Signup/Delete/5
        //[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.tblSignup == null)
            {
                return Problem("Entity set 'DataContext.tblSignup'  is null.");
            }
            var tblSignup = await _context.tblSignup.FindAsync(id);
            if (tblSignup != null)
            {
                _context.tblSignup.Remove(tblSignup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tblSignupExists(string id)
        {
            return (_context.tblSignup?.Any(e => e.cMobile == id)).GetValueOrDefault();
        }
    }
}
