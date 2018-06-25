using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.UserProfiles;
using System;
using System.Security;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            string siteUrl = "https://xxx.sharepoint.com";
            string username = "xxx@xxx.onmicrosoft.com";
            string passwords = "xxx";
            string targetUser = "xxx@xxx.onmicrosoft.com";

            ClientContext ctx = new ClientContext(siteUrl);

            var securepassword = new SecureString();

            foreach (char c in passwords)
            {
                securepassword.AppendChar(c);
            }

            ctx.Credentials = new SharePointOnlineCredentials(username, securepassword);

            // Get the PeopleManager object.
            PeopleManager peopleManager = new PeopleManager(ctx);

            // Retrieve specific properties by using the GetUserProfilePropertiesFor method. 
            // The returned collection contains only property values.
            string[] profilePropertyNames = new string[] { "PreferredName", "Department", "Title" };
            UserProfilePropertiesForUser profilePropertiesForUser = new UserProfilePropertiesForUser(ctx, targetUser, profilePropertyNames);
            IEnumerable<string> profilePropertyValues = peopleManager.GetUserProfilePropertiesFor(profilePropertiesForUser);

            // Load the request and run it on the server.
            ctx.Load(profilePropertiesForUser);
            ctx.ExecuteQuery();

            // Iterate through the property values.
            foreach (var value in profilePropertyValues)
            {
                Console.Write(value + "\\n");
            }
            Console.ReadKey(false);

        }
    }
}
