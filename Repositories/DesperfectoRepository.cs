using System.Data.SqlClient;
using System.Data;
using TallerMecanicoDiWork.DtosDAO;
using Dapper;
using TallerMecanicoDiWork.Interfaces;

namespace TallerMecanicoDiWork.Repositories
{
    public class DesperfectoRepository : IDesperfectoRepository
    {
        private readonly IConfiguration _config;
        private readonly IPresupuestoRepository _presupuestoRepository;

        public DesperfectoRepository(IConfiguration config, IPresupuestoRepository presupuestoRepository)
        {
            _config = config;
            _presupuestoRepository = presupuestoRepository;
        }
        /// <summary>
        /// Metodo que crea conexión y llama al método para obtener los desperfectos
        /// </summary>
        /// <returns>Lista de desperfectos</returns>
        public async Task<IEnumerable<DesperfectoDao>> GetAllDesperfectos()
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            if (db.State == ConnectionState.Closed) db.Open();
            IEnumerable<DesperfectoDao> desperfectos = await this.GetAllTDesperfectos(db);

            return desperfectos;
        }
        /// <summary>
        /// Método que crea la conexión y llama al método que trae los desperfecto por el id de presupuesto
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Lista de desperfectos para determinado presupuesto</returns>
        public async Task<IEnumerable<DesperfectoDao>> GetAllDesperfectosByIdPresupuesto(Int64 id)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            if (db.State == ConnectionState.Closed) db.Open();
            IEnumerable<DesperfectoDao> desperfectos = await this.GetAllTDesperfectosByIdPresupuesto(db, id);

            return desperfectos;
        }
        /// <summary>
        /// Método que obtiene la lista de desperfectos de la base
        /// </summary>
        /// <param name="db">objeto conexión</param>
        /// <returns>Lista de desperfectos</returns>
        public async Task<IEnumerable<DesperfectoDao>> GetAllTDesperfectos(IDbConnection db)
        {
            String storeProcedure = $"sp_GetAllDesperfectos";

            IEnumerable<DesperfectoDao> desperfectos = await db.QueryAsync<DesperfectoDao>(storeProcedure, CommandType.StoredProcedure).ConfigureAwait(false);

            return desperfectos;
        }
        /// <summary>
        /// Método que crea la conexión y transacción para insertar en la tabla desperfecto y 
        /// la tabla realcional desperfectoRepuesto
        /// </summary>
        /// <param name="desperfecto">Datos del desperfecto necesarios para el insert de las tablas</param>
        public async Task InsertDesperfecto(DesperfectoDto desperfecto)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            if (db.State == ConnectionState.Closed) db.Open();
            using IDbTransaction transaction = db.BeginTransaction();

            try
            {
                Int64 idDesperfecto = await this.InsertTDesperfecto(db, desperfecto, transaction);

                foreach(var repuesto in desperfecto.Repuestos)
                {
                    await this.InsertTDesperfectoRepuesto(db,idDesperfecto,repuesto, transaction);
                }

                await _presupuestoRepository.UpdatePresupuestoTotal(db,desperfecto.IdPresupuesto, transaction);

                transaction.Commit();   
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        /// <summary>
        /// Método que realiza el insert de la tabla desperfecto
        /// </summary>
        /// <param name="db">Objeto conexión</param>
        /// <param name="desperfecto">Datos del desperfecto</param>
        /// <param name="transaction">Objeto transacción</param>
        /// <returns>Id generado del insert</returns>
        public async Task<Int64> InsertTDesperfecto(IDbConnection db,DesperfectoDto desperfecto, IDbTransaction transaction)
        {
            String storeProcedure = $"sp_InsertarDesperfecto";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("@IdPresupuesto", desperfecto.IdPresupuesto, DbType.Int64);
            dp.Add("@Descripcion", desperfecto.Descripcion , DbType.String);
            dp.Add("@Mano_de_obra", desperfecto.Mano_De_Obra, DbType.Decimal);
            dp.Add("@Tiempo", desperfecto.Tiempo, DbType.Int16);

            dp.Add(name: "Id", dbType: DbType.Int64, direction: ParameterDirection.Output);

            await db.ExecuteAsync(storeProcedure, dp, transaction, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

            return dp.Get<Int64>("Id");

        }
        /// <summary>
        /// Método que inserta en la tabla relacion desperfecto repuesto
        /// </summary>
        /// <param name="db">Objeto conexión</param>
        /// <param name="desperfecto">Id del desperfecto</param>
        /// <param name="repuesto">Id del repuesto</param>
        /// <param name="transaction">Objeto trasacción</param>
        public async Task InsertTDesperfectoRepuesto(IDbConnection db, Int64 desperfecto, Int64 repuesto, IDbTransaction transaction)
        {
            String storeProcedure = $"sp_InsertarDesperfectoRepuesto";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("@IdDesperfecto", desperfecto, DbType.Int64);
            dp.Add("@IdRpuesto", repuesto, DbType.String);

            await db.ExecuteAsync(storeProcedure, dp, transaction, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

        }
        /// <summary>
        /// Método que trae los Desperfecto por el id del presupuesto
        /// </summary>
        /// <param name="db">Objeto conexión</param>
        /// <param name="id">Id del presupuesto</param>
        /// <returns></returns>
        public async Task<IEnumerable<DesperfectoDao>> GetAllTDesperfectosByIdPresupuesto(IDbConnection db,Int64 id)
        {
            String storeProcedure = $"sp_GetAllDesperfectosByIdPresupuesto";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("@Id", id, DbType.Int64);

            return await db.QueryAsync<DesperfectoDao>(storeProcedure, dp, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        }
    }
}
