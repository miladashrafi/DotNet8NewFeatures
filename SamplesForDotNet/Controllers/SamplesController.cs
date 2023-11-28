using DotNet8NewFeatures.Context;
using DotNet8NewFeatures.Models;
using DotNet8NewFeatures.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Frozen;
using System.Text.Json;
using CustomButton = (int Id, DotNet8NewFeatures.Models.Button Button);

namespace DotNet8NewFeatures.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SamplesController(IServiceProvider serviceProvider) : ControllerBase
    {

        [HttpGet("JsonStringEnumConverter")]
        public IActionResult JsonStringEnumConverter(Button button)
        {
            return Ok(button);
        }

        [HttpGet("GetItems_ShuffleList")]
        public IActionResult GetItems_ShuffleList()
        {
            return Ok(Random.Shared.GetItems(Constants.AllButtons, 15));
        }

        [HttpGet("GetItems_Shuffler")]
        public IActionResult GetItems_Shuffler()
        {
            var newList = Constants.AllButtons.Clone() as Button[];
            Random.Shared.Shuffle(newList!);
            return Ok(newList);
        }

        [HttpGet("FrozenDictionary")]
        public IActionResult FrozenDictionary()
        {
            var dic = new Dictionary<int, Button>
            {
                { 1, Button.Blue },
                { 2, Button.Red }
            };

            var frozenDictionary = dic.ToFrozenDictionary();

            return Ok(frozenDictionary);
        }

        [HttpPost("RangeExclusive")]
        public IActionResult RangeExclusive([FromBody] RangeExclusiveModel model)
        {
            return Ok(ModelState.IsValid);
        }

        [HttpPost("Length")]
        public IActionResult Length([FromBody] LengthModel model)
        {
            return Ok(ModelState.IsValid);
        }

        [HttpPost("Base64String")]
        public IActionResult Length([FromBody] Base64Model model)
        {
            return Ok(ModelState.IsValid);
        }

        [HttpPost("AllowedValuesDeniedValues")]
        public IActionResult AllowedValuesDeniedValues([FromBody] AllowedValuesDeniedValuesModel model)
        {
            return Ok(ModelState.IsValid);
        }

        [HttpGet("KeyedService")]
        public IActionResult KeyedService(string serviceKey)
        {
            var service = serviceProvider.GetKeyedService<IKeyedService>(serviceKey);
            return Ok(service!.GetMessage());
        }

        [HttpGet("AuthorizeSample")]
        [Authorize]
        public IActionResult AuthorizeSample()
        {
            return Content("You are authorized!");
        }

        [HttpGet("hierarchy-id-get-level")]
        public IActionResult HierarchyIdGetLevel()
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            var items = dbContext.Departments.Select(x=> new
            {
                Level = x.Path.GetLevel(),
                x.Name,
                Path = x.Path.ToString(),
                x.ParentId
            });
            return Ok(items.ToList());
        }

        [HttpGet("DefaultValue")]
        public IActionResult DefaultValue(int value)
        {
            var writeResponse = (int a, int b = 2) => $"Value A: {a} | Value B: {b}";

            List<string> results = [
                writeResponse(999999),
                writeResponse(999999, value)
            ];

            // For Alias Types (Usings)
            CustomButton testButton = (1, Button.Red);

            // For Inline Arrays
            var array = new InlineArrayModel();
            for (int i = 0; i < 2; i++)
            {
                array[i] = (int)Random.Shared.GetItems(Constants.AllButtons, 1)[0];
            }

            results.Add(JsonSerializer.Serialize(array));

            return Content(JsonSerializer.Serialize(results));
        }
    }
}
