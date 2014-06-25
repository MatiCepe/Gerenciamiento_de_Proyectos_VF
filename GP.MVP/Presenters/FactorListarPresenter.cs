using System;
using System.Globalization;
using System.Linq;
using GP.Gestores.Gestores;
using GP.MVP.Views;

namespace GP.MVP.Presenters
{
    public class FactorListarPresenter: BasePresenter<IFactorListarView>
    {
        private readonly IFactorListarView _view;
        private readonly FactorGestor _factorGestor;

        public FactorListarPresenter(IFactorListarView view, FactorGestor factorGestor) :
            base(view)
        {
            this._view = view;
            this._factorGestor = factorGestor;
        }

        public override void  Init()
        {
           try
            {
                _view.BuscarFactores += OnBuscarFactores;
                _view.Seleccionado += OnSeleccionarFactor;

                OnBuscarFactores();
           }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

        public void OnSeleccionarFactor()
        {
            try
            {
                if (_view.FactorSeleccionado > 0)
                
                    _view.MostrarDetalleFactor(_factorGestor.ObtainAll().FirstOrDefault(f => f.FactorId == _view.FactorSeleccionado));
                
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        public void OnBuscarFactores()
        {
            try
            {
                var resutl = _factorGestor.ObtainAll();
                if (!string.IsNullOrEmpty(_view.FiltroNombre))
                {
                    resutl = resutl.Where(f => f.Nombre.StartsWith(_view.FiltroNombre, true, CultureInfo.CurrentCulture)).ToList();
                }

                _view.Factores = resutl;
            }
            catch(Exception e)
            {
               var mensajeError = string.Empty;

                while (e != null)
                {
                    mensajeError = e.Message + Environment.NewLine;
                    e = e.InnerException;
                }

                throw new Exception("Error: " + Environment.NewLine + mensajeError);
               
            }
            
        }

        public void OnEditarFactor()
        {
            if (_view.FactorSeleccionado > 0)
            {

            }
        }
    }
}
