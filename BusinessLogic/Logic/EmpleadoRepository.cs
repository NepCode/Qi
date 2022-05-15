using BusinessLogic.Data;
using BusinessLogic.Extension;
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

        public async Task<ServiceResponseList<IReadOnlyList<Empleado>>> GetAll()
        {
            var results = await _context.Empleados.ToListAsync();
            return new ServiceResponseList<IReadOnlyList<Empleado>>()
            {
                Data = results
            };
        }

        public async Task<ServiceResponseList<IReadOnlyList<Empleado>>> GetAllWithSpec(DtParameters dtParameters)
        {

            var searchBy = dtParameters.Search?.Value;
            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;
            if (dtParameters.Order != null)
            {
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }
            else
            {
                orderCriteria = "IdPersonaFisica";
                orderAscendingDirection = true;
            }
            var result = await _context.Empleados.ToListAsync();
            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Nombre != null && r.Nombre.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.ApellidoPaterno != null && r.ApellidoPaterno.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.ApellidoMaterno != null && r.ApellidoMaterno.ToUpper().Contains(searchBy.ToUpper()) )
                    .ToList();
            }
            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc).ToList() : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc).ToList();
            var filteredResultsCount = result.Count();
            var totalResultsCount = await _context.Empleados.CountAsync();


            //var results = await _context.TbPersonasFisicas.ToListAsync();
            return new ServiceResponseList<IReadOnlyList<Empleado>>()
            {
                draw = dtParameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                Data = result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToList()
            };
        }

        public async Task<ServiceResponse<Empleado>> GetByIdWithSpec(int id)
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
