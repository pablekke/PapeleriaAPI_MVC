using Domain;

namespace MVC.Models
{
    public class ConfiguarViewModel
    {
        public int PageSize { get; set; }
        public int Tope { get; set; }
        public ConfiguarViewModel()
        {
            PageSize = Paginado.PageSize;
        }
    }
}