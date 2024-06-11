﻿using Exam.Shared.DTOs;


namespace Exam.BusinessLogic.Services.Interfaces
{
    public interface IPersonService
    {
        Task<PersonCreateDTO> CreatePersonAsync(int userId, PersonCreateDTO personCreateDTO);
        Task<PersonGetDTO> GetByIdAsync(int userId, int personId);
        Task<IEnumerable<PersonGetDTO>> GetAllPersonsByUserIdAsync(int userId);
        Task<PersonUpdateDTO> UpdatePersonDetailsAsync(int userId, int personId, PersonUpdateDTO personUpdateDetailsDTO);
        Task<PersonDeleteDTO> DeletePersonAsync(int userId, int personId);

    }
}
