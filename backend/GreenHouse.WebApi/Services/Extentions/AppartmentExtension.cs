﻿using GreenHouse.Domain.Entities;
using GreenHouse.HttpModels.DataTransferObjects;
using GreenHouse.HttpModels.Requests;
using GreenHouse.HttpModels.Responses;

namespace GreenHouse.WebApi.Services.Extentions
{
    public static class AppartmentExtension
    {
        public static AppartmentDto CreateDto(this Appartment appartment)
        {
            RulesListDto rules = null!;

            if (appartment.Rules is not null)
            {
                rules = new()
                {
                    IsChildrenAllowed = appartment.Rules.IsChildrenAllowed,
                    IsPetsAllowed = appartment.Rules.IsPetsAllowed,
                    IsSmokingAllowed = appartment.Rules.IsSmokingAllowed,
                    IsPartyAllowed = appartment.Rules.IsPartyAllowed
                };
            }

            var appartmentDto = new AppartmentDto()
            {
                Id = appartment.Id,
                Location = appartment.Location,
                NumberOfGuests = appartment.NumberOfGuests,
                NumberOfRooms = appartment.NumberOfRooms,
                NumberOfSlippingPlaces = appartment.NumberOfSlippingPlaces,
                Square = appartment.Square,
                Bail = appartment.Bail,
                Price = appartment.Price,
                Photos = appartment.Photos,
                Rules = rules,
            };

            return appartmentDto;
        }

        public static Appartment CreateAppartment(this AppartmentDto appartmentDto)
        {
            if(appartmentDto.Photos == null) throw new ArgumentNullException(nameof(appartmentDto.Photos));

            var rules = new RulesList();
            if (appartmentDto.Rules != null)
            {
                rules.IsChildrenAllowed = appartmentDto.Rules.IsChildrenAllowed;
                rules.IsPetsAllowed = appartmentDto.Rules.IsPetsAllowed;
                rules.IsSmokingAllowed = appartmentDto.Rules.IsSmokingAllowed;
                rules.IsPartyAllowed = appartmentDto.Rules.IsPetsAllowed;
            }

            var appartment = new Appartment()
            {
                Id = appartmentDto.Id,
                Location = appartmentDto.Location,
                NumberOfGuests = appartmentDto.NumberOfGuests,
                NumberOfRooms = appartmentDto.NumberOfRooms,
                NumberOfSlippingPlaces = appartmentDto.NumberOfSlippingPlaces,
                Square = appartmentDto.Square,
                Bail = appartmentDto.Bail,
                Price = appartmentDto.Price,                
                Photos = appartmentDto.Photos,
                Rules = rules
            };

            return appartment;
        }

        public static AppartmentResponse CreateResponce(this Appartment appartment)
        {
            List<OrderDto>? orders = null;
            if (appartment.Orders is not null)
            {
                orders = new();
                foreach (var order in appartment.Orders)
                {
                    if (order.DateTimeExit < DateTime.Now) continue;

                    var newOreder = new OrderDto()
                    {
                        DateTimeEntry = order.DateTimeEntry,
                        DateTimeExit = order.DateTimeExit
                    };
                    orders.Add(newOreder);
                }
            }

            RulesListDto rules = null!;
            if (appartment.Rules is not null)
            {
                rules = new()
                {
                    IsChildrenAllowed = appartment.Rules.IsChildrenAllowed,
                    IsPetsAllowed = appartment.Rules.IsPetsAllowed,
                    IsSmokingAllowed = appartment.Rules.IsSmokingAllowed,
                    IsPartyAllowed = appartment.Rules.IsPartyAllowed
                };
            }

            var appartmentResponse = new AppartmentResponse()
            {
                Id = appartment.Id,
                Location = appartment.Location,
                NumberOfGuests = appartment.NumberOfGuests,
                NumberOfRooms = appartment.NumberOfRooms,
                NumberOfSlippingPlaces = appartment.NumberOfSlippingPlaces,
                Square = appartment.Square,
                Bail = appartment.Bail,
                Price = appartment.Price,
                Photos = appartment.Photos,
                Rules = rules,
                Orders = orders,
                CityId = appartment.CityId,
            };

            return appartmentResponse;
        }

        public static AppartmentRequest CreateAppendRequest(this Appartment appartment)
        {
            RulesListDto rules = null!;

            if (appartment.Rules is not null)
            {
                rules = new()
                {
                    IsChildrenAllowed = appartment.Rules.IsChildrenAllowed,
                    IsPetsAllowed = appartment.Rules.IsPetsAllowed,
                    IsSmokingAllowed = appartment.Rules.IsSmokingAllowed,
                    IsPartyAllowed = appartment.Rules.IsPartyAllowed
                };
            }

            var appartmentRequest = new AppartmentRequest()
            {
                Id = appartment.Id,
                CityId = appartment.CityId,
                Location = appartment.Location,
                NumberOfGuests = appartment.NumberOfGuests,
                NumberOfRooms = appartment.NumberOfRooms,
                NumberOfSlippingPlaces = appartment.NumberOfSlippingPlaces,
                Square = appartment.Square,
                Bail = appartment.Bail,
                Price = appartment.Price,
                Photos = appartment.Photos,
                Rules = rules,
            };

            return appartmentRequest;
        }

        public static Appartment CreateAppartment(this AppartmentRequest appartmentRequest, bool changeId)
        {
            if (appartmentRequest.Photos == null) throw new ArgumentNullException(nameof(appartmentRequest.Photos));

            var rules = new RulesList();
            if (appartmentRequest.Rules != null)
            {
                rules.IsChildrenAllowed = appartmentRequest.Rules.IsChildrenAllowed;
                rules.IsPetsAllowed = appartmentRequest.Rules.IsPetsAllowed;
                rules.IsSmokingAllowed = appartmentRequest.Rules.IsSmokingAllowed;
                rules.IsPartyAllowed = appartmentRequest.Rules.IsPetsAllowed;
            }


            Appartment appartment;

            if (changeId)
            {
                appartment = new Appartment()
                {
                    Id = Guid.NewGuid(),
                    CityId = appartmentRequest.CityId,
                    Rules = rules
                };
            }
            else
            {
                appartment = new Appartment()
                {
                    Id = appartmentRequest.Id,
                    CityId = appartmentRequest.CityId,
                    Rules = rules
                };
            }

            appartment.Location = appartmentRequest.Location;
            appartment.NumberOfGuests = appartmentRequest.NumberOfGuests;
            appartment.NumberOfRooms = appartmentRequest.NumberOfRooms;
            appartment.NumberOfSlippingPlaces = appartmentRequest.NumberOfSlippingPlaces;
            appartment.Square = appartmentRequest.Square;
            appartment.Bail = appartmentRequest.Bail;
            appartment.Price = appartmentRequest.Price;
            appartment.Photos = appartmentRequest.Photos;

            return appartment;
        }
    }
}
