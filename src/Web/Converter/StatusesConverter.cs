using ApplicationCore;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Converter
{
    public class StatusesConverter : IConverter<StatusBLO, StatusModel>
    {
        public IList<StatusModel> Convert(IList<StatusBLO> listBLO)
        {
            List<StatusModel> statuses = new List<StatusModel>();
            foreach (StatusBLO statusBLO in listBLO)
            {
                StatusModel statusModel = Convert(statusBLO);
                statuses.Add(statusModel);
            }
            return statuses;
        }

        public StatusModel Convert(StatusBLO statusBLO)
        {
            StatusModel statusModel = new StatusModel()
            {
                Id = statusBLO.Id,
                Name = statusBLO.Name
            };
            return statusModel;
        }
        public StatusBLO Convert(StatusModel statusModel)
        {
            StatusBLO statusBLO = new StatusBLO()
            {
                Id = statusModel.Id,
                Name = statusModel.Name
            };
            return statusBLO;
        }
    }
}
