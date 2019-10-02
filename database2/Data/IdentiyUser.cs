namespace database2.Data
{
    internal class IdentiyUser
    {
        public object Email { get; set; }
        public string SecurityStamp { get; internal set; }
        public string UserName { get; internal set; }
    }
}