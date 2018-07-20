using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using EfficiencyClassWebAPI.Repository;
using EfficiencyClassWebAPI.EF;
using Newtonsoft.Json;

namespace EfficiencyClassWebAPI.Models
{
    public class UserManagementModel
    {
        [JsonProperty(PropertyName = "Id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "CDSID")]
        public string Cdsid { get; set; }
        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "marketNames")]
        public string MarketNames { get; set; }
        [JsonProperty(PropertyName = "marketIds")]
        public List<int> MarketIds { get; set; }
        [JsonProperty(PropertyName = "RoleId")]
        public int RoleId { get; set; }
        [JsonProperty(PropertyName = "CreatedBy")]
        public string CreatedBy { get; set; }
        [JsonProperty(PropertyName = "CreatedOn")]
        public DateTime? CreatedOn { get; set; }
        [JsonProperty(PropertyName = "UpdatedBy")]
        public string UpdatedBy { get; set; }
        [JsonProperty(PropertyName = "UpdatedOn")]
        public DateTime? UpdatedOn { get; set; }

        private UnitofWork uow;
        public UserManagementModel()
        {
            uow = new UnitofWork();
        }

        public UserManagementModel(UnitofWork unitofWork)
        {
            uow = unitofWork;
        }

        public List<UserManagementModel> GetUserDetails()
        {
            try
            {
                List<UserManagementModel> userDetailsList = new List<UserManagementModel>();
                //using (var user = new UnitofWork())
                //{
                int adminRoleId = uow.RoleRepository.Find(x => x.RoleName == "User").Select(y => y.Id).SingleOrDefault();
                var userDetails = from u1 in uow.UserDetailRepository.GetAll()
                                  join u2 in uow.UserRoleRepository.Find(x => x.RoleId == adminRoleId) on u1.Id equals u2.UserId
                                  select new { u1.Id, u1.CDSID, u1.UserName, u1.Email, u1.UserMarkets, u1.CreatedBy, u1.CreatedOn, u1.UpdatedBy, u1.UpdatedOn };
                foreach (var item in userDetails)
                {
                    var marketList = item.UserMarkets.Select(x => x.Market.MarketName).ToArray();
                    List<int> marketIds = item.UserMarkets.Select(x => x.MarketId).ToList();

                    string marketString = "";
                    marketString = string.Join(",", marketList);
                    userDetailsList.Add(new UserManagementModel()
                    {
                        Id = item.Id,
                        Cdsid = item.CDSID,
                        UserName = item.UserName,
                        Email = item.Email,
                        MarketIds = marketIds,
                        MarketNames = marketString,
                        CreatedBy = item.CreatedBy,
                        CreatedOn = item.CreatedOn,
                        UpdatedBy = item.UpdatedBy,
                        UpdatedOn = item.UpdatedOn
                    });
                }
                //}
                uow.Dispose();
                return userDetailsList;
            }
            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }

        }

        public UserManagementModel AddUserDetails(UserManagementModel userDetails)
        {
            try
            {
                List<UserRole> userRole = new List<UserRole>();
                List<UserMarket> userMarketDetails = new List<UserMarket>();
                UserDetail userData = new UserDetail();

                //using (var user = new UnitofWork())
                //{
                userDetails.RoleId = uow.RoleRepository.Find(x => x.RoleName == "User").Select(y => y.Id).SingleOrDefault();
                var userExists = uow.UserDetailRepository.Find(x => x.CDSID == userDetails.Cdsid || x.UserName == userDetails.UserName || x.Email == userDetails.Email).SingleOrDefault();
                if (userExists != null)
                {
                    if (userExists.UserRoles.Select(x => x.RoleId).SingleOrDefault() == userDetails.RoleId)
                    {
                        throw new Exception(Resource.GetResxValueByName("UserDuplicatemsg"));
                    }
                    else
                    {
                        throw new Exception(Resource.GetResxValueByName("SuperUserDuplicatemsg"));
                    }

                }


                userData.Id = userDetails.Id;
                userData.CDSID = userDetails.Cdsid;
                userData.UserName = userDetails.UserName;
                userData.Email = userDetails.Email;
                userData.CreatedBy = userDetails.CreatedBy;
                userData.CreatedOn = DateTime.Now;
                userData.UpdatedBy = userDetails.UpdatedBy;
                userData.UpdatedOn = DateTime.Now;

                uow.UserDetailRepository.Add(userData);
                int userId = uow.UserDetailRepository.Find(x => x.CDSID == userDetails.Cdsid).Select(y => y.Id).SingleOrDefault();

                foreach (var item in userDetails.MarketIds)
                {
                    userMarketDetails.Add(new UserMarket()
                    {
                        UserId = userId,
                        MarketId = item,
                        CreatedBy = userDetails.CreatedBy,
                        CreatedOn = DateTime.Now,
                        UpdatedBy = userDetails.UpdatedBy,
                        UpdatedOn = DateTime.Now
                    });
                }
                uow.UserMarketRepository.AddRange(userMarketDetails);
                userDetails.RoleId = uow.RoleRepository.Find(x => x.RoleName == "User").Select(y => y.Id).SingleOrDefault();
                userRole.Add(new UserRole()
                {
                    UserId = userId,
                    RoleId = userDetails.RoleId,
                    CreatedBy = userDetails.CreatedBy,
                    CreatedOn = DateTime.Now,
                    UpdatedBy = userDetails.UpdatedBy,
                    UpdatedOn = DateTime.Now
                });
                uow.UserRoleRepository.AddRange(userRole);
                //}
                uow.Dispose();
                return userDetails;
            }
            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }

