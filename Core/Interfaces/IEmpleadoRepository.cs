using BusinessLogic.Data;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IEmpleadoRepository
    {
        Task<ServiceResponse<Empleado>> GetByIdWithSpec(int id);
        Task<ServiceResponseList<IReadOnlyList<Empleado>>> GetAllWithSpec(DtParameters dtParameters);
        Task<ServiceResponseList<IReadOnlyList<Empleado>>> GetAll();
        Task<ServiceResponse<Empleado>> Add(Empleado entity);
        Task<ServiceResponse<Empleado>> Update(int id, Empleado entity);
        Task<ServiceResponse<Empleado>> Delete(int id);
    }
}
