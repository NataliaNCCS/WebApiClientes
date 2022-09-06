using Dapper;
using Microsoft.Data.SqlClient;

namespace WebApiClientes.Repository
{
    public class ClienteRepository
    {
        private readonly IConfiguration _configuration;

        public ClienteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Cadastro> ConsultarTodosOsCadastrosDoDB()
        {
            var query = "SELECT * FROM cliente";

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var conn = new SqlConnection(connectionString);
            // esse using com um campo mata a variável dps de usa-la

            return conn.Query<Cadastro>(query).ToList();

        }

        public bool InserirCadastrosNoDB(Cadastro cadastro)
        {
            var query = "INSERT INTO cliente VALUES(@cpf, @nome, @dataNascimento, @idade)";

            var parameters = new DynamicParameters();
            parameters.Add("cpf", cadastro.CPF);
            parameters.Add("nome", cadastro.Nome);
            parameters.Add("dataNascimento", cadastro.DataNascimento);
            parameters.Add("idade", cadastro.Idade);

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var conn = new SqlConnection(connectionString);

            //conn.Execute(query, parameters) RETORNA O Nº DE LINHAS AFETADAS,
            //SE MAIS DE UMA LINHA FOR AFETADA, DEU CERTO
            return (conn.Execute(query, parameters) > 0);

        }

        public bool DeletarCadastroNoDB(string cpf)
        {
            var query = "DELETE FROM cliente WHERE cpf = @cpf";

            var parameters = new DynamicParameters();
            parameters.Add("cpf", cpf);

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var conn = new SqlConnection(connectionString);

            return conn.Execute(query, parameters) > 0;
        }

        public bool AtualizarCadastroNoDB(string cpf, Cadastro cadastro)
        {
            var query = @"UPDATE cliente
SET cpf = @cpf, nome =  @nome, dataNascimento = @dataNascimento, idade = @idade
WHERE cpf = @cpf";

            var parameters = new DynamicParameters();
            parameters.Add("cpf", cadastro.CPF);
            parameters.Add("nome", cadastro.Nome);
            parameters.Add("dataNascimento", cadastro.DataNascimento);
            parameters.Add("idade", cadastro.Idade);

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var conn = new SqlConnection(connectionString);

            return conn.Execute(query, parameters) == 1;

        }

        public bool ConsultarPorCpfNoDB(string cpf)
        {
            var query = @"SELECT * FROM cliente WHERE cpf = @cpf";

            var parameters = new DynamicParameters();
            parameters.Add("cpf", cpf);

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var conn = new SqlConnection(connectionString);

            return conn.Query<Cadastro>(query, parameters).ToList().Count() == 1;
        }
    }
}
