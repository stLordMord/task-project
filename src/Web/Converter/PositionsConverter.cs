using ApplicationCore;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Converter
{
    public class PositionsConverter : IConverter<PositionBLO, PositionModel>
    {
        public IList<PositionModel> Convert(IList<PositionBLO> listBLO)
        {
            List<PositionModel> positions = new List<PositionModel>();
            foreach (PositionBLO positionBLO in listBLO)
            {
                PositionModel positionModel = Convert(positionBLO);
                positions.Add(positionModel);
            }
            return positions;
        }

        public PositionModel Convert(PositionBLO positionBLO)
        {
            PositionModel positionModel = new PositionModel()
            {
                Id = positionBLO.Id,
                Name = positionBLO.Name
            };
            return positionModel;
        }
        public PositionBLO Convert(PositionModel positionModel)
        {
            PositionBLO positionBLO= new PositionBLO()
            {
                Id = positionModel.Id,
                Name = positionModel.Name
            };
            return positionBLO;
        }
    }
}
