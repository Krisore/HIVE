using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HIVE.Shared.Model
{
    public class Document
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Abstract { get; set; } = string.Empty;
        public bool IsOpenAccess { get; set; } = true;
        public bool IsActive { get; set; } = true;
        public bool ToReview { get; set; } = true;
        public bool UnApproved { get; set; } = false;
        public bool IsArchived { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public DateTime DateUploaded { get; set; } = DateTime.Now;
        public DateTime? DatePublished { get; set; }
        public int CurriculumId { get; set; }
        public int AdviserId { get; set; }
        public int ReferenceId { get; set; }
        public int FileId { get; set; }
        public int UserId { get; set; }
    
        public FileEntry File { get; set; } = new FileEntry();
      
        public string UploaderEmail { get; set; } = string.Empty;
  
        public Curriculum Curriculum { get; set; } = new();
  
        public Adviser Adviser { get; set; } = new();
     
        public Reference Reference { get; set; } = new();
 
        public List<Topic> Topics { get; set; } = new List<Topic>();

        public List<Author> Authors { get; set; } = new List<Author>();
        public Document()
        {
            UpdateIsActive();
        }
        private void UpdateIsActive()
        {
            var tenYearsAgo = DateTime.Now.AddYears(-10);
            if (DatePublished.HasValue && DatePublished < tenYearsAgo)
            {
                IsActive = false;
            }
        }
    }
}
