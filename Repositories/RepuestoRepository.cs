using System.Data.SqlClient;
using System.Data;
using TallerMecanicoDiWork.DtosDAO;
using TallerMecanicoDiWork.Dtos;
using Dapper;
using System.Collections.Generic;
using TallerMecanicoDiWork.Interfaces;

namespace TallerMecanicoDiWork.Repositories
{
    public class RepuestoRepository : IRepuestoRepository
    {
        private readonly IConfiguration _config;

        public RepuestoRepository(IConfiguration config)
        {
            _config = config;
        }
        /// <summary>
        /// Método que crea el objeto conexión y llama al método para obtener todos los repuestos
        /// </summary>
        /// <returns>Lista de repuestos</returns>
        public async Task<IEnumerable<RepuestoDao>> GetAllRepuestos()
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            if (db.State == ConnectionState.Closed) db.Open();
            IEnumerable<RepuestoDao> repuestos = await this.GetAllTRepuestos(db);

            return repuestos;
        }
        /// <summary>
        /// Método que crea el objeto conexión y llama el método para obtener los respuestos
        /// por id de desperfecto
        /// </summary>
        /// <param name="id">Id de desperfecto</param>
        /// <returns>Lista de respuestos según id de desperfecto</returns>
        public async Task<IEnumerable<RepuestoDao>> GetAllRepuestosByIdDesperfecto(Int64 id)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            if (db.State == ConnectionState.Closed) db.Open();
            IEnumerable<RepuestoDao> repuestos = await this.GetAllTRepuestosByIdDesperfecto(db, id);

            return repuestos;
        }
        /// <summary>
        /// Método que obtiene la lista de respuestos por id de desperfecto
        /// </summary>
        /// <param name="id">Id de desperfecto</param>
        /// <returns>Lista de respuestos según id de desperfecto</returns>
        public async Task<IEnumerable<RepuestoDao>> GetAllTRepuestos(IDbConnection db)
        {
            String storeProcedure = $"sp_GetRepuestos";

            IEnumerable<RepuestoDao> repuestos = await db.QueryAsync<RepuestoDao>(storeProcedure,CommandType.StoredProcedure).ConfigureAwait(false);

            return repuestos;
        }
        /// <summary>
        /// Método que obtiene la lista de respuestos por id de desperfecto
        /// </summary>
        /// <param name="id">Id de desperfecto</param>
        /// <returns>Lista de respuestos según id de desperfecto</returns>
        public async Task<IEnumerable<RepuestoDao>> GetAllTRepuestosByIdDesperfecto(IDbConnection db,Int64 id)
        {
            String storeProcedure = "sp_GetAllTRepuestosByIdDesperfecto";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("@Id", id, DbType.Int64);

            return await db.QueryAsync<RepuestoDao>(storeProcedure, dp, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        }
    }
}
