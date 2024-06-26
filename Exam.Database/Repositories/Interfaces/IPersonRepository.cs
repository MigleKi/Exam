﻿using Exam.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Database.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        Task CreatePersonAsync(Person person);
        Task<Person> GetByIdAsync(int id);
        Task<IEnumerable<Person>> GetAllPersonsByUserIdAsync(int userId);
        Task UpdatePersonAsync(Person person);
        Task<Person> DeletePersonAsync(int personId);

    }
}
