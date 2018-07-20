using EfficiencyClassWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EfficiencyClassWebAPI.EF;
using Newtonsoft.Json;

namespace EfficiencyClassWebAPI.Models
{
    public class Marketdetails
    {
        [JsonProperty(PropertyName = "marketId")]
        public int MarketId { get; set; }
        [JsonProperty(PropertyName = "SpecMarketCode")]
        public int SpecMarketCode { get; set; }
        [JsonProperty(PropertyName = "marketName")]
        public string MarketName { get; set; }
        [JsonProperty(PropertyName = "CreatedBy")]
        public string CreatedBy { get; set; }
        [JsonProperty(PropertyName = "CreatedOn")]
        public DateTime? CreatedOn { get; set; }
        [JsonProperty(PropertyName = "UpdatedBy")]
        public string UpdatedBy { get; set; }
        [JsonProperty(PropertyName = "UpdatedOn")]
        public DateTime? UpdatedOn { get; set; }

        private UnitofWork uow;
        public Marketdetails()
        {
            uow = new UnitofWork();
        }

        public Marketdetails(UnitofWork unitofWork)
        {
            uow = unitofWork;
        }
        public List<Marketdetails> GetMarketDetails(string cdsid)
        {
            try
            {
                //using (var marketRepo = new UnitofWork())
                //{
                    int uId = uow.UserDetailRepository.Find(x => x.CDSID == cdsid).Select(y => y.Id).SingleOrDefault();
                    var markets = (from u1 in uow.UserMarketRepository.Find(x=>x.UserId==uId).ToList()
                                         join u3 in uow.MarketRepository.GetAll() on u1.MarketId equals u3.Id
                                         select new {u3.Id,u3.SpecMarket,u3.MarketName});
                    List<Marketdetails> marketDetails = new List<Marketdetails>();
                    foreach (var item in markets)
                    {
                        marketDetails.Add(new Marketdetails()
                        {
                            MarketId=item.Id,
                            SpecMarketCode=item.SpecMarket,
                            MarketName=item.MarketName
                        });
                    }
                uow.Dispose();
                return marketDetails;
                
                //}
            }
            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }
       
        public List<Marketdetails> GetSpecificMarketDetails()
        {
            List<Marketdetails> marketDetails = new List<Marketdetails>();
            //using (var market = new UnitofWork())
            //{
                var marketList = uow.MarketRepository.GetAll().ToList();
                
                foreach (var item in marketList)
                {
                    marketDetails.Add(new Marketdetails(uow)
                    {
                        MarketId=item.Id,
                        SpecMarketCode=item.SpecMarket,
                        MarketName=item.MarketName,
                        CreatedBy=item.CreatedBy,
                        CreatedOn=item.CreatedOn,
                        UpdatedBy=item.UpdatedBy,
                        UpdatedOn=item.UpdatedOn
                    });
                }
            uow.Dispose();
            //}

            return marketDetails;
        }

        public void AddSpecificMarketDetails(Marketdetails marketDetails)
        {
            try
            {
                //using (var market = new UnitofWork())
                //{
                    Market specMarket = new Market();
                    if (uow.MarketRepository.Find(x => x.SpecMarket == marketDetails.SpecMarketCode || x.MarketName == marketDetails.MarketName).Any())
                    {
                        throw new Exception(Resource.GetResxValueByName("MarketDuplicatemsg"));
                    }
                    specMarket.SpecMarket = marketDetails.SpecMarketCode;
                    specMarket.MarketName = marketDetails.MarketName;
                    specMarket.CreatedBy = marketDetails.CreatedBy;
                    specMarket.CreatedOn = marketDetails.CreatedOn;
                    specMarket.UpdatedBy = marketDetails.UpdatedBy;
                    specMarket.UpdatedOn = marketDetails.UpdatedOn;

                    uow.MarketRepository.Add(specMarket);
                uow.Dispose();
                // }
            }
            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw new Exception(ex.Message);
            }
        }

        public void UpdateSpecificMarketDetails(Marketdetails marketDetails)
        {
            try
            {
                using (var market = new UnitofWork())
                {
                    Market specMarket = new Market();
                    if (market.MarketRepository.Find(x => x.Id != marketDetails.MarketId && (x.SpecMarket == marketDetails.SpecMarketCode || x.MarketName == marketDetails.MarketName)).Any())
                    {
                        throw new Exception(Resource.GetResxValueByName("MarketDuplicatemsg"));
                    }
                    specMarket.Id = marketDetails.MarketId;
                    specMarket.SpecMarket = marketDetails.SpecMarketCode;
                    specMarket.MarketName = marketDetails.MarketName;
                    specMarket.CreatedBy = marketDetails.CreatedBy;
                    specMarket.CreatedOn = marketDetails.CreatedOn;
                    specMarket.UpdatedBy = marketDetails.UpdatedBy;
                    specMarket.UpdatedOn = marketDetails.UpdatedOn;

                    List<string> marketFields = new List<string>();
                    marketFields.Add("SpecMarket");
                    marketFields.Add("MarketName");
                    marketFields.Add("UpdatedBy");
                    marketFields.Add("UpdatedOn");


                    market.MarketRepository.Update(specMarket, marketFields);

                }
            }
            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw new Exception(ex.Message);
            }
        }

        public void DeleteSpecificMarketDetails(int marketId)
        {
            try
            {
                MarketDataModel marketObj = new MarketDataModel();
                using (var market = new UnitofWork())
                {
                    List<long> mmidLst = market.Market2MarketTypeParameterGroupRepository.Find(x => x.MarketId == marketId).Select(y => y.MMId).ToList();
                    foreach (var id in mmidLst)
                    {
                        marketObj.DeleteMarket(id);
                    }
                    List<UserMarket> userMarkets = market.UserMarketRepository.Find(x => x.MarketId == marketId).ToList();
                    market.UserMarketRepository.RemoveRange(userMarkets);
                }
                using (var item = new UnitofWork())
                {
                    Market marketDetails = item.MarketRepository.Find(x => x.Id == marketId).SingleOrDefault();
                    item.MarketRepository.Remove(marketDetails);
                }

            }
            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }
    }
}