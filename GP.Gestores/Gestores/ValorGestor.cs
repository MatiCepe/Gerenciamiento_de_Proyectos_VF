﻿
using System;
using System.Collections.Generic;
using System.Text;
using GP.DTO.DTO;
using GP.Dominio.Models;
using GP.Log;
using GP.Repositorio.Repositories;

namespace GP.Gestores.Gestores
{
    public class ValorGestor : IGestor<ValorDTO>, IDisposable
    {
        private Valor _valor;
        private readonly ValorRepository _valorRepository;

        readonly Logger _log;

       public ValorGestor()
       {
           try
           {
               _valorRepository = new ValorRepository();

               _log = new Logger();
           }
           catch (Exception e)
            {
                throw e;
            }
       }

        public void Save(ValorDTO entity)
        {
            try
            {
                var isValid = Validate(entity);
                if (string.IsNullOrEmpty(isValid))
                {
                    _valor = DTOaValor(entity);
                    _valorRepository.Create(_valor);    
                }
                else
                {
                    _log.WriteLog(isValid);
                    throw new Exception(isValid);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void Edit(ValorDTO entity)
        {
            var isValid = Validate(entity);
            try
            {
                if (string.IsNullOrEmpty(isValid))
                {
                    Valor f = DTOaValor(entity);
                    _valor = f;
                    _valorRepository.Update(_valor);
                }
                else
                {
                    _log.WriteLog(isValid);
                    throw new Exception(isValid);
                }
            }
            catch (Exception e)
            {
                _log.WriteLog(e.ToString());
                throw e;
            }
        }

        public void Enable(ValorDTO entity)
        {
            entity.Deshabilitado = 0;
            Edit(entity);
        }

        public void Disable(ValorDTO entity)
        {
            entity.Deshabilitado = 1;
            Edit(entity);
        }

        public ValorDTO ObtainId(int id)
        {
            try
            {
                var f = _valorRepository.GetByID(id);
                return ModeloaDTO(f);
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public IList<ValorDTO> ObtainAll()
        {
            Dispose();

            try
            {
                IList<ValorDTO> valorDTOLista = new List<ValorDTO>();
                IList<Valor> valorLista = _valorRepository.GetAll();
                foreach (Valor f in valorLista)
                    valorDTOLista.Add(ModeloaDTO(f));
                return valorDTOLista;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public String Validate(ValorDTO entidad)
        {
            var s = new StringBuilder().Clear();
            
            if (!((entidad.Influencia) >= 0 && (entidad.Influencia) <= 2))
                s.Append("La Influencia del Valor es Incorrecta.");
            
            if (String.IsNullOrEmpty(entidad.Nombre))
                s.Append("El Nombre no puede ser vacio.");

            if (!((entidad.Deshabilitado) == 0 || (entidad.Deshabilitado) == 1))
                s.Append("La opcion deshabilitar debe valer 0 (no) o 1 (si)");

            return s.ToString();
        }

        public void Dispose()
        {
            _valorRepository.Dispose();
        }

        private Valor DTOaValor(ValorDTO _valDTO)
        {
            var valor = new Valor();

            if (_valDTO.ValorId > 0)
                valor = _valorRepository.GetByID(_valDTO.ValorId);

            valor.ValorId = _valDTO.ValorId;
            valor.Nombre = _valDTO.Nombre;
            valor.Deshabilitado = _valDTO.Deshabilitado;
            valor.Influencia = _valDTO.Influencia;

            return valor;
        }

        private ValorDTO ModeloaDTO(Valor _val)
        {
            var _vDTO = new ValorDTO();

            _vDTO.ValorId = _val.ValorId;
            _vDTO.Nombre = _val.Nombre;
            _vDTO.Deshabilitado = _val.Deshabilitado;
            _vDTO.Influencia = _val.Influencia;

            return _vDTO;
        }
    }
}
