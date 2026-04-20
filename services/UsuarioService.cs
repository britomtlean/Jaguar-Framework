using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JaguarFramework.Context;
using JaguarFramework.Models;
using MongoDB.Driver;

namespace JaguarFramework.Services
{
    public class UsuarioService : MongoContextDb
    {

        public UsuarioService(){}

        public IMongoCollection<Usuario> usuarios =>
            _database.GetCollection<Usuario>("Usuarios");


        public async Task<List<Usuario>> GetUsers()
        {
            var usuarios = await this.usuarios.Find(_ => true).ToListAsync();

            foreach (var u in usuarios)
            {
                Console.WriteLine($"Nome: {u.Nome}");
            }

            return usuarios;
        }

        public async Task<bool> CreateUser(Usuario newUser)
        {
            Console.WriteLine($"Usuário recebido", newUser);

            await this.usuarios.InsertOneAsync(newUser);
            return true;
        }

    }
}
