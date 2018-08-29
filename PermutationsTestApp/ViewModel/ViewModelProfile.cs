using AutoMapper;
using PermutationsTestApp.Model;

namespace PermutationsTestApp.ViewModel
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            CreateMap<Element, ElementView>();
        }
    }
}
