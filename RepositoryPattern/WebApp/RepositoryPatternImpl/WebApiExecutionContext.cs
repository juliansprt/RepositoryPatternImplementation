using Newtonsoft.Json;
using RepositoryPattern;
using RepositoryPattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.RepositoryPatternImpl
{
    public interface IWebExecutionContext : IExecutionContext
    {
        T GetCookie<T>(string key) where T : class;

    }

    [Export]
    public class WebApiExecutionContext : IWebExecutionContext
    {
        #region IExecutionContext Members

        public T GetObject<T>(string key)
        {
            var result = HttpContext.Current.Items[key];
            return result != null ? (T)result : default(T);
        }

        public T GetCookie<T>(string key) where T : class
        {
            T data = null;
            var cookie = HttpContext.Current.Request.Cookies[key];
            if (cookie != null)
            {
                data = JsonConvert.DeserializeObject<T>(HttpUtility.UrlDecode(cookie.Value));
            }

            return data;
        }



        private static void RemoveCookie(string cookieName)
        {
            HttpContext current = HttpContext.Current;

            if (current.Request.Cookies[cookieName] != null)
            {
                HttpCookie myCookie = new HttpCookie(cookieName);
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                current.Response.Cookies.Add(myCookie);
            }
        }

        public void SetObject(string key, object val)
        {
            if (HttpContext.Current.Items.Contains(key))
                this.DoRemoveObject(key);
            HttpContext.Current.Items.Add(key, val);
        }

        public void RemoveObject(string key)
        {
            this.DoRemoveObject(key);
        }

        public void Dispose()
        {
        }

        #endregion

        protected void DoRemoveObject(string key)
        {
            HttpContext.Current.Items.Remove(key);
        }
    }
}