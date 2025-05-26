using Services.GameControlService;

namespace Grid
{
    public class GridModel
    {
        protected GameControlService GameControlService => GameControlService.Instance;
    }
}