        public UserManagementModel UpdateUserDetails(UserManagementModel userDetails)
        {
            try
            {
                List<string> userDetailFields = new List<string>();
                List<UserMarket> userMarketDetails = new List<UserMarket>();
                UserDetail userData = new UserDetail();

                using (var user = new UnitofWork())
                {
                    userDetails.RoleId = user.RoleRepository.Find(x => x.RoleName == "User").Select(y => y.Id).SingleOrDefault();
                    var userExists = user.UserDetailRepository.Find(x => x.Id != userDetails.Id && (x.CDSID == userDetails.Cdsid || x.UserName == userDetails.UserName || x.Email == userDetails.Email)).SingleOrDefault();
                    if (userExists != null)
                    {
                        if (userExists.UserRoles.Select(x => x.RoleId).SingleOrDefault() == userDetails.RoleId)
                        {
                            throw new Exception(Resource.GetResxValueByName("UserDuplicatemsg"));
                        }
                        else
                        {
                            throw new Exception(Resource.GetResxValueByName("SuperUserDuplicatemsg"));
                        }

                    }

                    userData.Id = userDetails.Id;
                    userData.CDSID = userDetails.Cdsid;
                    userData.UserName = userDetails.UserName;
                    userData.Email = userDetails.Email;

                    userData.CreatedBy = userDetails.CreatedBy;
                    userData.CreatedOn = DateTime.Now;
                    userData.UpdatedBy = userDetails.UpdatedBy;
                    userData.UpdatedOn = DateTime.Now;

                    userDetailFields.Add("CDSID");
                    userDetailFields.Add("UserName");
                    userDetailFields.Add("Email");
                    userDetailFields.Add("UpdatedBy");
                    userDetailFields.Add("UpdatedOn");

                    user.UserDetailRepository.Update(userData, userDetailFields);

                    var userMarketList = user.UserMarketRepository.Find(x => x.UserId == userDetails.Id).ToList();
                    user.UserMarketRepository.RemoveRange(userMarketList);

                    foreach (var item in userDetails.MarketIds)
                    {
                        userMarketDetails.Add(new UserMarket()
                        {
                            UserId = userDetails.Id,
                            MarketId = item,
                            CreatedBy = userDetails.CreatedBy,
                            CreatedOn = DateTime.Now,
                            UpdatedBy = userDetails.UpdatedBy,
                            UpdatedOn = DateTime.Now
                        });
                    }

                    user.UserMarketRepository.AddRange(userMarketDetails);

                }
                return userDetails;
            }
            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }

        public void DeleteUserDetails(int id)
        {
            try
            {
                List<UserRole> userRole = new List<UserRole>();
                List<UserMarket> userMarketDetails = new List<UserMarket>();
                UserDetail userData = new UserDetail();
                //using (var user = new UnitofWork())
                //{
                userData = uow.UserDetailRepository.Find(x => x.Id == id).SingleOrDefault();
                userRole = userData.UserRoles.ToList();
                userMarketDetails = userData.UserMarkets.ToList();

                uow.UserRoleRepository.RemoveRange(userRole);
                uow.UserMarketRepository.RemoveRange(userMarketDetails);
                uow.UserDetailRepository.Remove(userData);
                uow.Dispose();
                //}
            }
            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }

        public List<UserMarketDetails> GetUserMarketDetails(int uId)
        {
            try
            {
                List<UserMarketDetails> userMarketDetails = new List<UserMarketDetails>();
                using (var user = new UnitofWork())
                {
                    var marketDetails = user.UserMarketRepository.Find(x => x.UserId == uId).ToList();

                    foreach (var item in marketDetails)
                    {
                        userMarketDetails.Add(new UserMarketDetails()
                        {
                            MarketId = item.MarketId,
                            MarketName = item.Market.MarketName
                        });
                    }
                }
                return userMarketDetails;
            }
            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }

        public List<RoleDetails> GetTypeOfUser(string cdsid)
        {
            try
            {
                List<RoleDetails> userRole = new List<RoleDetails>();
                using (var user = new UnitofWork())
                {

                    var role = (from u1 in user.UserDetailRepository.Find(x => x.CDSID == cdsid).ToList()
                                join u2 in user.UserRoleRepository.GetAll() on u1.Id equals u2.UserId
                                join u3 in user.RoleRepository.GetAll() on u2.RoleId equals u3.Id
                                select new { u3.Id, u3.RoleName }).SingleOrDefault();
                    userRole.Add(new RoleDetails { RoleId = role.Id, RoleName = role.RoleName });
                }
                return userRole;
            }
            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }

        }
    }

    public class UserMarketDetails
    {
        [JsonProperty(PropertyName = "marketId")]
        public int MarketId { get; set; }
        [JsonProperty(PropertyName = "marketName")]
        public string MarketName { get; set; }
    }

    public class RoleDetails
    {
        [JsonProperty(PropertyName = "RoleId")]
        public int RoleId { get; set; }
        [JsonProperty(PropertyName = "RoleName")]
        public string RoleName { get; set; }
    }

}