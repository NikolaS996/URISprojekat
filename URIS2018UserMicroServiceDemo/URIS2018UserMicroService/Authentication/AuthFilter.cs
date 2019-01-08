using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace URIS2018UserMicroService.Authentication
{
    public class AuthFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext filterContext)
        {
            URIS2018UserMicroServiceDemo.Models.User loadedUser = null;
            string email = filterContext.Request.Headers.GetValues("email").First();
            string password = filterContext.Request.Headers.GetValues("password").First();

            if(!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password))
            {
                loadedUser = URIS2018UserMicroServiceDemo.DataAccess.UserDB.GetUser(email, password);
            }

            if(loadedUser == null || !loadedUser.Active)
            {
                filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            base.OnAuthorization(filterContext);
        }
    }
}