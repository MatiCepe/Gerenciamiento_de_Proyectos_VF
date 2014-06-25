using System;
using GP.DTO.DTO;
using GP.Gestores.Gestores;
using GP.MVP.Views;

namespace GP.MVP.Presenters
{
    public class ValorEditarPresenter : BasePresenter<IValorEditarView>
    {
        private readonly ValorGestor _valorGestor;
        private readonly IValorEditarView _view;
        private int _valorId;
        private ValorDTO _valorDTO;

         public ValorEditarPresenter(IValorEditarView view, ValorGestor valorGestor):
            base(view)
        {
            this._view = view;
            this._valorGestor = valorGestor;
        }

        public override void Init()
        {
            try
            {
                this._view.Guardar += OnClickGuardar;
                this._view.Limpiar += OnClickLimpiar;
                this._view.Volver += OnClickVolver;

                if (this._valorId > 0)
                {
                    this._valorDTO = this._valorGestor.ObtainId(this._valorId);
                    this._view.MostrarValor(this._valorId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public int ValorId
        {
            get { return _valorId; }
            set { _valorId = value; }
        }

        public ValorDTO ValorDTO
        {
            get { return _valorDTO; }
            set { _valorDTO = value; }
        }

        public void OnClickGuardar()
        {
            try
            {
                if (this._valorId > 0)
                    _valorGestor.Edit(this._valorDTO);
                else
                    _valorGestor.Save(this._valorDTO);
            }
            catch (Exception e)
            {
                throw;
            }
            
        }

        public void OnClickLimpiar()
        {
            this._view.MostrarValor(0);
        }

        public void OnClickVolver()
        {
            
        }
    }
}
