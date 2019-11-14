
#region Additional Namespaces
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;
using System.Data.Entity;
using WebApp.Models;
using ChinookSystem.BLL;
#endregion

namespace WebApp.Security
{//inherits from create db if not exist goes to db to check if you have seurity tables if now it will execute this class for you. 
    //if you have them it will not execute. 
    public class SecurityDbContextInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            #region Seed the roles
            //grab a hold of the role manager identity role defination 
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //goes to get the app settings the webappsettings.config tis will bring all the roles in using the split method list of strings
            var startupRoles = ConfigurationManager.AppSettings["startupRoles"].Split(';');
            foreach (var role in startupRoles)
                //use the create to add the names for the role areas this is bad because you will have to maually type all the roles in the org.
                roleManager.Create(new IdentityRole { Name = role });

            //taje roles from your database such as a poitions table or 
            //off some other data record. 
            //we have a title column on the employees table which hold the roles. 

    
         
            #endregion

            #region Seed the users
            string adminUser = ConfigurationManager.AppSettings["adminUserName"];
            string adminRole = ConfigurationManager.AppSettings["adminRole"];
            string adminEmail = ConfigurationManager.AppSettings["adminEmail"];
            string adminPassword = ConfigurationManager.AppSettings["adminPassword"];
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            //we creating the webmaster here they are not tied to any particular employee
            var result = userManager.Create(new ApplicationUser
            {
                UserName = adminUser,
                Email = adminEmail
            }, adminPassword);
            if (result.Succeeded)
                userManager.AddToRole(userManager.FindByName(adminUser).Id, adminRole);

            //string customerUser = ConfigurationManager.AppSettings["customerUserName"];
            //string customerRole = ConfigurationManager.AppSettings["customerRole"];
            //string customerEmail = ConfigurationManager.AppSettings["customerEmail"];

            //this is an example of hard coding a new user 
            string userPassword = ConfigurationManager.AppSettings["newUserPassword"];
          
            result = userManager.Create(new ApplicationUser
            {
                UserName = "HansenB",
                Email = "HansenB@hotmail.somewhere.ca",
                CustomerId = 4
            }, userPassword);
            if (result.Succeeded)
                userManager.AddToRole(userManager.FindByName("HansenB").Id, "Customers");

            //seeding employees from the employee table 
            //TODO:
            //Retrieve list of employee from the database
            //foreachemployee
            //username such as lastname and first inital possible add a number logic add number or increment 
            //Email of the employee or null or generate one add @ chinook.somewhere.ca to user name 
            //Employee id is the ok of the employee record
            //se the appsettingnew user password fot the password 
            //Succeeded, find the user name need the role can come from the employee record 



            #endregion

            // ... etc. ...

            base.Seed(context);
        }
    }
}