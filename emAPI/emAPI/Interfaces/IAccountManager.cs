
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * IAccountManager.cs
 * Code source: Handwritten
 */
		

using System;
using emAPI.ClassLibrary;

namespace emAPI.Interfaces
{
    /// <summary>
    /// Interface for AccountManager objects
    /// </summary>
    public interface IAccountManager
    {
        void addPropertyToUser(int propertyId, int userId);
        bool checkPassword(string submittedPassword, int userId);
        int createUser(string forname, string surname, string email, string username, string cryptedPassword);
        User editUser(int userId, string userJSON);
        bool emailIsUnique(string email);
        System.Collections.Generic.List<AnonymousProperty> getComparativeCostsForUser(int userId);
        System.Collections.Generic.List<Property> getPropertiesForUser(int userId);
        User getUser(int userId);
        bool updatePassword(int userId, string newPassword);
        bool usernameIsUnique(string username);
        int validateUser(string username, string password);
    }
}
