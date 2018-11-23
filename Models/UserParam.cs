using System;
namespace TodoApi.Models
{
    public class UserParam
    {
        public string Id { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public int UserId { get; set; } = 0;
    }
}
