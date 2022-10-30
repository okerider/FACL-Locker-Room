using FACL_Locker_Room_API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FACL_Locker_Room_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LockersController : ControllerBase
    {
        // Created a model called: Locker in Models folder for user data
        public static Locker locker = new Locker();
        private IWebHostEnvironment webHostEnvironment;
        private readonly IConfiguration _configuration;

        public LockersController(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _configuration = configuration;
            webHostEnvironment = environment;
        }

        [HttpGet("GetCurrentVersion")]
        public string GetCurrentVersion()
        {
            // retrieve the app version from appsetings.json
            //string value = _configuration.GetSection("config").GetSection("AppVersion").Value;
            string value = _configuration.GetValue<string>("config:AppVersion");

            return value;
        }

        
        // POST api/<LockersController>
        [HttpPost("CreateAccount")]
        public IActionResult CreateAccount([FromForm] Locker request)
        {
            locker.FirstName = request.FirstName;
            locker.LastName = request.LastName;
            locker.DateOfBirth = request.DateOfBirth;
            locker.Gender = request.Gender;
            locker.Nationality = request.Nationality;


            // Generate the json string.
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(locker, Newtonsoft.Json.Formatting.Indented);

            // create path accounts/[first-name]-[last-name].json
            string path = Path.Combine(webHostEnvironment.ContentRootPath, "accounts/" + locker.FirstName + locker.LastName + ".json");

            //check if file alredy created or exists

            if (System.IO.File.Exists(path))
            {
                //yes - return Status:400, message: Account already exist
                return BadRequest("Account already exist");
            }
            else
            {
                //no -  return a 200 HttpResponse with message: Account created
                // Write the json string to file.
                System.IO.File.WriteAllTextAsync(path, json);
                return Ok("Account created");
            }

           
        }

        //GetAccount

        [HttpGet("GetAccount")]
        public IActionResult GetAccount(string FirstName, string LastName)
        {
            if(FirstName == null || LastName == null)
            {
                return NotFound();
            }
            // create path to the account
            string path = Path.Combine(webHostEnvironment.ContentRootPath, "accounts/" + FirstName + LastName + ".json");

            
            if (System.IO.File.Exists(path))
            {
                // Read the json file.
               // return Ok(System.IO.File.ReadAllTextAsync(path));

                string text = System.IO.File.ReadAllText(path);

                var person = JsonSerializer.Deserialize<Locker>(text);

                return Ok(person);

            }



            return BadRequest();

        }


        
    }
}
