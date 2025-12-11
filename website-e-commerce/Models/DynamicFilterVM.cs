namespace website_e_commerce.Models
{
    public class DynamicFilterVM
    {
        public string Key { get; set; }          // ram, rom, cpu, screen, gpu, ...
        public string Label { get; set; }        // RAM, Bộ nhớ trong, Vi xử lý, ...
        public List<string> Values { get; set; } = new();
    }
}
