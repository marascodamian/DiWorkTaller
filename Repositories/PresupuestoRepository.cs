using System.Data.SqlClient;
using System.Data;
using TallerMecanicoDiWork.Dtos;
using TallerMecanicoDiWork.DtosDAO;
using Dapper;
using TallerMecanicoDiWork.Interfaces;

namespace TallerMecanicoDiWork.Repositories
{
    public class PresupuestoRepository : IPresupuestoRepository
    {
        private readonly IConfiguration _config;

        public PresupuestoRepository(IConfiguration config)
        {
            _config = config;
        }
        /// <summary>
        /// Método que obtiene el presupuesto por el id
        /// </summary>
        /// <param name="id">id del presupuesto</param>
        /// <returns>Datos del presupuesto</returns>
        public async Task<PresupuestoDao> GetPresupuestoById(Int64 id)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            if (db.State == ConnectionState.Closed) db.Open();
            PresupuestoDao presupuesto = await this.GetTPresupuestoById(db, id);

            return presupuesto;
        }
        /// <summary>
        /// Método que obtiene el presupuesto por el id del vehículo
        /// </summary>
        /// <param name="id">id del vehículo</param>
        /// <returns>Datos del presupuesto</returns>
        public async Task<PresupuestoDao> GetPresupuestoByIdVehiculo(Int64 id)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            if (db.State == ConnectionState.Closed) db.Open();
            PresupuestoDao presupuesto = await this.GetTPresupuestoByIdVehiculo(db, id);

            return presupuesto;
        }
        /// <summary>
        /// Método que obtiene el prespuesto por el id
        /// </summary>
        /// <param name="db">Objeto cone´xión</param>
        /// <param name="id">Id del presupuesto</param>
        /// <returns>Datos del presupuesto</returns>
        public async Task<PresupuestoDao> GetTPresupuestoById( IDbConnection db, Int64 id)
        {
            String storeProcedure = "sp_GetPresupuestoById";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("@Id", id, DbType.Int64);

            return await db.QueryFirstOrDefaultAsync<PresupuestoDao>(storeProcedure, dp, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        }
        /// <summary>
        /// Método que obtiene el prespuesto por el id del vehículo
        /// </summary>
        /// <param name="db">Objeto cone´xión</param>
        /// <param name="id">Id del vehículo</param>
        /// <returns>Datos del presupuesto</returns>
        public async Task<PresupuestoDao> GetTPresupuestoByIdVehiculo(IDbConnection db, Int64 id)
        {
            String storeProcedure = "sp_GetPresupuestoByIdVehiculo";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("@Id", id, DbType.Int64);

            return await db.QueryFirstOrDefaultAsync<PresupuestoDao>(storeProcedure, dp, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        }
        /// <summary>
        /// Inserta datos del prespuesto
        /// </summary>
        /// <param name="db">Objeto conexión</param>
        /// <param name="presupuesto">Datos necesarios para el insert del presupuesto</param>
        /// <param name="transaction">Objeto transacción</param>
        public async Task InsertPresupuesto(IDbConnection db,PresupuestoDto presupuesto, IDbTransaction transaction)
        {
            String storeProcedure = "sp_InsertarPresupuesto";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("@Nombre", presupuesto.Nombre, DbType.String);
            dp.Add("@Apellido", presupuesto.Apellido, DbType.String);
            dp.Add("@EMail", presupuesto.EMail, DbType.String);
            dp.Add("@Id_vehiculo", presupuesto.Id_vehiculo, DbType.String);
            dp.Add("@Total", presupuesto.Total, DbType.Decimal);

            await db.ExecuteAsync(storeProcedure, dp,transaction, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

        }

        public async Task UpdatePresupuestoTotal(IDbConnection db,Int64 idPresupuesto, IDbTransaction transaction)
        {
            String storeProcedure = "sp_UpdatePresupuesto";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("@Id", idPresupuesto, DbType.Int64);

            await db.ExecuteAsync(storeProcedure, dp, transaction, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        }
    }
}
