using HIVE.Server.Services.Interface;
using HIVE.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HIVE.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityLog _activity;

        public ActivityController(IActivityLog activity)
        {
            _activity = activity;
        }

        [HttpPost]
        [Route("write")]
        public async Task<ActionResult<Activity>> Write(Activity activity)
        {
            var response = await _activity.InsertLog(activity);
            return Ok(response);
        }

        [HttpGet]
        [Route("logs")]
        public async Task<ActionResult<List<Activity>>> GetLogs()
        {
            var response = await _activity.GetLogs();
            return Ok(response);
        }
        [HttpDelete]
        [Route("logs/delete/{id:int}")]
        public async Task<ActionResult<List<Activity>>> ClearLogs(int id)
        {
            await _activity.ClearLogs(id);
            return Ok();
        }
    }
}
