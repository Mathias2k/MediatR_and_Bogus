using AspNet_MediatR1.Controllers;
using AspNet_MediatR1.Domain.Entity;
using Bogus;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_MediatR1.Repository
{
    public class ProdutoRepository : IRepository<Produto>
    {
        private Dictionary<int, Produto> produtos = new Dictionary<int, Produto>();
        private List<Produto> produtosBogus = new List<Produto>();

        public static List<Produto> ListaClientesFake()
        {
            var produtosFaker = new Faker<Produto>("pt_BR")
                .RuleFor(c => c.Id, f => f.IndexFaker)
                .RuleFor(c => c.Nome, f => f.Name.FullName(Bogus.DataSets.Name.Gender.Female))
                .RuleFor(c=> c.Preco, f=> f.Random.Decimal(1,200));
                //.RuleFor(c => c.Email, f => f.Internet.Email(f.Person.FirstName).ToLower())
                //.RuleFor(c => c.Telefone, f => f.Person.Phone)
                //.RuleFor(c => c.Endereco, f => f.Address.StreetAddress())
                //.RuleFor(c => c.Nascimento, f => f.Date.Recent(100))
                //.RuleFor(c => c.Sexo, f => f.PickRandom(new string[] { "masculino", "feminino" }))
                //.RuleFor(c => c.Ativo, f => f.PickRandomParam(new bool[] { true, true, false }))
                //.RuleFor(o => o.Renda, f => f.Random.Decimal(500, 2000));
            var produtos = produtosFaker.Generate(100);
            return produtos;
        }
        public ProdutoRepository()
        {
            produtosBogus = ListaClientesFake();
            //produtos = GetProdutos();
        }

        public async Task<List<Produto>> GetAllFakeDataBogus()
        {
            return await Task.Run(() => produtosBogus);
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await Task.Run(() => produtos.Values.ToList());
        }

        public async Task<Produto> Get(int id)
        {
            return await Task.Run(() => produtos.GetValueOrDefault(id));
        }

        public async Task Add(Produto produto)
        {
             await Task.Run(() => produtos.Add(produto.Id, produto));
        }

        public async Task Edit(Produto produto)
        {
            await Task.Run(() =>
            {
                produtos.Remove(produto.Id);
                produtos.Add(produto.Id, produto);
            });
        }

        public async Task Delete(int id)
        {
            await Task.Run(() => produtos.Remove(id));
        }
    }
}
