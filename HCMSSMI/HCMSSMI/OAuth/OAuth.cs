using HCMSSMI.Entities.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web;
using System.Security.Claims;
using System.Threading;
using HCMSSMI.Reader;
using HCMSSMI.Writer;

namespace HCMSSMI
{
    public class OAuth : IOAuth
    {
        #region Private Members

        private readonly IReaderService reader;
        private readonly IWriterService writer;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="reader">A single instance parameter from <see cref="IReaderService"/></param>
        /// <param name="writer">A single instance parameter from <see cref="IWriterService"/></param>
        public OAuth(IReaderService reader, IWriterService writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        #endregion


        public async Task<Users> UserIdentityAsync()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            string userName = identity.Claims.Where(c => c.Type == ClaimTypes.Name)
                                                      .Select(c => c.Value).SingleOrDefault();

            var loginUser = await reader.GetUserAccountDetail(userName);

            Users users = new Users();

            foreach (var user in loginUser.Data)
            {
                users = new Users()
                {
                    ID = user.ID,
                    Username = user.Username,
                    RoleID = user.RoleID,
                    Email = user.Email,
                    FullName = user.FullName,
                    Roles = new Roles()
                    {
                        ID = user.Roles.ID,
                        Name = user.Roles.Name,
                        Description = user.Roles.Description,
                    }
                };
            }

            return users;
        }

    }
}