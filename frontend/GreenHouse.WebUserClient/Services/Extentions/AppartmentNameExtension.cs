using GreenHouse.HttpModels.Responses;

namespace GreenHouse.WebUserClient.Services.Extentions
{
    public static class AppartmentNameExtension
    {
        public static string GetAppartmentName(this AppartmentResponse appartment)
        {
            switch (appartment.NumberOfRooms)
            {
                case 0:
                    return "Квартира-студия";
                default:
                    return $"{appartment.NumberOfRooms}-к. квартира";
            }
        }
    }
}
