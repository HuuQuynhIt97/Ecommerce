using System;

namespace ecommerce_api.DTO
{
    public class GeneralSettingDto
    {
        public GeneralSettingDto()
        {
            Created_at = DateTime.Now;
        }
        
        public int ID { get; set; }
        public string Frontend_color { get; set; }
        public string Logo { get; set; }
        public string Footer_logo { get; set; }
        public string Admin_logo { get; set; }
        public string Admin_login_background { get; set; }
        public string Admin_login_sidebar { get; set; }
        public string Favicon { get; set; }
        public string Site_name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string Youtube { get; set; }
        public string Google_plus { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}