namespace PersonalWebsiteWebAPI.Models
{
    public class ImgSrcModel
    {
        public long Id { get; set; }

        private string _src;
        public string Src { get { return _src; } set { _src = value; } }
    }
}
