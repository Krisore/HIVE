﻿using HIVE.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIVE.Shared.Request
{
    public class UploadDocumentRequest
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Abstract { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public bool IsOpenAccess { get; set; } = true;
        public bool IsActive { get; set; } = true;
        public bool ToReview { get; set; } = true;
        public bool IsArchived { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public bool IsConfirmForPlagiarism { get; set; }
        public bool IsConfirmedForGrammarAndStatistic { get; set; }
        public bool IsAgree { get; set; }
        public DateTime DateUploaded { get; set; } = DateTime.Now;
        public DateTime? DatePublished { get; set; } = DateTime.Now;
        public int CurriculumId { get; set; }
        public int AdviserId { get; set; }
        public int ReferenceId { get; set; }
        public int FileId { get; set; }
        public string UploaderEmail { get; set; } = string.Empty;
        public List<CreateTopic> Topics { get; set; } = new List<CreateTopic>();
    }
}
