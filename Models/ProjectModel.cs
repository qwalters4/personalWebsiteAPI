using System.ComponentModel.DataAnnotations;

namespace PersonalWebsiteWebAPI.Models
{
    public class ProjectModel
    {
        public long Id { get; set; }
        private string _title;
        private string _description;

        [Required]
        private List<ImgSrcModel>? _img_srcs;

        private string _content;
        private string _category;

        public string Title { get { return _title; } set { _title = value; } }
        public string Description { get { return _description; } set { _description = value; } }

        [Required] 
        public List<ImgSrcModel>? ImgSrcs { get { return _img_srcs; } set { _img_srcs = value; } }
        public string Content { get { return _content; } set { _content = value; } }
        public string Category { get { return _category; } set { _category = value; } }

        public ProjectModel()
        { }

    }
}
