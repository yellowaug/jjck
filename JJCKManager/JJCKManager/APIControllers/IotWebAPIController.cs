using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;
using System.Web.Http;
using JJCKManager.Models;
using JJCKManager.BAL;

namespace JJCKManager.Controllers
{

    public class IotWebAPIController : ApiController
    {
        // GET: IotWebAPI
        public List<IotTemperListFunction> GetApi()
        {
            IiotDataList iiotData = new GetIotData();
            return iiotData.GetIotList();
        }
        [HttpPost]
        public IotTemperListFunction PostApi([FromBody]IotTemperListFunction iottempdata)
        {
            
            IAddIotData addIot = new AddData();
            addIot.addIotdata(iottempdata);
            return iottempdata; 
        }


    }
}