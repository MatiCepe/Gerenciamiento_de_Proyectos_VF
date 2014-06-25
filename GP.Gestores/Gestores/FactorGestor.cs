using System;
using System.Collections.Generic;
using System.Linq;

using GP.DTO.DTO;
using GP.Dominio.Models;
using GP.Log;
using GP.Repositorio.Repositories;

namespace GP.Gestores.Gestores
{
    public class FactorGestor : IGestor<FactorDTO>, IDisposable
    {
        private Factor _factor;

        private readonly FactorRepository _factorRepository;
        private readonly ValorRepository _valorRepository;

        Logger _log;

        public FactorGestor()
        {
            try
            {
                _factorRepository = new FactorRepository();
                _valorRepository = new ValorRepository();

                _log = new Logger();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Save(FactorDTO entity)
        {
            try
            {
                var validate = this.Validate(entity);

                if (!string.IsNullOrEmpty(validate))
                {
                    throw new Exception(validate);
                }

                _factor = DTOFactorAFactor(entity);

                _factorRepository.Create(_factor);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void Edit(FactorDTO entity)
        {
            try
            {
                var validate = this.Validate(entity);

                if (!string.IsNullOrEmpty(validate))
                {
                    throw new Exception(validate);
                }

                _factor = DTOFactorAFactor(entity);

                _factorRepository.Update(_factor);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Enable(FactorDTO entity)
        {
            entity.Deshabilitado = 0;
            Edit(entity);
        }

        public void Disable(FactorDTO entity)
        {
            entity.Deshabilitado = 1;
            Edit(entity);
        }

        public FactorDTO ObtainId(int id)
        {
            try{
                var f = _factorRepository.GetByID(id);
                return FactorAFactorDTO(f);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IList<FactorDTO> ObtainAll()
        {
            try
            {
                Dispose();

                IList<Factor> factorLista = _factorRepository.GetAll();

                return factorLista.Select(FactorAFactorDTO).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string Validate(FactorDTO entidad)
        {
            var returnValue = string.Empty;

            if (string.IsNullOrEmpty(entidad.Nombre))
            {
                returnValue += "El campo nombre no puede ser vacio " + Environment.NewLine;
            }

            if (entidad.ValoresSeleccionados == null || entidad.ValoresSeleccionados.Count() != 3)
            {
                returnValue += "Debe seleccionar 3 valores al factor ";
            }

            return returnValue;
        }

        public void Dispose()
        {
            _factorRepository.Dispose();
        }

        private Factor DTOFactorAFactor(FactorDTO factorDto)
        {
            var factor = new Factor();

            if (factorDto.FactorId > 0)
            {
                factor = _factorRepository.GetByID(factorDto.FactorId);
            }

            factor.FactorId = factorDto.FactorId;
            factor.Nombre = factorDto.Nombre;
            factor.Deshabilitado = factorDto.Deshabilitado;

            if (factor.ValoresSeleccionados != null)
            {
                factor.ValoresSeleccionados.Clear();
            }
            else
            {
                factor.ValoresSeleccionados = new List<Valor>();
            }

            foreach (var valorDto in factorDto.ValoresSeleccionados)
            {
                if (valorDto.ValorId > 0)
                {
                    var valor = _factorRepository.GetValorById(valorDto.ValorId);
                    
                    factor.ValoresSeleccionados.Add(valor);
                }
            }

            return factor;
        }

        private FactorDTO FactorAFactorDTO(Factor factor)
        {
            var factorDto = new FactorDTO();

            factorDto.ValoresSeleccionados = new List<ValorDTO>();

            if (factor != null)
            {
                factorDto.FactorId = factor.FactorId;
                factorDto.Nombre = factor.Nombre;
                factorDto.Deshabilitado = factor.Deshabilitado;

                foreach (var valor in factor.ValoresSeleccionados)
                {
                    var valorDto = new ValorDTO
                                       {
                                           Influencia = valor.Influencia, ValorId = valor.ValorId, Nombre = valor.Nombre
                                       };

                    factorDto.ValoresSeleccionados.Add(valorDto);
                }
            }

            return factorDto;
        }
    }
}
