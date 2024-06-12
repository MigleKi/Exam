using Exam.Shared.DTOs;


namespace Exam.BusinessLogic.Services.Interfaces
{
    public interface IPersonService
    {
        Task<PersonCreateDTO> CreatePersonAsync(int userId, PersonCreateDTO personCreateDTO);
        Task<PersonGetDTO> GetByIdAsync(int userId, int personId);
        Task<PersonUpdateDTO> UpdatePersonAsync(int userId, int personId, PersonUpdateDTO personUpdateDetailsDTO);
        Task<PersonDeleteDTO> DeletePersonAsync(int personId, int userId);

    }
}
