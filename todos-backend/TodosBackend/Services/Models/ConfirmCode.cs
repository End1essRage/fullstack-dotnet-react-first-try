namespace TodosBackend.Web.Services.Models
{
    public class ConfirmCode
    {
        public ConfirmCode(string prefix) 
        { 
            Prefix = prefix;
        }
        public string Prefix { get; set; }
        public string Key { private get; set; }
        public string Code { get; set; }

        public string GetKey()
        {
            return Prefix + Key;
        }
    }
}
