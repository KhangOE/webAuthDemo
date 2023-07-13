using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using System.Security.Claims;

namespace web_authentication.filter
{
    public class UpperAgeRequireAttribute : Attribute, IAuthorizationFilter
    {
        private readonly int _age;

        public UpperAgeRequireAttribute(int age)
        {
            _age = age;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var birthday = user.Claims.Where(x => x.Type == ClaimTypes.DateOfBirth).Select(x => x.Value).FirstOrDefault();
            DateTime birthDayDateTime = DateTime.Parse(birthday, null);
            if (CalculatingAge(birthDayDateTime) < _age)
            {
                context.Result = new ForbidResult("require " + _age + " +");
                return;
            }
        }
        private int CalculatingAge(DateTime ngaySinh)
        {
            DateTime ngayHienTai = DateTime.Today;
            int tuoi = ngayHienTai.Year - ngaySinh.Year;
            if (ngayHienTai < ngaySinh.AddYears(tuoi))
            {
                tuoi--;
            }

            return tuoi;
        }
    }
}
