using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AspMvc.Controllers
{
    public static class Validation
    {
        public static void ValidationModel(this Controller controller, object model)
        {
            controller.ModelState.Clear();

            var validationResult = Utils.ValidateModel(model);

            foreach( var result in validationResult )
            {
                foreach( var name in result.MemberNames)
                {
                    controller.ModelState.AddModelError(name, result.ErrorMessage);
                }
            }
        }
    }
}