﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(ExemploLoginIdentity.Startup))]

namespace ExemploLoginIdentity
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
          
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Acesso/Login")
            });
        }
    }
}
