using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingProject.Model
{
    public class Chat
    {
        [Key]
        public int chatID { get; set; }
        public string chatCode { get; set; }
        public string message { get; set; }
        public DateTime? createDate { get; set; }
    }
}
