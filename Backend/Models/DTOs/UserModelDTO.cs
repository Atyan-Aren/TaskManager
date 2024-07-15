using AutoMapper;
using TaskManager.Interfaces;
using TaskManager.Models.DBModels;

namespace TaskManager.Models.DTOs
{
    public class UserModelDTO : IMapWith<UserModel>
    {
        #region Properties

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string TelegramNickname { get; set; }

        #endregion

        #region Methods: Public

        public UserModel Map()
        {
            var mapperConfig = new MapperConfiguration(config => config.CreateMap<UserModelDTO, UserModel>()
                    .ForMember("Name", opt => opt.MapFrom(src => src.Username))
                    .ForMember("Email", opt => opt.MapFrom(src => src.Email))
                    .ForMember("TelegramNickname", opt => opt.MapFrom(src => src.TelegramNickname))
                    .ForMember("Password", opt => opt.MapFrom(src => src.Password)));
            var mapper = new Mapper(mapperConfig);
            UserModel user = mapper.Map<UserModelDTO, UserModel>(this);
            return user;
        }

        #endregion
    }
}