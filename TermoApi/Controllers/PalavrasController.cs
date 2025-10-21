using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PalavrasController : ControllerBase
{
    private static readonly List<string> todasPalavras;

    static PalavrasController()
    {
        todasPalavras = System.IO.File.ReadAllLines("Data/palavras.txt")
            .Select(p => p.Trim().ToLower())
            .Where(p => p.Length == 5)
            .Distinct()
            .ToList();
    }

    [HttpGet("todas")]
    public ActionResult<IEnumerable<string>> GetTodas()
    {
        return todasPalavras;
    }

    [HttpGet("random")]
    public ActionResult<string> GetRandom()
    {
        var rnd = new Random();
        return todasPalavras[rnd.Next(todasPalavras.Count)];
    }

    [HttpGet("valida/{palavra}")]
    public ActionResult<bool> Valida(string palavra)
    {
        return todasPalavras.Contains(palavra.ToLower());
    }
}