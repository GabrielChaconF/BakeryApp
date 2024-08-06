using BakeryApp_v1.Models;
using System.Data.Entity;

namespace BakeryApp_v1.DAO
{
    public class TiposDePagoDAOImpl : TiposPagoDAO
    {
        private readonly BakeryAppContext dbContext;
        public TiposDePagoDAOImpl(BakeryAppContext dbContext)
        {
            this.dbContext = dbContext; 
        }

        public async Task<IEnumerable<Tipospago>> ObtenerTodosLosTiposDePago()
        {
            IEnumerable<Tipospago> todosLosTiposDePago = await dbContext.Tipospagos.ToListAsync();
            return todosLosTiposDePago;
        }
    }
}
