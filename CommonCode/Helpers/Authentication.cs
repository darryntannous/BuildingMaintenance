using BuildingMaintenance.Models;
using BuildingMaintenance.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;



namespace BuildingMaintenance.CommonCode.Helpers
{
    public interface IIAuthenticationService
    {
        void AddUserInSession(ApplicationUser user);
        ApplicationUser GetUserFromSession();

        void ResetSession();

        bool IsAdminUser();

        bool IsLoggedIn();

    }
    public class AuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        private const string USER_SESSION_KEY = "SESSION_USER";
        private ApplicationUser user;

        public AuthenticationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            user = _session.GetObjectFromJson<ApplicationUser>(USER_SESSION_KEY);
        }

        public bool IsAdminUser { get { return user.IsAdminUser; } }
        public bool IsLoggedIn { get { return user != null; } }
        public ApplicationUser User { get { return user; } }


        public void ResetSession()
        {
            _session.Clear();
            if (_session.Keys.Count() > 0)
            {
                _session.Set(USER_SESSION_KEY, null);
            }
        }

        public void AddUserInSession(ApplicationUser user)
        {
            _session.SetObjectAsJson(USER_SESSION_KEY, user);
        }
    }
}
