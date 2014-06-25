using System;
using System.Linq;
using GP.Gestores.Gestores;
using GP.MVP.Views;

namespace GP.MVP.Presenters
{
    public class FactorEditarPresenter: BasePresenter<IFactorEditarView>
    {
        private readonly IFactorEditarView _view;
        private readonly FactorGestor _factorGestor;
        private readonly ValorGestor _valorGestor;

        public FactorEditarPresenter(IFactorEditarView view, FactorGestor factorGestor, ValorGestor valorGestor) :
            base(view)
        {
            this._view = view;
            this._factorGestor = factorGestor;
            this._valorGestor = valorGestor;
        }

        public override void  Init()
        {
            try
            {
                _view.Guardar += OnGuardarFactor;

                GetFactor();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }

        public void OnGuardarFactor()
        {
            try
            {
                if (_view.IdFactor > 0)
                {
                    _factorGestor.Edit(_view.Factor);
                }
                else
                {
                    _factorGestor.Save(_view.Factor);
                }
            }
            catch (Exception e)
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

        public void ObtenerValoresNoSeleccionados()
        {

            var seleccionados = _factorGestor.ObtainId(_view.IdFactor).ValoresSeleccionados;

            var valores = _valorGestor.ObtainAll();

            if (seleccionados==null)
            {
                _view.ValoresNoSeleccionados = valores.ToList();
            }
            else
            {
                var valoresNoSeleccionados = _valorGestor.ObtainAll();

                valoresNoSeleccionados = valores.Aggregate(valoresNoSeleccionados, (current1, valorDTO) => seleccionados.Where(valorSublist => valorSublist.ValorId == valorDTO.ValorId).Aggregate(current1, (current, valorSublist) => current.Where(x => x.ValorId != valorSublist.ValorId).ToList()));
            
                _view.ValoresNoSeleccionados = valoresNoSeleccionados.ToList();
            }
            
        }

        private void GetFactor()
        {
            try
            {
                _view.Factor = _factorGestor.ObtainId(_view.IdFactor);
            }
            catch (Exception e)
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

    }
}
