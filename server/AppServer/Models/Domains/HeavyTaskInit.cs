namespace AppServer.Models.Domains
{
    public partial class HeavyTask
    {
        public HeavyTask() 
        { 
            this.startedAt = DateTime.Now;
        }
    }
}
