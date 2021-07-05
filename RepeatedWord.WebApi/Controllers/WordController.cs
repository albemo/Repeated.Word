using Microsoft.AspNetCore.Mvc;
using RepeatedWord.WebApi.ViewModels;
using System;
using System.Linq;

namespace RepeatedWord.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class WordController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetRepeatedWords(string text)
        {
            //Convert the string into an array of words  
            string[] items = text.Split(new char[] {' ', '.', ',', '?', '!', ';', ':'}, StringSplitOptions.RemoveEmptyEntries);

            var model = items
                .GroupBy(word => word) // Group by word 
                .Select(x => new WordViewModel // Projection to return
                {
                    Word = x.Key,
                    Count = x.Count()
                })
                .OrderByDescending(x => x.Count) // sort by count of repeated words descending
                .ToList();

            return Ok(model);
        }
    }
}
