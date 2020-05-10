using ApplicationCore;
using Common;
using Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Converter
{
    class PositionConverter : IConverter<PositionDTO, PositionBLO>
    {
        private readonly ILogger<PositionConverter> logger;
        public PositionConverter(ILogger<PositionConverter> logger)
        {
            this.logger = logger;
        }
        public IList<PositionBLO> Convert(IList<PositionDTO> listDTO)
        {
            if (listDTO == null)
            {
                logger.LogError("Значение не может быть null");
                throw new ArgumentNullException(nameof(listDTO));
            }
            List<PositionBLO> positions = new List<PositionBLO>();
            foreach (PositionDTO positionDTO in listDTO)
            {
                PositionBLO position = Convert(positionDTO);
                positions.Add(position);
            }
            return positions;
        }

        public PositionBLO Convert(PositionDTO positionDTO)
        {
            if (positionDTO == null)
            {
                logger.LogError("Значение не может быть null");
                throw new ArgumentNullException(nameof(positionDTO));
            }
            PositionBLO positionBLO = new PositionBLO()
            {
                Id = positionDTO.Id,
                Name = positionDTO.Name
            };
            return positionBLO;
        }

        public PositionDTO Convert(PositionBLO positionBLO)
        {
            if (positionBLO == null)
            {
                logger.LogError("Значение не может быть null");
                throw new ArgumentNullException(nameof(positionBLO));
            }
            PositionDTO positionDTO = new PositionDTO()
            {
                Id = positionBLO.Id,
                Name = positionBLO.Name
            };
            return positionDTO;
        }
    }
}
