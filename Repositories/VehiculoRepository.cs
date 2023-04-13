using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using TallerMecanicoDiWork.Dtos;
using TallerMecanicoDiWork.DtosDAO;
using TallerMecanicoDiWork.Enums;
using TallerMecanicoDiWork.Interfaces;
using TallerMecanicoDiWork.Models;

namespace TallerMecanicoDiWork.Repositories
{
    public class VehiculoRepository : IVehiculoRepository
    {
        private IConfiguration _config;
        private readonly IPresupuestoRepository _presupuestoRepository;

        public VehiculoRepository(IConfiguration config, IPresupuestoRepository presupuestoRepository)
        {
            _config = config;
            _presupuestoRepository = presupuestoRepository;
        }
        /// <summary>
        /// Método que crea la conexión y llama al método para obtener los vehículos de la base
        /// </summary>
        /// <returns>Lista de vehículos</returns>
        public async Task<IEnumerable<VehiculoDao>> GetAllVehiculos()
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            if (db.State == ConnectionState.Closed) db.Open();
            IEnumerable<VehiculoDao> vehiculos = await this.GetAllTVehiculos(db);

            return vehiculos;

        }
        /// <summary>
        /// Método que crea la conexión y llama al método para obtener los automóviles de la base
        /// </summary>
        /// <returns>Lista de automóviles</returns>
        public async Task<IEnumerable<AutomovilDao>> GetAllAutomoviles()
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            if (db.State == ConnectionState.Closed) db.Open();

            IEnumerable<AutomovilDao> automoviles = await this.GetAllTAutomoviles(db);

            return automoviles;

        }
        /// <summary>
        /// Método que crea la conexión y llama al método para obtener los motos de la base
        /// </summary>
        /// <returns>Lista de motos</returns>
        public async Task<IEnumerable<MotoDao>> GetAllMotos()
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            if (db.State == ConnectionState.Closed) db.Open();

            IEnumerable<MotoDao> motos = await this.GetAllTMotos(db);

            return motos;
        }
        /// <summary>
        /// Método que crea el objeto conexión y llama a los método para insertar
        /// en la tabla vehículo y en la tabla presupuesto
        /// </summary>
        /// <param name="vehiculo">Datos necesario del vehículo y del cliente para el presupuesto</param>
        public async Task InsertVehiculo(VehiculoDto vehiculo)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            if (db.State == ConnectionState.Closed) db.Open();
            using IDbTransaction transaction = db.BeginTransaction();

            try
            {

                Int64 id_vehiculo = await this.InsertTVehiculos(db, vehiculo, transaction);

                vehiculo.Presupuesto.Id_vehiculo = id_vehiculo;

                await _presupuestoRepository.InsertPresupuesto(db, vehiculo.Presupuesto, transaction);
                
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }


        }
        /// <summary>
        /// Método que crea la conexión y llama al método para obtener el vehículo por el id
        /// </summary>
        /// <param name="id">Id del vehículo</param>
        /// <returns>Vehículo</returns>
        public async Task<VehiculoDao> GetVehiculoById(Int64 id)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            if (db.State == ConnectionState.Closed) db.Open();

            return await this.GetTVehiculoById(db, id);
            

        }
        /// <summary>
        /// Método que obtine el vehículo por el id del mismo
        /// </summary>
        /// <param name="db">Objeto conexión</param>
        /// <param name="id">Ide del vehículo</param>
        /// <returns>Vehiculo</returns>
        public async Task<VehiculoDao> GetTVehiculoById(IDbConnection db, Int64 id)
        {
            String storeProcedure = "sp_GetVehiculoById";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("@Id", id, DbType.Int64);
            
            return await db.QueryFirstOrDefaultAsync<VehiculoDao>(storeProcedure,dp, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        }
        /// <summary>
        /// Metodo que obtiene la lista de vehículos de la base
        /// </summary>
        /// <param name="db">Objeto conexión</param>
        /// <returns>Lista de vehículos</returns>
        public async Task<IEnumerable<VehiculoDao>>GetAllTVehiculos(IDbConnection db)
        {
            String storeProcedure = $"sp_GetVehiculos";

            return await db.QueryAsync<VehiculoDao>(storeProcedure,CommandType.StoredProcedure);
        }
        /// <summary>
        /// Método que obtiene todos los automóviles
        /// </summary>
        /// <param name="db">Objeto conexión</param>
        /// <returns>Lista de automóviles</returns>
        public async Task<IEnumerable<AutomovilDao>> GetAllTAutomoviles(IDbConnection db)
        {
            String storeProcedure = $"sp_GetAutomoviles";

            IEnumerable<AutomovilDao> autos = await db.QueryAsync<AutomovilDao>(storeProcedure, CommandType.StoredProcedure);

            return autos;
        }
        /// <summary>
        /// Método que obtiene todas las motos
        /// </summary>
        /// <param name="db">Objeto conexión</param>
        /// <returns>LIsta de motos</returns>
        public async Task<IEnumerable<MotoDao>>GetAllTMotos(IDbConnection db)
        {
            String storeProcedure = $"sp_GetMotos";

            IEnumerable<MotoDao> motos = await db.QueryAsync<MotoDao>(storeProcedure, CommandType.StoredProcedure);

            return motos;
        }
        /// <summary>
        /// Método que inserta un vehículo en la tabla vehículos
        /// </summary>
        /// <param name="db">Objeto conexión</param>
        /// <param name="vehiculo">Datos necesario para el insert del vehículo</param>
        /// <param name="transaction">Objeto transacción</param>
        /// <returns></returns>
        public async Task<Int64> InsertTVehiculos(IDbConnection db, VehiculoDto vehiculo, IDbTransaction transaction)
        {
            String storeProcedure = $"sp_InsertarVehiculo";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("@Marca", vehiculo.Marca,DbType.String);
            dp.Add("@Modelo", vehiculo.Modelo, DbType.String);
            dp.Add("@Patente", vehiculo.Patente, DbType.String);
            dp.Add("@Tipo_Vehiculo", vehiculo.Tipo_Vehiculo, DbType.Int16);
            dp.Add("@Tipo_Automovil", vehiculo.Tipo_Automovil, DbType.Int16);
            dp.Add("@Cantidad_De_Puertas", vehiculo.Cantidad_De_Puertas, DbType.Int16);
            dp.Add("@Cilindrada", vehiculo.Cilindrada, DbType.Int16);

            dp.Add(name: "Id", dbType: DbType.Int64, direction: ParameterDirection.Output);

            await db.ExecuteAsync(storeProcedure,dp,transaction,commandType :CommandType.StoredProcedure).ConfigureAwait(false);


            return dp.Get<Int64>("Id");

        }

    }
}
