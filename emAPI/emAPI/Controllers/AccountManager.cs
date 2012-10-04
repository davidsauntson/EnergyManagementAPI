
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * AccountManager.cs
 * Code source: Handwritten
 */
		


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using emAPI.ClassLibrary;
using emAPI.Interfaces;

namespace emAPI.Controllers
{
    /// <summary>
    /// Controller object for User model object related operations.
    /// </summary>
    internal class AccountManager : IAccountManager
    {
        private EMMediator mediator = new EMMediator();

        internal AccountManager()
        {
            mediator = new EMMediator();
        }

//* * * DATA ACCESS METHODS

        public User getUser(int userId)
        {
            User user = mediator.DataManager.getUser(userId);
            return user;
        }


        public bool usernameIsUnique(string username)
        {
            User user = new User();

            try
            {
                user = mediator.DataManager.getUserByUsername(username);
            }
            catch
            {
                user = null;
            }

            return user == null;
        }

        public bool emailIsUnique(string email)
        {
            User user = new User();

            try
            {
                user = mediator.DataManager.getUserByEmail(email);
            }

            catch
            {
                user = null;
            }

            return user == null;
        }

        public List<Property> getPropertiesForUser(int userId)
        {
            List<Property> properties = mediator.DataManager.getProperties(userId);
            return properties;
        }


//* * * CREATION METHODS

        public int createUser(string forname, string surname, string email, string username, string cryptedPassword)
        {
            User user = new User
            {
               Forename = forname, Surname = surname, Email = email, Username = username, Password = cryptedPassword
            };

            return mediator.DataManager.saveUser(user);
        }


//* * * UPDATE METHODS

        public User editUser(int userId, string userJSON)
        {
            User updatedUser = EMConverter.fromJSONtoA<User>(userJSON);
            updatedUser = mediator.DataManager.editUser(userId, updatedUser);
            return updatedUser;
        }

//* * * OTHER METHODS

        public bool checkPassword(string submittedPassword, int userId)
        {
            string storedPassword = mediator.DataManager.getUserPassword(userId);
            return submittedPassword == storedPassword;
        }

        public void addPropertyToUser(int propertyId, int userId)
        {
            mediator.DataManager.addPropertyToUser(propertyId, userId);
        }

        public int validateUser(string username, string password)
        {
            User user = new User();
            try
            {
                user = mediator.DataManager.getUserByUsername(username);
            }
            catch
            {
                return 0;
            }

            string storedPassword = mediator.DataManager.getUserPassword(user.Id);
            if (storedPassword == password)
            {
                return user.Id;
            }
            else
            {
                return 0;
            }
        }

        public List<AnonymousProperty> getComparativeCostsForUser(int userId)
        {
            List<Property> usersProperties = mediator.DataManager.getProperties(userId);
            List<AnonymousProperty> anonProperties = new List<AnonymousProperty>();

            List<AnonymousProperty> allProperties = EMDatabaseStats.getAllPropertyAnnualCosts();
            List<AnonymousProperty> result = new List<AnonymousProperty>();

            foreach (AnonymousProperty anonProperty in allProperties)
            {
                AnonymousProperty resultProperty = new AnonymousProperty();
                resultProperty.Id = anonProperty.Id;
                resultProperty.Postcode = anonProperty.Postcode;
                resultProperty.AnnualCost = anonProperty.AnnualCost;
                resultProperty.isUsers = usersProperties.ToList().Exists(p => p.Id == anonProperty.Id);

                result.Add(resultProperty);
            }

            return result;
        
        }

        public bool updatePassword(int userId, string newPassword)
        {
            bool success = true;

            try
            {
                User user = mediator.DataManager.getUser(userId);
                user.Password = newPassword;

                mediator.DataManager.editUser(userId, user);
            }
            catch
            {
                success = false;
            }

            return success;
            
        }
    }
}
