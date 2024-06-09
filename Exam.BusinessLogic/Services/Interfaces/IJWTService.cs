using Exam.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.BusinessLogic.Services.Interfaces
{
    public interface IJWTService
    {
        string GetJWT(string user, int userID);
        bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt);
        User CreateUser(string username, string password);
    }
}
