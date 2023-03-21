using System.Numerics;

namespace SuperHeroAPI.Models
{
    public class ServerResponse
    {
        public bool isError { get; set; }
        public int ResponseCode { get; set; }
        public string Message { get; set; }
    }
}