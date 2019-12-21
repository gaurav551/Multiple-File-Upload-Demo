using PhotoUpload.Models;
namespace PhotoUpload.Models
{
    public class StudentImage
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public Student Student {get;set;}
        public int StudentId { get; set; }
    }
}