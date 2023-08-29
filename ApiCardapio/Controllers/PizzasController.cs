using ApiCardapio.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiCardapio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzasController : ControllerBase
    {
        private readonly List<PizzaModel> _pizzas = new List<PizzaModel>
        {
            new PizzaModel { Id = 1, Nome = "Margherita", Preco = 38.99 },
            new PizzaModel { Id = 2, Nome = "Brócolis c/ Queijo", Preco = 48.99 },
            new PizzaModel { Id = 3, Nome = "Frango c/ Catupiry", Preco = 42.99 },
            new PizzaModel { Id = 4, Nome = "Vegetariana", Preco = 45.99 },
            new PizzaModel { Id = 5, Nome = "4 Queijos", Preco = 45.99 },
        };

        private int _nextPizzaId = 6;

        [HttpGet]
        public IActionResult GetPizzas()
        {
            return Ok(_pizzas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var resposta = _pizzas.FirstOrDefault(i => i.Id == id);

            if (resposta == null)
            {
                return NotFound("Id não encontrado");
            }
            else
            {
                return Ok(resposta);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] PizzaModel newPizza)
        {
            newPizza.Id = _nextPizzaId++;
            _pizzas.Add(newPizza);
            return StatusCode(StatusCodes.Status201Created, newPizza.Id);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] PizzaModel pizzaAtualizada)
        {
            var pizzaAtual = _pizzas.FirstOrDefault(p => p.Id == id);

            if (pizzaAtual == null)
            {
                return NotFound("Pizza não encontrada");
            }

            pizzaAtual.Nome = pizzaAtualizada.Nome;
            pizzaAtual.Preco = pizzaAtualizada.Preco;

            return Ok(pizzaAtual);
        }
        
        [HttpPatch("{id}")]
        public IActionResult Update2([FromRoute] int id, [FromBody] PizzaModel pizzaAtualizada)
        {
            var pizzaAtual = _pizzas.FirstOrDefault(p => p.Id == id);

            if (pizzaAtual == null)
            {
                return NotFound("Pizza não encontrada");
            }

            pizzaAtual.Nome = pizzaAtualizada.Nome;
            pizzaAtual.Preco = pizzaAtualizada.Preco;

            return Ok(pizzaAtual);
        }

        [HttpDelete()]
        public IActionResult Delete([FromQuery] int id)
        {
            if (id == 0)
            {
                return BadRequest("Este Id não pode ser usado");
            }
            else
            {
                _pizzas.RemoveAt(id);
                return Ok($"O item {id} foi exluido.");
            }
        }
    }
}