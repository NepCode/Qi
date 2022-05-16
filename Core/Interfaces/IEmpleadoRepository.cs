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
        Task<ServiceResponse<Empleado>> GetByIdWith(int id);
        Task<ServiceResponseList<IReadOnlyList<Empleado>>> GetAll(EmpleadoParams? empleadoParams);
        Task<ServiceResponse<Empleado>> Add(Empleado entity);
        Task<ServiceResponse<Empleado>> Update(int id, Empleado entity);
        Task<ServiceResponse<Empleado>> Delete(int id);
    }
}
