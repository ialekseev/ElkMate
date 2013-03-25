﻿using System.Web.Mvc;

namespace ElkMate.Web.Controllers
{
    public class BaseController: Controller
    {
        public JsonResult JsonSuccess(object info)
        {
            return Json(new { ok = true, data = info });
        }

        public JsonResult JsonSuccess()
        {
            return Json(new { ok = true });
        }

        public JsonResult JsonFailure(string info)
        {
            return Json(new { ok = false, error = info });
        }

        public JsonResult JsonFailure()
        {
            return Json(new { ok = false });
        }

        public JsonResult JsonStatus(bool ok)
        {
            return Json(new { ok });
        }
    }
}
