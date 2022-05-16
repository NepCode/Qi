using BusinessLogic.Data;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly DataDbContext _context;

        public EmpleadoRepository(DataDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Empleado>> Add(Empleado entity)
        {
            var response = new ServiceResponse<Empleado>();

            _context.Empleados.Add(entity);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                response.Data = entity;
            }
            else
            {
                response.Success = false;
            }
            return response; ;
            
          
        }

        public async Task<ServiceResponse<Empleado>> Delete(int id)
        {
            var response = new ServiceResponse<Empleado>();

            var dbPersonaFisica = await _context.Empleados.FindAsync(id);
            if (dbPersonaFisica == null)
            {
                response.Success = false;
                response.Data = dbPersonaFisica;
                return response;
            }

            dbPersonaFisica.FechaBaja = DateTime.Now;
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                response.Success = true;
            }
            else
            {
                response.Success = false;
            }            
            response.Data = dbPersonaFisica; 
            return response;
           
        }

        public async Task<ServiceResponseList<IReadOnlyList<Empleado>>> GetAll(EmpleadoParams? empleadoParams)
        {
            var results = await _context.Empleados
                 .Where
                 (
                    (e =>
                        ( string.IsNullOrEmpty(empleadoParams.Nombre) || e.Nombre.Contains(empleadoParams.Nombre) ) &&
                        ( string.IsNullOrEmpty(empleadoParams.Rfc) || e.Rfc.Contains(empleadoParams.Rfc) ) &&
                        ( !empleadoParams.Estatus.HasValue || ( (bool)empleadoParams.Estatus ? e.FechaBaja == null  :  e.FechaBaja != null ) ) 
                    ) 
                 )
                 .ToListAsync();
            return new ServiceResponseList<IReadOnlyList<Empleado>>()
            {
                Data = results
            };
        }

        public async Task<ServiceResponse<Empleado>> GetByIdWith(int id)
        {
            var result = await _context.Empleados.FirstOrDefaultAsync(pf => pf.Id == id);
            return new ServiceResponse<Empleado>() { 
                Data =  result
            } ;
        }

        public async Task<ServiceResponse<Empleado>> Update(int id, Empleado entity)
        {
            var response = new ServiceResponse<Empleado>();

            var dbPersonaFisica = await _context.Empleados.FindAsync(id);
            if (dbPersonaFisica == null)
            {
                response.Data = null; ;
                response.Success = false;
                return response;
            }

            dbPersonaFisica.EstadoCivil = entity.EstadoCivil;
            dbPersonaFisica.Direccion = entity.Direccion;
            dbPersonaFisica.Email = entity.Email;
            dbPersonaFisica.Telefono = entity.Telefono;
            dbPersonaFisica.Puesto = entity.Puesto;
            dbPersonaFisica.FechaBaja = entity.FechaBaja;

            await _context.SaveChangesAsync();

            
            response.Data = entity;
            response.Message = "Registro guardado";
            
            return response;

        }
    }
}
